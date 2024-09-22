using System.Net;
using System.Threading;
using Undersoft.SDK.Ethernet.Transfer;
using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Ethernet
{
    public class EthernetServer : IEthernetServer
    {
        private IEthernetListener server;

        public void ClearClients()
        {
            Echo("Client registry cleaned");
            if (server != null)
                server.ClearClients();
        }

        public void Close()
        {
            if (server != null)
            {
                Echo("Server instance shutdown ");
                server.CloseListener();
                server = null;
            }
            else
            {
                Echo("Server instance doesn't exist ");
            }
        }

        public ITransferContext HeaderReceived(object inetdealcontext)
        {
            string clientEcho = ((ITransferContext)inetdealcontext)
                .Transfer
                .RequestHeader
                .Context
                .Echo;
            Echo(string.Format("Client header received"));
            if (clientEcho != null && clientEcho != "")
                Echo(string.Format("Client echo: {0}", clientEcho));

            EthernetContext trctx = ((ITransferContext)inetdealcontext).Transfer.ResponseHeader.Context;
            if (trctx.Echo == null || trctx.Echo == "")
                trctx.Echo = "Server say Hello";
            if (!((ITransferContext)inetdealcontext).Synchronic)
                server.Send(TransitPart.Header, ((ITransferContext)inetdealcontext).Id);
            else
                server.Receive(TransitPart.Message, ((ITransferContext)inetdealcontext).Id);

            return ((ITransferContext)inetdealcontext);
        }

        public ITransferContext HeaderSent(object inetdealcontext)
        {
            Echo("Server header sent");

            ITransferContext context = (ITransferContext)inetdealcontext;
            if (context.Close)
            {
                context.Transfer.Dispose();
                server.CloseClient(context.Id);
            }
            else
            {
                if (!context.Synchronic)
                {
                    if (context.HasMessageToReceive)
                        server.Receive(TransitPart.Message, context.Id);
                }
                if (context.HasMessageToSend)
                    server.Send(TransitPart.Message, context.Id);
            }
            return context;
        }

        public bool IsActive()
        {
            if (server != null)
            {
                Echo("Server Instance Is Active");
                return true;
            }
            else
            {
                Echo("Server Instance Doesn't Exist");
                return false;
            }
        }

        public ITransferContext MessageReceived(object inetdealcontext)
        {
            Echo(string.Format("Client message received"));
            if (((ITransferContext)inetdealcontext).Synchronic)
                server.Send(TransitPart.Header, ((ITransferContext)inetdealcontext).Id);
            return (ITransferContext)inetdealcontext;
        }

        public ITransferContext MessageSent(object inetdealcontext)
        {
            Echo("Server message sent");
            ITransferContext result = (ITransferContext)inetdealcontext;
            if (result.Close)
            {
                result.Transfer.Dispose();
                server.CloseClient(result.Id);
            }
            return result;
        }

        public void Start(IPEndPoint endPoint,  IInvoker echoMethod = null
        )
        {
            server = new EthernetListener(endPoint);

            server.HeaderSent = new EthernetMethod(nameof(this.HeaderSent), this);
            server.MessageSent = new EthernetMethod(nameof(this.MessageSent), this);
            server.HeaderReceived = new EthernetMethod(nameof(this.HeaderReceived), this);
            server.MessageReceived = new EthernetMethod(nameof(this.MessageReceived), this);
            server.WriteEcho = echoMethod;

            new Thread(new ThreadStart(server.StartListening)).Start();

            Echo("Server instance started");
        }

        public void Echo(string message)
        {
            if (server != null)
                server.Echo(message);
        }
    }
}
