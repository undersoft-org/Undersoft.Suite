using System.Net;
using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Ethernet
{
    public interface IEthernetServer
    {
        void ClearClients();
        void Close();
        void Echo(string message);
        ITransitContext HeaderReceived(object inetdealcontext);
        ITransitContext HeaderSent(object inetdealcontext);
        bool IsActive();
        ITransitContext MessageReceived(object inetdealcontext);
        ITransitContext MessageSent(object inetdealcontext);
        void Start(IPEndPoint endPoint, IInvoker echoMethod = null);
    }
}