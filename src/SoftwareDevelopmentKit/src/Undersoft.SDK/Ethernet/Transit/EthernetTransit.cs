namespace Undersoft.SDK.Ethernet
{
    public class EthernetTransit : IDisposable
    {
        public ITransitContext Context;
        public TransitHeader HeaderReceived;
        public TransitManager Manager;
        public TransitMessage MessageReceived;
        public TransitHeader MyHeader;
        private TransitMessage mymessage;

        public EthernetTransit()
        {
            MyHeader = new TransitHeader(this);
            Manager = new TransitManager(this);
            MyMessage = new TransitMessage(this, DirectionType.Send, null);
        }

        public EthernetTransit(
            object message = null,
            ITransitContext context = null
        )
        {
            Context = context;
            if (Context != null)
                MyHeader = new TransitHeader(this, Context);
            else
                MyHeader = new TransitHeader(this);

            Manager = new TransitManager(this);
            MyMessage = new TransitMessage(this, DirectionType.Send, message);
        }

        public TransitMessage MyMessage
        {
            get { return mymessage; }
            set { mymessage = value; }
        }

        public void Dispose()
        {
            if (MyHeader != null)
                MyHeader.Dispose();
            if (mymessage != null)
                mymessage.Dispose();
            if (HeaderReceived != null)
                HeaderReceived.Dispose();
            if (MessageReceived != null)
                MessageReceived.Dispose();
            if (Context != null)
                Context.Dispose();
        }
    }
}
