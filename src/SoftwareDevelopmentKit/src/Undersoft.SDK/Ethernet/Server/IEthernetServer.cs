using System.Net;
using Undersoft.SDK.Ethernet.Transfer;
using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Ethernet
{
    public interface IEthernetServer
    {
        void ClearClients();
        void Close();
        void Echo(string message);
        ITransferContext HeaderReceived(object inetdealcontext);
        ITransferContext HeaderSent(object inetdealcontext);
        bool IsActive();
        ITransferContext MessageReceived(object inetdealcontext);
        ITransferContext MessageSent(object inetdealcontext);
        void Start(IPEndPoint endPoint, IInvoker echoMethod = null);
    }
}