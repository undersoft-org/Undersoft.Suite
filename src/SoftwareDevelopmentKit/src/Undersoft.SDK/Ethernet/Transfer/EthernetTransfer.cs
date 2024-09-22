namespace Undersoft.SDK.Ethernet.Transfer
{
    public class EthernetTransfer : IDisposable
    {
        public EthernetTransfer()
        {
            ResponseHeader = new TransferHeader(this);
            Manager = new TransferManager(this);
            ResponseMessage = new TransferMessage(this, DirectionType.Send, null);
        }

        public EthernetTransfer(object message = null, ITransferContext context = null)
        {
            Context = context;
            if (Context != null)
                ResponseHeader = new TransferHeader(this, Context);
            else
                ResponseHeader = new TransferHeader(this);

            Manager = new TransferManager(this);
            ResponseMessage = new TransferMessage(this, DirectionType.Send, message);
        }

        public ITransferContext Context { get; set; }

        public TransferManager Manager { get; set; }

        public TransferHeader RequestHeader { get; set; }

        public TransferMessage RequestMessage { get; set; }

        public TransferHeader ResponseHeader { get; set; }

        public TransferMessage ResponseMessage { get; set; }

        public void Dispose()
        {
            if (ResponseHeader != null)
                ResponseHeader.Dispose();
            if (ResponseMessage != null)
                ResponseMessage.Dispose();
            if (RequestHeader != null)
                RequestHeader.Dispose();
            if (RequestMessage != null)
                RequestMessage.Dispose();
            if (Context != null)
                Context.Dispose();
        }
    }
}
