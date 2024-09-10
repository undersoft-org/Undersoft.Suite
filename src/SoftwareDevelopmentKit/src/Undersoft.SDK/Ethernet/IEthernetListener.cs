namespace Undersoft.SDK.Ethernet
{
    using System;
    using Undersoft.SDK.Invoking;

    public interface IEthernetListener : IDisposable
    {
        IInvoker HeaderReceived { get; set; }

        IInvoker HeaderSent { get; set; }

        IInvoker MessageReceived { get; set; }

        IInvoker MessageSent { get; set; }

        IInvoker WriteEcho { get; set; }

        void ClearClients();

        void CloseClient(int id);

        void CloseListener();

        void Echo(string message);

        void HeaderReceivedCallback(IAsyncResult result);

        void HeaderSentCallback(IAsyncResult result);

        bool IsConnected(int id);

        void MessageReceivedCallback(IAsyncResult result);

        void MessageSentCallback(IAsyncResult result);

        void OnConnectCallback(IAsyncResult result);

        void Receive(TransitPart messagePart, int id);

        void Send(TransitPart messagePart, int id);

        void StartListening();
    }
}
