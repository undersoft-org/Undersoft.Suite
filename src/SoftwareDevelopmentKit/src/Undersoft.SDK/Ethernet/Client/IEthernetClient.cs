using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Ethernet.Client
{
    public interface IEthernetClient : IDisposable
    {
        IInvoker Connected { get; set; }

        ITransitContext Context { get; set; }

        IInvoker HeaderReceived { get; set; }

        IInvoker HeaderSent { get; set; }

        IInvoker MessageReceived { get; set; }

        IInvoker MessageSent { get; set; }

        void Connect();

        bool IsConnected();

        void Receive(TransitPart messagePart);

        void Send(TransitPart messagePart);
    }
}
