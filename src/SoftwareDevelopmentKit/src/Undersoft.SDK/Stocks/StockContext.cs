using Undersoft.SDK.Extracting;

namespace Undersoft.SDK.Stocks
{
    using System.IO;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Proxies;

    public unsafe class StockContext : InnerProxy, IStockContext, IDisposable
    {
        public nint binReceivePtr;
        public nint binSendPtr;
        public int ClientWaitCount = 0;
        public int ReadCount = 0;
        public int ServerWaitCount = 0;
        public int WriteCount = 0;
        private byte[] binReceive = new byte[0];
        private byte[] binSend = new byte[0];
        private MemoryStream msRead = new MemoryStream();
        private MemoryStream msReceive = new MemoryStream();

        public StockContext()
        {
        }

        public int BlockOffset { get; set; } = 16;

        public long BlockSize { get; set; } = 0;

        public long BufferSize { get; set; } = 1048576;

        public int ClientCount { get; set; } = 1;

        public byte[] DeserialBlock
        {
            get
            {
                byte[] result = null;
                lock (binReceive)
                {
                    BlockSize = 0;
                    result = binReceive;
                    binReceive = new byte[0];
                }
                return result;
            }
        }

        public int DeserialBlockId { get; set; } = 0;

        public nint DeserialBlockPtr
        {
            get { return GCHandle.FromIntPtr(binReceivePtr).AddrOfPinnedObject() + BlockOffset; }
        }

        public int Elements { get; set; } = 1;

        public string File { get; set; }

        public long FreeSize { get; set; } = 0;

        public long ItemCapacity { get; set; } = -1;

        public int ItemCount { get; set; } = -1;

        public int ItemSize { get; set; } = -1;

        public int NodeCount { get; set; } = 50;

        public int ObjectPosition { get; set; } = 0;

        public int ObjectsLeft { get; set; } = 0;

        public string Path { get; set; }

        public ushort SectorId { get; set; } = 0;

        public byte[] SerialBlock
        {
            get
            {
                return binSend;
            }
            set
            {
                binSend = value;
                if (binSend != null && BlockOffset > 0)
                {
                    long size = binSend.Length - BlockOffset;
                    new byte[] { (byte) 'V',
                                 (byte) 'S',
                                 (byte) 'S',
                                 (byte) 'P' }.CopyTo(binSend, 0);
                    size.GetBytes().CopyTo(binSend, 4);
                    ObjectPosition.GetBytes().CopyTo(binSend, 12);
                    GCHandle gc = GCHandle.Alloc(binSend, GCHandleType.Pinned);
                    binSendPtr = GCHandle.ToIntPtr(gc);
                }
            }
        }

        public int SerialBlockId { get; set; } = 0;

        public nint SerialBlockPtr
        {
            get { return GCHandle.FromIntPtr(binSendPtr).AddrOfPinnedObject(); }
        }

        public int ServerCount { get; set; } = 1;

        public ushort StockId { get; set; } = 0;

        public long UsedSize { get; set; } = 0;

        public void Dispose()
        {
            msRead.Dispose();
            msReceive.Dispose();
            if (!binReceivePtr.Equals(nint.Zero))
            {
                GCHandle gc = GCHandle.FromIntPtr(binReceivePtr);
                gc.Free();
            }
            if (!binSendPtr.Equals(nint.Zero))
            {
                GCHandle gc = GCHandle.FromIntPtr(binSendPtr);
                gc.Free();
            }
            binReceive = null;
            binSend = null;
        }

        public object ReadStock(ITableStock stock)
        {
            if (stock != null)
            {
                stock.ReadHeader();
                BufferSize = stock.BufferSize;
                byte[] bufferread = new byte[BufferSize];
                GCHandle handler = GCHandle.Alloc(bufferread, GCHandleType.Pinned);
                nint rawpointer = handler.AddrOfPinnedObject();
                stock.Read(rawpointer, BufferSize, 0L);
                ReceiveBytes(bufferread, BufferSize);
                handler.Free();
            }
            return DeserialBlock;
        }

        public nint ReadStockPtr(ITableStock stock)
        {
            if (stock != null)
            {
                stock.ReadHeader();
                BufferSize = stock.BufferSize;
                binReceive = new byte[BufferSize];
                GCHandle handler = GCHandle.Alloc(binReceive, GCHandleType.Pinned);
                binReceivePtr = GCHandle.ToIntPtr(handler);
                nint rawpointer = handler.AddrOfPinnedObject();
                stock.Read(rawpointer, BufferSize, 0);
                ReceiveBytes(rawpointer, BufferSize);
            }
            return DeserialBlockPtr;
        }

        public MarkupType ReceiveBytes(byte[] buffer, int received)
        {
            MarkupType noiseKind = MarkupType.None;
            lock (binReceive)
            {
                int offset = 0, length = received;
                bool inprogress = false;

                if (BlockSize == 0)
                {
                    BlockSize = BitConverter.ToInt64(buffer, 4);
                    DeserialBlockId = BitConverter.ToInt32(buffer, 12);
                    binReceive = new byte[BlockSize];
                    GCHandle gc = GCHandle.Alloc(binReceive, GCHandleType.Pinned);
                    binReceivePtr = GCHandle.ToIntPtr(gc);
                    offset = BlockOffset;
                    length -= BlockOffset;

                }
                if (BlockSize > 0)
                    inprogress = true;

                BlockSize -= length;

                if (BlockSize < 1)
                {
                    long endPosition = received;
                    noiseKind = buffer.SeekMarkup(out endPosition, SeekDirection.Backward);
                }
                int destid = binReceive.Length - ((int)BlockSize + length);
                if (inprogress)
                {
                    fixed (void* msgbuff = buffer)
                    {
                        Extract.CopyBlock(GCHandle.FromIntPtr(binReceivePtr).AddrOfPinnedObject().ToPointer(), (ulong)destid, msgbuff, (ulong)offset, (ulong)length);
                    }
                }
            }
            return noiseKind;
        }

        public MarkupType ReceiveBytes(byte[] buffer, long received)
        {

            MarkupType noiseKind = MarkupType.None;
            lock (binReceive)
            {
                int offset = 0, length = (int)received;
                bool inprogress = false;
                if (BlockSize == 0)
                {

                    BlockSize = BitConverter.ToInt64(buffer, 4);
                    DeserialBlockId = BitConverter.ToInt32(buffer, 12);
                    binReceive = new byte[BlockSize];
                    GCHandle gc = GCHandle.Alloc(binReceive, GCHandleType.Pinned);
                    binReceivePtr = GCHandle.ToIntPtr(gc);
                    offset = BlockOffset;
                    length -= BlockOffset;
                }

                if (BlockSize > 0)
                    inprogress = true;

                BlockSize -= length;

                if (BlockSize < 1)
                {
                    long endPosition = received;
                    noiseKind = buffer.SeekMarkup(out endPosition, SeekDirection.Backward);
                }

                int destid = binReceive.Length - ((int)BlockSize + length);
                if (inprogress)
                {
                    fixed (byte* msgbuff = buffer)
                    {
                        Extract.CopyBlock(GCHandle.FromIntPtr(binReceivePtr).AddrOfPinnedObject().ToPointer(), (ulong)destid, msgbuff, (ulong)offset, (ulong)length);

                    }
                }
            }
            return noiseKind;
        }

        public void ReceiveBytes(nint buffer, long received)
        {
            lock (binReceive)
            {
                BlockSize = *(int*)(buffer + 4);
                DeserialBlockId = *(int*)(buffer + 12);
            }
        }

        public void WriteStock(ITableStock stock)
        {
            if (stock != null)
            {
                GCHandle handler = GCHandle.Alloc(SerialBlock, GCHandleType.Pinned);
                nint rawpointer = handler.AddrOfPinnedObject();
                stock.BufferSize = SerialBlock.Length;
                stock.WriteHeader();
                stock.Write(rawpointer, SerialBlock.Length);
                handler.Free();
            }
        }

        public void WriteStockPtr(ITableStock stock)
        {
            if (stock != null)
            {
                stock.BufferSize = BlockSize;
                stock.WriteHeader();
                stock.Write(SerialBlockPtr, BlockSize);
            }
        }
    }
}
