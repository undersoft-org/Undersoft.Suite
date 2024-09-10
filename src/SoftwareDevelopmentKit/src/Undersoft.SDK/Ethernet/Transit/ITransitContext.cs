using System.Collections;
using System.Net.Sockets;
using System.Text;
using Undersoft.SDK.Series;

namespace Undersoft.SDK.Ethernet
{
    public interface ITransitContext : ITransitBuffer
    {
        ManualResetEvent ChunksReceivedNotice { get; set; }

        int BufferSize { get; }

        bool Close { get; set; }

        bool Denied { get; set; }

        string Echo { get; }

        byte[] HeaderBuffer { get; }

        ManualResetEvent HeaderReceivedNotice { get; set; }

        ManualResetEvent HeaderSentNotice { get; set; }

        int Id { get; set; }

        Socket Listener { get; set; }

        byte[] MessageBuffer { get; }

        ManualResetEvent MessageReceivedNotice { get; set; }

        ManualResetEvent MessageSentNotice { get; set; }

        ProtocolMethod Method { get; set; }

        int ItemIndex { get; set; }

        int ItemsLeft { get; set; }

        EthernetProtocol Protocol { get; set; }

        bool HasMessageToReceive { get; set; }

        bool HasMessageToSend { get; set; }

        bool Synchronic { get; set; }

        EthernetTransit Transfer { get; set; }

        MarkupType IncomingHeader(int received);

        MarkupType IncomingMessage(int received);

        void Reset();
    }
}
