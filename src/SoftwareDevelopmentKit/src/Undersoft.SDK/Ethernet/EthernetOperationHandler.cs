

using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Ethernet
{
    public enum StateFlag : ushort
    {
        Synced = 1,
        Edited = 2,
        Added = 4,
        Quered = 8,
        Saved = 16,
        Canceled = 32
    }

    public class EthernetOperationHandler : IDisposable
    {
        [NonSerialized]
        public ITransitContext transferContext;
        private ITransitable _data;
        private EthernetContext dealContext;
        private DirectionType direction;
        private EthernetSite site;
        private ushort state;

        public EthernetOperationHandler(object data)
        {
            site = EthernetSite.Server;
            direction = DirectionType.None;
            state = ((IUniqueStructure)data).Flags;
            _data = (ITransitable)data;
        }

        public EthernetOperationHandler(object data, DirectionType directionType, EthernetTransit transfer)
            : this(data)
        {
            direction = directionType;
            transferContext = transfer.Context;
            dealContext = transfer.MyHeader.Context;
        }

        public void Dispose() { }

        public void Handle(out object[] messages)
        {
            messages = null;
            switch (site)
            {
                case EthernetSite.Server:
                    switch (direction)
                    {
                        case DirectionType.Receive:

                            break;
                        case DirectionType.Send:
                            switch (state & (int)StateFlag.Synced)
                            {
                                case 0:
                                    SrvSendSync(out messages);
                                    break;
                            }
                            break;
                        case DirectionType.None:
                            switch (state & (int)StateFlag.Synced)
                            {
                                case 0:
                                    SrvSendSync(out messages);
                                    break;
                            }
                            break;
                    }
                    break;
                case EthernetSite.Client:
                    switch (direction)
                    {
                        case DirectionType.Receive:

                            break;
                        case DirectionType.Send:
                            switch (state & (int)StateFlag.Synced)
                            {
                                case 0:
                                    CltSendSync(out messages);
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        private void CltSendSync(out object[] messages)
        {
            if (direction != DirectionType.None)
                if (
                    ((state & (int)StateFlag.Edited) > 0)
                    || ((state & (int)StateFlag.Saved) > 0)
                    || ((state & (int)StateFlag.Quered) > 0)
                    || ((state & (int)StateFlag.Canceled) > 0)
                )
                {
                    transferContext.Synchronic = true;
                    dealContext.Synchronic = true;
                }

            messages = _data.GetMessage();
        }

        private void SrvSendSync(out object[] messages)
        {
            if (direction != DirectionType.None)
                if (
                    ((state & (int)StateFlag.Edited) > 0)
                    || ((state & (int)StateFlag.Saved) > 0)
                    || ((state & (int)StateFlag.Quered) > 0)
                    || ((state & (int)StateFlag.Canceled) > 0)
                )
                {
                    transferContext.Synchronic = true;
                    dealContext.Synchronic = true;
                }

                messages = _data.GetMessage();            
        }
    }
}
