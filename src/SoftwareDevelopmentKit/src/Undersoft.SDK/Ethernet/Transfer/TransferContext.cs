using System.Collections;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Ethernet.Transfer
{
    public sealed class TransferContext : ITransferContext, IDisposable
    {
        const int BUFFER_SIZE = 4096;

        readonly EthernetStream instream;

        readonly EthernetStream outstream;

        byte[] inputData = new byte[0];

        private nint inputDataPtr;

        private nint inputHandlePtr;

        private byte[] sendData = new byte[0];

        private nint outputDataPtr;

        private nint outputHandlePtr;

        private nint headerBufferAddress;

        private nint headerBufferHandler;

        private nint messageBufferAddress;

        private nint messageBufferHandler;

        public MemoryStream receiveStream;

        public MemoryStream sendStream;

        private Socket _listener;
        private int DATA_OFFSET = 16;

        private bool disposed = false;

        private byte[] headerbuffer = new byte[BUFFER_SIZE];

        private int id;

        private byte[] messagebuffer = new byte[BUFFER_SIZE];

        private ISeries<byte[]> resources;

        private EthernetTransfer tr;

        public TransferContext(Socket listener, int _id = -1, bool withStream = false)
        {
            _listener = listener;
            if (withStream)
            {
                instream = new EthernetStream(listener);
                outstream = new EthernetStream(listener);
            }

            GCHandle gc = GCHandle.Alloc(messagebuffer, GCHandleType.Pinned);
            messageBufferHandler = GCHandle.ToIntPtr(gc);
            messageBufferAddress = gc.AddrOfPinnedObject();
            gc = GCHandle.Alloc(headerbuffer, GCHandleType.Pinned);
            headerBufferHandler = GCHandle.ToIntPtr(gc);
            headerBufferAddress = gc.AddrOfPinnedObject();

            id = _id;
            Close = false;
            Denied = false;
            ItemIndex = 0;
            ItemsLeft = 0;
            InputId = 0;
            Size = 0;
            HasMessageToSend = true;
            HasMessageToReceive = true;
            disposed = true;

            HeaderSentNotice.Reset();
            HeaderReceivedNotice.Reset();
            MessageSentNotice.Reset();
            MessageReceivedNotice.Reset();
            ChunksReceivedNotice.Reset();
        }

        public ManualResetEvent ChunksReceivedNotice { get; set; } = new ManualResetEvent(false);

        public int Offset
        {
            get { return DATA_OFFSET; }
            set { DATA_OFFSET = value; }
        }

        public long Size { get; set; }

        public int BufferSize
        {
            get { return BUFFER_SIZE; }
        }

        public bool Close { get; set; }

        public bool Denied { get; set; }

        public byte[] Input
        {
            get
            {
                byte[] result = null;
                lock (inputData)
                {
                    disposed = false;
                    Size = 0;
                    result = inputData;
                    inputData = new byte[0];
                }
                return result;
            }
        }

        public int InputId { get; set; }

        public nint InputPtr => inputDataPtr;

        public byte[] HeaderBuffer
        {
            get { return headerbuffer; }
        }

        public ManualResetEvent HeaderReceivedNotice { get; set; } = new ManualResetEvent(false);

        public ManualResetEvent HeaderSentNotice { get; set; } = new ManualResetEvent(false);

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Socket Listener
        {
            get { return _listener; }
            set { _listener = value; }
        }

        public byte[] MessageBuffer
        {
            get { return messagebuffer; }
        }

        public ManualResetEvent MessageReceivedNotice { get; set; } = new ManualResetEvent(false);

        public ManualResetEvent MessageSentNotice { get; set; } = new ManualResetEvent(false);

        public ProtocolMethod Method { get; set; } = ProtocolMethod.NONE;

        public int ItemIndex { get; set; }

        public int ItemsLeft { get; set; }

        public EthernetProtocol Protocol { get; set; } = EthernetProtocol.NONE;

        public bool HasMessageToReceive { get; set; }

        public bool HasMessageToSend { get; set; }

        public byte[] Output
        {
            get { return sendData; }
            set
            {
                if (value != null)
                {
                    lock (sendData)
                    {
                        disposed = false;
                        sendData = value;
                        if (Protocol != EthernetProtocol.HTTP)
                        {
                            long size = sendData.Length - Offset;
                            new byte[] { (byte)'D', (byte)'E', (byte)'A', (byte)'L' }.CopyTo(
                                sendData,
                                0
                            );
                            BitConverter.GetBytes(size).CopyTo(sendData, 4);
                            BitConverter.GetBytes(ItemIndex).CopyTo(sendData, 12);
                        }
                        value = null;
                    }
                }
            }
        }

        public int OutputId { get; set; }

        public nint OutputPtr => outputDataPtr;

        public EthernetSite Site
        {
            get { return Transfer.ResponseHeader.Context.IdentitySite; }
        }

        public bool Synchronic { get; set; }

        public EthernetTransfer Transfer
        {
            get { return tr; }
            set
            {
                if (value.Context == null)
                {
                    value.Context = this;
                    value.ResponseHeader.BindContext(value.Context);
                }
                if (value.ResponseMessage.Data != null)
                {
                    if (value.ResponseMessage.Data.GetType() == typeof(object[][]))
                        value.ResponseHeader.Context.ItemsCount = (
                            (object[][])value.ResponseMessage.Data
                        ).Length;
                }
                tr = value;
            }
        }

        internal EthernetMethod SendEcho { get; set; }

        public void DirSearch(string dir, List<string> jspfiles)
        {
            try
            {
                foreach (string f in Directory.GetFiles(dir))
                    jspfiles.Add(f);
                foreach (string d in Directory.GetDirectories(dir))
                    DirSearch(d, jspfiles);
            }
            catch (Exception ex) { }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                sendStream.Dispose();
                receiveStream.Dispose();
                GCHandle gc;
                lock (inputData)
                {
                    if (!inputHandlePtr.Equals(nint.Zero))
                    {
                        gc = GCHandle.FromIntPtr(inputHandlePtr);
                        gc.Free();
                    }
                    inputData = null;
                }
                lock (sendData)
                {
                    if (!outputHandlePtr.Equals(nint.Zero))
                    {
                        gc = GCHandle.FromIntPtr(outputHandlePtr);
                        gc.Free();
                    }
                    sendData = null;
                }

                gc = GCHandle.FromIntPtr(messageBufferHandler);
                gc.Free();
                messagebuffer = null;
                gc = GCHandle.FromIntPtr(headerBufferHandler);
                gc.Free();
                headerbuffer = null;

                HeaderSentNotice.Dispose();
                HeaderReceivedNotice.Dispose();
                MessageSentNotice.Dispose();
                MessageReceivedNotice.Dispose();
                ChunksReceivedNotice.Dispose();

                disposed = true;
            }
        }

        public MarkupType ReadHeader(int received)
        {
            disposed = false;
            return ProcessHeader(received);
        }

        public unsafe MarkupType ReadMessage(int received)
        {
            disposed = false;
            return ProcessMessage(received);
        }

        public void Reset()
        {
            if (!disposed)
            {
                sendStream.Dispose();
                sendStream = new MemoryStream();
                receiveStream.Dispose();
                receiveStream = new MemoryStream();

                lock (inputData)
                {
                    if (!inputHandlePtr.Equals(nint.Zero))
                    {
                        GCHandle gc = GCHandle.FromIntPtr(inputHandlePtr);
                        gc.Free();
                        inputData = new byte[0];
                    }
                }
                lock (sendData)
                    sendData = new byte[0];
            }
        }

        public unsafe MarkupType ProcessHeader(int received)
        {
            MarkupType noiseKind = MarkupType.None;

            lock (inputData)
            {
                int offset = 0,
                    length = received;
                bool inprogress = false;
                if (Size == 0)
                {
                    Size = *(int*)(headerBufferAddress + 4).ToPointer();
                    InputId = *(int*)(headerBufferAddress + 12).ToPointer();

                    inputData = new byte[BufferSize];
                    GCHandle gc = GCHandle.Alloc(inputData, GCHandleType.Pinned);
                    inputHandlePtr = GCHandle.ToIntPtr(gc);
                    inputDataPtr = gc.AddrOfPinnedObject();

                    offset = Offset;
                    length -= Offset;
                }

                if (Size > 0)
                    inprogress = true;

                Size -= length;

                if (Size < 1)
                {
                    long endPosition = length;
                    noiseKind = HeaderBuffer.SeekMarkup(out endPosition, SeekDirection.Backward);
                }

                int destid = (int)(inputData.Length - (Size + length));

                if (inprogress)
                {
                    Extracting.Extract.CopyBlock(
                        inputDataPtr,
                        destid,
                        headerBufferAddress,
                        offset,
                        length
                    );
                }
            }
            return noiseKind;
        }

        public unsafe MarkupType ProcessMessage(int received)
        {
            MarkupType noiseKind = MarkupType.None;

            lock (inputData)
            {
                int offset = 0,
                    length = received;
                bool inprogress = false;

                if (Size == 0)
                {
                    Size = *(int*)(messageBufferAddress + 4).ToPointer();
                    InputId = *(int*)(messageBufferAddress + 12).ToPointer();

                    inputData = new byte[Size];
                    GCHandle gc = GCHandle.Alloc(inputData, GCHandleType.Pinned);
                    inputHandlePtr = GCHandle.ToIntPtr(gc);
                    inputDataPtr = gc.AddrOfPinnedObject();

                    offset = Offset;
                    length -= Offset;
                }

                if (Size > 0)
                    inprogress = true;

                Size -= length;

                if (Size < 1)
                {
                    long endPosition = length;
                    noiseKind = MessageBuffer.SeekMarkup(out endPosition, SeekDirection.Backward);
                }

                int destid = (int)(inputData.Length - (Size + length));
                if (inprogress)
                {
                    Extracting.Extract.CopyBlock(
                        inputDataPtr,
                        destid,
                        messageBufferAddress,
                        offset,
                        length
                    );
                }
            }
            return noiseKind;
        }
    }
}
