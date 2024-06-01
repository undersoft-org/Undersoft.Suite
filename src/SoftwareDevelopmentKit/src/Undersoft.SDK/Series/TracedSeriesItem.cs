namespace Undersoft.SDK.Series
{
    using Base;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Uniques;
    using Undersoft.SDK.Updating;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class TracedSeriesItem<V> : SeriesItemBase<V> where V : class, ITracedSeries
    {
        private long _key;
        private Updater<V> _proxy;

        public TracedSeriesItem() { }

        public TracedSeriesItem(ISeriesItem<V> value) : base(value) { }

        public TracedSeriesItem(object key, V value) : base(key, value) { }

        public TracedSeriesItem(long key, V value) : base(key, value) { }

        public TracedSeriesItem(V value) : base(value) { }

        public override long Id
        {
            get { return _key; }
            set { _key = value; }
        }

        public override int CompareTo(ISeriesItem<V> other)
        {
            return (int)(Id - other.Id);
        }

        public override int CompareTo(object other)
        {
            return (int)(Id - other.UniqueKey64());
        }

        public override int CompareTo(long key)
        {
            return (int)(Id - key);
        }

        public override bool Equals(object y)
        {
            return Id.Equals(y.UniqueKey64());
        }

        public override bool Equals(long key)
        {
            return Id == key;
        }

        public override byte[] GetBytes()
        {
            return GetIdBytes();
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }

        public unsafe override byte[] GetIdBytes()
        {
            byte[] b = new byte[8];
            fixed (byte* s = b)
                *(long*)s = _key;
            return b;
        }

        public override void Set(ISeriesItem<V> item)
        {
            if (this.value == null)
            {
                _proxy = new Updater<V>(item.Value);
                value = item.Value;
            }
            else
            {
                value.PatchTo(_proxy);
            }

            _key = item.Id;
        }

        public override void Set(object key, V value)
        {
            if (this.value == null)
            {
                _proxy = new Updater<V>(value);
                this.value = value;
            }
            else
            {
                value.PatchTo(_proxy);
            }

            _key = key.UniqueKey64();
        }

        public override void Set(V value)
        {
            Set(value.UniqueKey64(), value);
        }

        public override V Value
        {
            get => base.Value;
            set => value.PatchTo(_proxy);
        }
    }
}
