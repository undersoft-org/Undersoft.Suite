using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Workflows
{
    using Extracting;
    using Series;
    using Uniques;

    public class WorkMethod : SeriesItem<IInvoker>
    {
        private long key;

        public WorkMethod() { }

        public WorkMethod(IInvoker value)
        {
            Value = value;
        }

        public WorkMethod(long key, IInvoker value) : base(key, value) { }

        public WorkMethod(object key, IInvoker value) : base(key.UniqueKey64(), value) { }

        public override long Id
        {
            get => key;
            set => key = value;
        }

        public override int CompareTo(object other)
        {
            return (int)(Id - other.UniqueKey());
        }

        public override bool Equals(object y)
        {
            return Id == y.UniqueKey64();
        }

        public override byte[] GetBytes()
        {
            return Id.GetBytes();
        }

        public override int GetHashCode()
        {
            return Id.GetBytes().BitAggregate64to32().ToInt32();
        }

        public override byte[] GetIdBytes()
        {
            return Id.GetBytes();
        }

        public override void Set(ISeriesItem<IInvoker> item)
        {
            Id = item.Id;
            Value = item.Value;
            Removed = false;
        }

        public override void Set(IInvoker value)
        {
            Value = value;
            Removed = false;
        }

        public override void Set(object key, IInvoker value)
        {
            Id = key.UniqueKey64();
            Value = value;
            Removed = false;
        }
    }
}
