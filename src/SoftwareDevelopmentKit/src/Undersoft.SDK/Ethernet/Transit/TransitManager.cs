using System;
using System.Linq;

namespace Undersoft.SDK.Ethernet
{
    public class TransitManager
    {
        private EthernetContext ethernetContext;
        private EthernetSite site;
        private EthernetTransit transit;
        private ITransitContext transitContext;
        private EthernetOperation operation;

        public TransitManager(EthernetTransit _transit)
        {
            transit = _transit;
            transitContext = transit.Context;
            ethernetContext = transit.MyHeader.Context;
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
                if (ifaces.Contains(typeof(ITransitable)))
                {
                    transit.MyHeader.Context.ContentType = _data.GetType();

                    if (direction == DirectionType.Send)
                        _data = ((ITransitable)value).GetHeader();

                    object[] messages_ = null;
                    if (operation.Execute(_data, direction, out messages_))
                    {
                        if (messages_.Length > 0)
                        {
                            ethernetContext.ItemsCount = messages_.Length;
                            for (int i = 0; i < ethernetContext.ItemsCount; i++)
                            {
                                ITransitable message = ((ITransitable[])messages_)[i];
                                ITransitable head = (ITransitable)
                                    ((ITransitable[])messages_)[i].GetHeader();
                                message.OutputChunks = message.ItemsCount;
                                head.OutputChunks = message.ItemsCount;
                            }

                            if (direction == DirectionType.Send)
                                transit.MyMessage.Data = messages_;
                            else
                                transit.MyMessage.Data = (
                                    (ITransitable)_data
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
                    if (ifaces.Contains(typeof(ITransitable)))
                    {
                        object[] messages_ = ((ITransitable)value).GetMessage();
                        if (messages_ != null)
                        {
                            int length = messages_.Length;
                            for (int i = 0; i < length; i++)
                            {
                                ITransitable message = ((ITransitable[])messages_)[i];
                                ITransitable head = (ITransitable)
                                    ((ITransitable[])messages_)[i].GetHeader();
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
