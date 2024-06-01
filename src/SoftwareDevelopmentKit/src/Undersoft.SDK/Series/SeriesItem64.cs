namespace Undersoft.SDK.Series
{
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Uniques;
    using Base;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class SeriesItem64<V> : SeriesItemBase<V>
    {
        private long _key;

        public SeriesItem64() { }

        public SeriesItem64(ISeriesItem<V> value) : base(value) { }

        public SeriesItem64(object key, V value) : base(key, value) { }

        public SeriesItem64(long key, V value) : base(key, value) { }

        public SeriesItem64(V value) : base(value) { }

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
            this.value = item.Value;
            _key = item.Id;
        }

        public override void Set(object key, V value)
        {
            this.value = value;
            _key = key.UniqueKey64();
        }

        public override void Set(V value)
        {
            this.value = value;
            _key = value.UniqueKey64();
        }
    }
}
