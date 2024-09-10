namespace Undersoft.SDK.Ethernet
{
    public class EthernetOperation : IDisposable
    {
        public ITransitContext transitContext;
        private EthernetContext ethernetContext;
        private EthernetSite site;
        private EthernetTransit _transit;

        public EthernetOperation(EthernetTransit transit)
        {
            _transit = transit;
            transitContext = transit.Context;
            ethernetContext = transit.MyHeader.Context;
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
