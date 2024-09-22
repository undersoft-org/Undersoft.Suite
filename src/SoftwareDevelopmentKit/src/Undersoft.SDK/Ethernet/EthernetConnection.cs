namespace Undersoft.SDK.Ethernet.Connection
{
    using System.Net;
    using System.Threading;
    using Undersoft.SDK.Ethernet.Client;
    using Undersoft.SDK.Ethernet.Transfer;
    using Undersoft.SDK.Invoking;

    public interface IEthernetConnection
    {
        object Content { get; set; }

        void Close();

        ITransferContext Initiate(bool isAsync = true);

        void Reconnect();

        void SetCallback(IInvoker OnCompleteEvent);

        void SetCallback(string methodName, object classObject);
    }

    public class EthernetConnection : IEthernetConnection
    {
        private readonly ManualResetEvent completeNotice = new ManualResetEvent(false);
        public IInvoker CompleteMethod;
        public IInvoker EchoMethod;
        private IInvoker connected;
        private IInvoker headerReceived;
        private IInvoker headerSent;
        private IInvoker messageReceived;
        private IInvoker messageSent;
        private bool isAsync = true;

        public EthernetConnection(IPEndPoint endPoint,
            IInvoker OnCompleteEvent = null,
            IInvoker OnEchoEvent = null
        )
        {
            EthernetClient client = new EthernetClient(endPoint);
            Transit = new EthernetTransfer();

            connected = new EthernetMethod(nameof(this.Connected), this);
            headerSent = new EthernetMethod(nameof(this.HeaderSent), this);
            messageSent = new EthernetMethod(nameof(this.MessageSent), this);
            headerReceived = new EthernetMethod(nameof(this.HeaderReceived), this);
            messageReceived = new EthernetMethod(nameof(this.MessageReceived), this);

            client.Connected = connected;
            client.HeaderSent = headerSent;
            client.MessageSent = messageSent;
            client.HeaderReceived = headerReceived;
            client.MessageReceived = messageReceived;

            CompleteMethod = OnCompleteEvent;
            EchoMethod = OnEchoEvent;

            Client = client;

            WriteEcho("Client Connection Created");
        }

        public object Content
        {
            get { return Transit.ResponseHeader.Data; }
            set { Transit.ResponseHeader.Data = value; }
        }

        public ITransferContext Context { get; set; }

        public EthernetTransfer Transit { get; set; }

        private EthernetClient Client { get; set; }

        public void Close()
        {
            Client.Dispose();
        }

        public ITransferContext Connected(object inetdealclient)
        {
            WriteEcho("Client Connection Established");
            Transit.ResponseHeader.Context.Echo = "Client say Hello. ";
            Context = Client.Context;
            Client.Context.Transfer = Transit;

            IEthernetClient idc = (IEthernetClient)inetdealclient;

            idc.Send(TransitPart.Header);

            return idc.Context;
        }

        public ITransferContext HeaderReceived(object inetdealclient)
        {
            string serverEcho = Transit.RequestHeader.Context.Echo;
            WriteEcho(string.Format("Server header received"));
            if (serverEcho != null && serverEcho != "")
                WriteEcho(string.Format("Server echo: {0}", serverEcho));

            IEthernetClient idc = (IEthernetClient)inetdealclient;

            if (idc.Context.Close)
                idc.Dispose();
            else
            {
                if (!idc.Context.Synchronic)
                {
                    if (idc.Context.HasMessageToSend)
                        idc.Send(TransitPart.Message);
                }

                if (idc.Context.HasMessageToReceive)
                    idc.Receive(TransitPart.Message);
            }

            if (!idc.Context.HasMessageToReceive && !idc.Context.HasMessageToSend)
            {
                if (CompleteMethod != null)
                    CompleteMethod.Invoke(idc.Context);
                if (!isAsync)
                    completeNotice.Set();
            }

            return idc.Context;
        }

        public ITransferContext HeaderSent(object inetdealclient)
        {
            WriteEcho("Client header sent");
            IEthernetClient idc = (IEthernetClient)inetdealclient;
            if (!idc.Context.Synchronic)
                idc.Receive(TransitPart.Header);
            else
                idc.Send(TransitPart.Message);

            return idc.Context;
        }

        public ITransferContext Initiate(bool IsAsync = true)
        {
            isAsync = IsAsync;
            Client.Connect();
            if (!isAsync)
            {
                completeNotice.WaitOne();
                return Context;
            }

            return null;
        }

        public ITransferContext MessageReceived(object inetdealclient)
        {
            WriteEcho(string.Format("Server message received"));

            ITransferContext context = ((IEthernetClient)inetdealclient).Context;
            if (context.Close)
                ((IEthernetClient)inetdealclient).Dispose();

            if (CompleteMethod != null)
                CompleteMethod.Invoke(context);
            if (!isAsync)
                completeNotice.Set();
            return context;
        }

        public ITransferContext MessageSent(object inetdealclient)
        {
            WriteEcho("Client message sent");

            IEthernetClient idc = (IEthernetClient)inetdealclient;
            if (idc.Context.Synchronic)
                idc.Receive(TransitPart.Header);

            if (!idc.Context.HasMessageToReceive)
            {
                if (CompleteMethod != null)
                    CompleteMethod.Invoke(idc.Context);
                if (!isAsync)
                    completeNotice.Set();
            }

            return idc.Context;
        }

        public void Reconnect()
        {
            IPEndPoint endpoint = new IPEndPoint(Client.EndPoint.Address, Client.EndPoint.Port);
            Transit.Dispose();
            EthernetClient client = new EthernetClient(endpoint);
            Transit = new EthernetTransfer(endpoint);
            client.Connected = connected;
            client.HeaderSent = headerSent;
            client.MessageSent = messageSent;
            client.HeaderReceived = headerReceived;
            client.MessageReceived = messageReceived;
            Client = client;
        }

        public void SetCallback(IInvoker OnCompleteEvent)
        {
            CompleteMethod = OnCompleteEvent;
        }

        public void SetCallback(string methodName, object classObject)
        {
            CompleteMethod = new EthernetMethod(methodName, classObject);
        }

        private void WriteEcho(string message)
        {
            if (EchoMethod != null)
                EchoMethod.Invoke(message);
        }
    }
}
