using System.Collections;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Ethernet
{
    public sealed class TransitContext : ITransitContext, IDisposable
    {
        private const int BUFFER_SIZE = 4096;

        private readonly EthernetStream instream;

        private readonly EthernetStream outstream;

        public byte[] receiveData = new byte[0];

        public IntPtr receiveDataAddress;

        public IntPtr receiveDataHandler;

        public byte[] sendData = new byte[0];

        public IntPtr sendDataAddress;

        public IntPtr sendDataHandler;

        public IntPtr headerBufferAddress;

        public IntPtr headerBufferHandler;

        public IntPtr messageBufferAddress;

        public IntPtr messageBufferHandler;

        public MemoryStream receiveStream;

        public MemoryStream sendStream;

        private Socket _listener;
        private int DATA_OFFSET = 16;

        private bool disposed = false;

        private byte[] headerbuffer = new byte[BUFFER_SIZE];

        private int id;

        private byte[] messagebuffer = new byte[BUFFER_SIZE];

        private ISeries<byte[]> resources;

        private StringBuilder sb = new StringBuilder();

        private EthernetTransit tr;

        public TransitContext(Socket listener, int _id = -1, bool withStream = false)
        {
            this._listener = listener;
            if (withStream)
            {
                this.instream = new EthernetStream(listener);
                this.outstream = new EthernetStream(listener);
            }

            GCHandle gc = GCHandle.Alloc(messagebuffer, GCHandleType.Pinned);
            messageBufferHandler = GCHandle.ToIntPtr(gc);
            messageBufferAddress = gc.AddrOfPinnedObject();
            gc = GCHandle.Alloc(headerbuffer, GCHandleType.Pinned);
            headerBufferHandler = GCHandle.ToIntPtr(gc);
            headerBufferAddress = gc.AddrOfPinnedObject();

            this.id = _id;
            this.Close = false;
            this.Denied = false;
            this.ItemIndex = 0;
            this.ItemsLeft = 0;
            this.InputId = 0;
            this.Size = 0;
            this.HasMessageToSend = true;
            this.HasMessageToReceive = true;
            this.disposed = true;

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
                lock (receiveData)
                {
                    disposed = false;
                    Size = 0;
                    result = receiveData;
                    receiveData = new byte[0];
                }
                return result;
            }
        }

        public int InputId { get; set; }

        public IntPtr InputPtr => receiveDataAddress;

        public string Echo
        {
            get { return this.sb.ToString(); }
        }

        public byte[] HeaderBuffer
        {
            get { return this.headerbuffer; }
        }

        public ManualResetEvent HeaderReceivedNotice { get; set; } = new ManualResetEvent(false);

        public ManualResetEvent HeaderSentNotice { get; set; } = new ManualResetEvent(false);      

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public Socket Listener
        {
            get { return this._listener; }
            set { this._listener = value; }
        }

        public byte[] MessageBuffer
        {
            get { return this.messagebuffer; }
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

        public IntPtr OutputPtr => sendDataAddress;

        public EthernetSite Site
        {
            get { return Transfer.MyHeader.Context.IdentitySite; }
        }

        public bool Synchronic { get; set; }

        public EthernetTransit Transfer
        {
            get { return this.tr; }
            set
            {
                if (value.Context == null)
                {
                    value.Context = this;
                    value.MyHeader.BindContext(value.Context);
                }
                if (value.MyMessage.Data != null)
                {
                    if (value.MyMessage.Data.GetType() == typeof(object[][]))
                        value.MyHeader.Context.ItemsCount = (
                            (object[][])value.MyMessage.Data
                        ).Length;
                }
                this.tr = value;
            }
        }

        internal EthernetMethod SendEcho { get; set; }

        public void Append(string text)
        {
            this.sb.Append(text);
        }

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
                sb.Clear();
                sendStream.Dispose();
                receiveStream.Dispose();
                GCHandle gc;
                lock (receiveData)
                {
                    if (!receiveDataHandler.Equals(IntPtr.Zero))
                    {
                        gc = GCHandle.FromIntPtr(receiveDataHandler);
                        gc.Free();
                    }
                    receiveData = null;
                }
                lock (sendData)
                {
                    if (!sendDataHandler.Equals(IntPtr.Zero))
                    {
                        gc = GCHandle.FromIntPtr(sendDataHandler);
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

        public MarkupType IncomingHeader(int received)
        {
            disposed = false;
            return SyncHeader(received);
        }

        public unsafe MarkupType IncomingMessage(int received)
        {
            disposed = false;
            return SyncMessage(received);
        }       

        public void Reset()
        {
            if (!disposed)
            {
                sb.Clear();
                sb = new StringBuilder();
                sendStream.Dispose();
                sendStream = new MemoryStream();
                receiveStream.Dispose();
                receiveStream = new MemoryStream();

                lock (receiveData)
                {
                    if (!receiveDataHandler.Equals(IntPtr.Zero))
                    {
                        GCHandle gc = GCHandle.FromIntPtr(receiveDataHandler);
                        gc.Free();
                        receiveData = new byte[0];
                    }
                }
                lock (sendData)
                    sendData = new byte[0];
            }
        }

        public unsafe MarkupType SyncHeader(int received)
        {
            MarkupType noiseKind = MarkupType.None;

            lock (receiveData)
            {
                int offset = 0,
                    length = received;
                bool inprogress = false;
                if (Size == 0)
                {
                    Size = *((int*)(headerBufferAddress + 4).ToPointer());
                    InputId = *((int*)(headerBufferAddress + 12).ToPointer());

                    receiveData = new byte[BufferSize];
                    GCHandle gc = GCHandle.Alloc(receiveData, GCHandleType.Pinned);
                    receiveDataHandler = GCHandle.ToIntPtr(gc);
                    receiveDataAddress = gc.AddrOfPinnedObject();

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

                int destid = (int)(receiveData.Length - (Size + length));

                if (inprogress)
                {
                    Extracting.Extract.CopyBlock(
                        receiveDataAddress,
                        destid,
                        headerBufferAddress,
                        offset,
                        length
                    );
                }
            }
            return noiseKind;
        }

        public unsafe MarkupType SyncMessage(int received)
        {
            MarkupType noiseKind = MarkupType.None;

            lock (receiveData)
            {
                int offset = 0,
                    length = received;
                bool inprogress = false;

                if (Size == 0)
                {
                    Size = *((int*)(messageBufferAddress + 4).ToPointer());
                    InputId = *((int*)(messageBufferAddress + 12).ToPointer());

                    receiveData = new byte[Size];
                    GCHandle gc = GCHandle.Alloc(receiveData, GCHandleType.Pinned);
                    receiveDataHandler = GCHandle.ToIntPtr(gc);
                    receiveDataAddress = gc.AddrOfPinnedObject();

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

                int destid = (int)(receiveData.Length - (Size + length));
                if (inprogress)
                {
                    Extracting.Extract.CopyBlock(
                        receiveDataAddress,
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
