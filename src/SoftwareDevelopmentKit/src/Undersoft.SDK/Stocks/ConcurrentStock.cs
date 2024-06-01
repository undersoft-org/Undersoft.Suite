namespace Undersoft.SDK.Stocks
{
    using System.Linq;
    using System.Runtime.InteropServices;
    using System;
    using System.Threading;

    public unsafe class ConcurrentStock : StockBase
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct NodeHeader
        {
            public volatile int ReadEnd;
            public volatile int ReadStart;
            public volatile int WriteEnd;
            public volatile int WriteStart;

            public int NodeCount;
            public long NodeBufferSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Node
        {
            public int Next;
            public int Prev;
            public int Index;
            public long AmountWritten;

            public long Offset;

            public volatile int DoneRead;
            public volatile int DoneWrite;
        }

        public int NodeCount { get; private set; }
        public long NodeBufferSize { get; private set; }

        protected EventWaitHandle DataExists { get; set; }
        protected EventWaitHandle NodeAvailable { get; set; }

        protected virtual long NodeHeaderOffset
        {
            get { return 0; }
        }

        protected virtual long NodeOffset
        {
            get { return NodeHeaderOffset + Marshal.SizeOf(typeof(NodeHeader)); }
        }

        protected virtual long NodeBufferOffset
        {
            get { return NodeOffset + Marshal.SizeOf(typeof(Node)) * NodeCount; }
        }

        protected virtual Node* this[int i]
        {
            get
            {
                if (i < 0 || i >= NodeCount)
                    throw new ArgumentOutOfRangeException();

                return (Node*)(BufferStartPtr + NodeOffset) + i;
            }
        }

        private NodeHeader* _nodeHeader = null;

        public ConcurrentStock(string file, string name, int nodeCount, long nodeBufferSize) :
            this(file, name, nodeCount, nodeBufferSize, true)
        {
            Open();
            CheckArchive();
        }

        public ConcurrentStock(string file, string name) :
            this(file, name, 0, 0, false)
        {
            Open();
        }

        private ConcurrentStock(string file, string name, int nodeCount, long nodeBufferSize, bool ownsSharedMemory) :
            base(file, name, 1, Marshal.SizeOf(typeof(NodeHeader)) +
                             Marshal.SizeOf(typeof(Node)) * nodeCount +
                             nodeCount * nodeBufferSize,
                ownsSharedMemory)
        {
            if (ownsSharedMemory && nodeCount < 2)
                throw new ArgumentOutOfRangeException("nodeCount", nodeCount, "The node count must be a minimum of 2.");
#if DEBUG
            else if (!ownsSharedMemory && (nodeCount != 0 || nodeBufferSize > 0))
                System.Diagnostics.Debug.Write(
                    "Node count and nodeBufferSize are ignored when opening an existing shared memory circular buffer.",
                    "Warning");
#endif

            if (IsOwnerOfSharedMemory)
            {
                NodeCount = nodeCount;
                NodeBufferSize = nodeBufferSize;
            }
        }

        public bool CheckArchive(int timeout = 1000)
        {
            if (Exists)
            {
                if (FixSize)
                {
                    NodeBufferSize = (int)((BufferSize - (Marshal.SizeOf(typeof(NodeHeader)) +
                                                          Marshal.SizeOf(typeof(Node)) * NodeCount)) / NodeCount);
                }

                Node* firstnode = GetNodeForWriting(timeout);
                if (firstnode == null) return false;
                long firstamount = Math.Min(NodeBufferSize, NodeBufferSize);
                firstnode->AmountWritten = firstamount;
                PostNode(firstnode);
                return true;
            }

            return false;
        }

        protected override bool DoOpen()
        {
            DataExists = new EventWaitHandle(false, EventResetMode.AutoReset, Name + "_evt_dataexists");
            NodeAvailable = new EventWaitHandle(false, EventResetMode.AutoReset, Name + "_evt_nodeavail");

            if (IsOwnerOfSharedMemory)
            {
                _nodeHeader = (NodeHeader*)(BufferStartPtr + NodeHeaderOffset);

                InitialiseNodeHeader();

                InitialiseLinkedListNodes();
            }
            else
            {
                _nodeHeader = (NodeHeader*)(BufferStartPtr + NodeHeaderOffset);
                NodeCount = _nodeHeader->NodeCount;
                NodeBufferSize = _nodeHeader->NodeBufferSize;
            }

            return true;
        }

        private void InitialiseNodeHeader()
        {
            if (!IsOwnerOfSharedMemory)
                return;

            NodeHeader header = new NodeHeader();
            header.ReadStart = 0;
            header.ReadEnd = 0;
            header.WriteEnd = 0;
            header.WriteStart = 0;
            header.NodeBufferSize = NodeBufferSize;
            header.NodeCount = NodeCount;
            object head = header;
            base.Write(head, NodeHeaderOffset);
        }

        private void InitialiseLinkedListNodes()
        {
            if (!IsOwnerOfSharedMemory)
                return;

            int RtStruct = 0;

            Node[] nodes = new Node[NodeCount];

            nodes[RtStruct].Next = 1;
            nodes[RtStruct].Prev = NodeCount - 1;
            nodes[RtStruct].Offset = NodeBufferOffset;
            nodes[RtStruct].Index = RtStruct;

            for (RtStruct = 1; RtStruct < NodeCount - 1; RtStruct++)
            {
                nodes[RtStruct].Next = RtStruct + 1;
                nodes[RtStruct].Prev = RtStruct - 1;
                nodes[RtStruct].Offset = NodeBufferOffset + NodeBufferSize * RtStruct;
                nodes[RtStruct].Index = RtStruct;
            }

            nodes[RtStruct].Next = 0;
            nodes[RtStruct].Prev = NodeCount - 2;
            nodes[RtStruct].Offset = NodeBufferOffset + NodeBufferSize * RtStruct;
            nodes[RtStruct].Index = RtStruct;

            base.Write(nodes.SelectMany(o => new object[] { o }).ToArray(), 0, nodes.Length, NodeOffset, typeof(Node));
        }

        protected override void DoClose()
        {
            if (DataExists != null)
            {
                (DataExists as IDisposable).Dispose();
                DataExists = null;
                (NodeAvailable as IDisposable).Dispose();
                NodeAvailable = null;
            }

            _nodeHeader = null;
        }

        protected virtual Node* GetNodeForWriting(int timeout)
        {
            for (; ; )
            {
                int blockIndex = _nodeHeader->WriteStart;
                Node* node = this[blockIndex];
                if (node->Next == _nodeHeader->ReadEnd)
                {
                    if (NodeAvailable.WaitOne(timeout))
                        continue;

                    return null;
                }

#pragma warning disable 0420
                if (Interlocked.CompareExchange(ref _nodeHeader->WriteStart, node->Next, blockIndex) == blockIndex)
                    return node;
#pragma warning restore 0420

                continue;
            }
        }

        protected virtual void PostNode(Node* node)
        {
            node->DoneWrite = 1;

            for (; ; )
            {
                int blockIndex = _nodeHeader->WriteEnd;
                node = this[blockIndex];
#pragma warning disable 0420
                if (Interlocked.CompareExchange(ref node->DoneWrite, 0, 1) != 1)
                {
                    return;
                }

                Interlocked.CompareExchange(ref _nodeHeader->WriteEnd, node->Next, blockIndex);
#pragma warning restore 0420

                if (blockIndex == _nodeHeader->ReadStart)
                    DataExists.Set();
            }
        }

        public virtual int Write(byte[] source, int startIndex = 0, int timeout = 1000)
        {
            Node* node = GetNodeForWriting(timeout);
            if (node == null) return 0;

            long amount = Math.Min(source.Length - startIndex, NodeBufferSize);

            Marshal.Copy(source, startIndex, new nint(BufferStartPtr + node->Offset), (int)amount);
            node->AmountWritten = amount;

            PostNode(node);

            return (int)amount;
        }

        public virtual int Write(object source, int startIndex = 0, Type t = null, int timeout = 1000)
        {
            int structSize = Marshal.SizeOf(source.GetType());
            if (structSize > NodeBufferSize)
                throw new ArgumentOutOfRangeException("T",
                    "The size of structure " + source.GetType().Name + " is larger than NodeBufferSize");

            Node* node = GetNodeForWriting(timeout);
            if (node == null) return 0;

            base.Write(source, node->Offset);
            node->AmountWritten = structSize;

            PostNode(node);

            return structSize;
        }

        public virtual int Write(object[] source, int startIndex = 0, Type t = null, int timeout = 1000)
        {
            Node* node = GetNodeForWriting(timeout);
            if (node == null) return 0;

            long count = Math.Min(source.Length - startIndex, NodeBufferSize / Marshal.SizeOf(source.GetType()));
            base.Write(source, startIndex, (int)count, node->Offset);
            node->AmountWritten = count * Marshal.SizeOf(source.GetType());

            PostNode(node);

            return (int)count;
        }

        public virtual int Write(nint source, int length, Type t = null, int timeout = 1000)
        {
            Node* node = GetNodeForWriting(timeout);
            if (node == null) return 0;

            long amount = Math.Min(length, NodeBufferSize);
            base.Write(source, amount, node->Offset);
            node->AmountWritten = amount;

            PostNode(node);

            return (int)amount;
        }

        public virtual int Write(Func<nint, int> writeFunc, int timeout = 1000)
        {
            Node* node = GetNodeForWriting(timeout);
            if (node == null) return 0;

            int amount = 0;
            try
            {
                amount = writeFunc(new nint(BufferStartPtr + node->Offset));
                node->AmountWritten = amount;
            }
            finally
            {
                PostNode(node);
            }

            return amount;
        }

        public NodeHeader ReadNodeHeader()
        {
            return (NodeHeader)Marshal.PtrToStructure(new nint(_nodeHeader), typeof(NodeHeader));
        }

        protected virtual Node* GetNodeForReading(int timeout)
        {
            for (; ; )
            {
                int blockIndex = _nodeHeader->ReadStart;
                Node* node = this[blockIndex];
                if (blockIndex == _nodeHeader->WriteEnd)
                {
                    if (DataExists.WaitOne(timeout))
                        continue;

                    return null;
                }

#pragma warning disable 0420
                if (Interlocked.CompareExchange(ref _nodeHeader->ReadStart, node->Next, blockIndex) == blockIndex)
                    return node;
#pragma warning restore 0420

                continue;
            }
        }

        protected virtual void ReturnNode(Node* node)
        {
            node->DoneRead = 1;

            node->AmountWritten = 0;

            for (; ; )
            {
                int blockIndex = _nodeHeader->ReadEnd;
                node = this[blockIndex];
#pragma warning disable 0420
                if (Interlocked.CompareExchange(ref node->DoneRead, 0, 1) != 1)
                {
                    return;
                }

                Interlocked.CompareExchange(ref _nodeHeader->ReadEnd, node->Next, blockIndex);
#pragma warning restore 0420

                if (node->Prev == _nodeHeader->WriteStart)
                    NodeAvailable.Set();
            }
        }

        public virtual int Read(byte[] destination, int startIndex = 0, Type t = null, int timeout = 1000)
        {
            Node* node = GetNodeForReading(timeout);
            if (node == null) return 0;

            int amount = (int)Math.Min(destination.Length - startIndex, node->AmountWritten);

            Marshal.Copy(new nint(BufferStartPtr + node->Offset), destination, startIndex, amount);

            ReturnNode(node);

            return amount;
        }

        public virtual int Read(object destination, int startIndex = 0, Type t = null, int timeout = 1000)
        {
            int structSize = Marshal.SizeOf(t);
            if (structSize > NodeBufferSize)
                throw new ArgumentOutOfRangeException("T",
                    "The size of structure " + t.Name + " is larger than NodeBufferSize");

            Node* node = GetNodeForReading(timeout);
            if (node == null && t != null)
            {
                destination = GetDefault(t);
                return 0;
            }

            base.Read(destination, node->Offset);

            ReturnNode(node);

            return structSize;
        }

        public virtual int Read(object[] destination, int startIndex = 0, Type t = null, int timeout = 1000)
        {
            Node* node = GetNodeForReading(timeout);
            if (node == null) return 0;

            int count = (int)Math.Min(destination.Length - startIndex, node->AmountWritten / Marshal.SizeOf(t));
            base.Read(destination, startIndex, count, node->Offset);

            ReturnNode(node);

            return count;
        }

        public virtual int Read(nint destination, int length, Type t = null, int timeout = 1000)
        {
            Node* node = GetNodeForReading(timeout);
            if (node == null) return 0;

            int amount = (int)Math.Min(length, node->AmountWritten);

            base.Read(destination, amount, node->Offset);

            ReturnNode(node);

            return amount;
        }

        public virtual int Read(Func<nint, int> readFunc, int timeout = 1000)
        {
            Node* node = GetNodeForReading(timeout);
            if (node == null) return 0;

            int amount = 0;
            try
            {
                amount = readFunc(new nint(BufferStartPtr + node->Offset));
            }
            finally
            {
                ReturnNode(node);
            }

            return amount;
        }

        public static object GetDefault(Type t)
        {
            Func<object> f = GetDefault<object>;
            return f.Method.GetGenericMethodDefinition().MakeGenericMethod(t).Invoke(null, null);
        }

        private static T GetDefault<T>()
        {
            return default;
        }
    }
}