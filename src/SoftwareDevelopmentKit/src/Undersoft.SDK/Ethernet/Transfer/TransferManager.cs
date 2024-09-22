using System.Linq;

namespace Undersoft.SDK.Ethernet.Transfer
{
    public class TransferManager
    {
        private EthernetContext ethernetContext;
        private EthernetSite site;
        private EthernetTransfer transit;
        private ITransferContext transitContext;
        private EthernetOperation operation;

        public TransferManager(EthernetTransfer _transit)
        {
            transit = _transit;
            transitContext = transit.Context;
            ethernetContext = transit.ResponseHeader.Context;
            site = ethernetContext.IdentitySite;
            operation = new EthernetOperation(_transit);
        }

        public void HeaderContent(object data, object value, DirectionType _direction)
        {
            DirectionType direction = _direction;
            object _data = value;
            if (_data != null)
            {
                Type[] ifaces = _data.GetType().GetInterfaces();
                if (ifaces.Contains(typeof(ITransferable)))
                {
                    transit.ResponseHeader.Context.ContentType = _data.GetType();

                    if (direction == DirectionType.Send)
                        _data = ((ITransferable)value).GetHeader();

                    object[] messages_ = null;
                    if (operation.Execute(_data, direction, out messages_))
                    {
                        if (messages_.Length > 0)
                        {
                            ethernetContext.ItemsCount = messages_.Length;
                            for (int i = 0; i < ethernetContext.ItemsCount; i++)
                            {
                                ITransferable message = ((ITransferable[])messages_)[i];
                                ITransferable head = (ITransferable)
                                    ((ITransferable[])messages_)[i].GetHeader();
                                message.OutputChunks = message.ItemsCount;
                                head.OutputChunks = message.ItemsCount;
                            }

                            if (direction == DirectionType.Send)
                                transit.ResponseMessage.Data = messages_;
                            else
                                transit.ResponseMessage.Data = (
                                    (ITransferable)_data
                                ).GetHeader();
                        }
                    }
                }
            }
            data = _data;
        }

        public void MessageContent(ref object content, object value, DirectionType _direction)
        {
            DirectionType direction = _direction;
            object _content = value;
            if (_content != null)
            {
                if (direction == DirectionType.Receive)
                {
                    Type[] ifaces = _content.GetType().GetInterfaces();
                    if (ifaces.Contains(typeof(ITransferable)))
                    {
                        object[] messages_ = ((ITransferable)value).GetMessage();
                        if (messages_ != null)
                        {
                            int length = messages_.Length;
                            for (int i = 0; i < length; i++)
                            {
                                ITransferable message = ((ITransferable[])messages_)[i];
                                ITransferable head = (ITransferable)
                                    ((ITransferable[])messages_)[i].GetHeader();
                                message.OutputChunks = head.OutputChunks;
                                message.InputChunks = head.InputChunks;
                            }

                            _content = messages_;
                        }
                    }
                }
            }
            content = _content;
        }
    }
}
