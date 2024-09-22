using Undersoft.SDK.Ethernet.Transfer;

namespace Undersoft.SDK.Ethernet
{
    public class EthernetOperation : IDisposable
    {
        public ITransferContext transitContext;
        private EthernetContext ethernetContext;
        private EthernetSite site;
        private EthernetTransfer _transit;

        public EthernetOperation(EthernetTransfer transit)
        {
            _transit = transit;
            transitContext = transit.Context;
            ethernetContext = transit.ResponseHeader.Context;
            site = ethernetContext.IdentitySite;
        }

        public bool Execute(object data, DirectionType direction, out object[] messages)
        {
            messages = null;

            EthernetOperationHandler operation = new EthernetOperationHandler(data, direction, _transit);
            operation.Handle(out messages);

            if (messages != null)
                return true;
            else
                return false;
        }

        public void Dispose()
        {
            if (transitContext != null)
                transitContext.Dispose();
        }
    }
}
