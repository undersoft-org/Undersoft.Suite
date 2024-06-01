namespace Undersoft.SDK.Series
{
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Uniques;
    using Base;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class SeriesItem32<V> : SeriesItemBase<V>
    {
        private int _key;

        public SeriesItem32() { }

        public SeriesItem32(ISeriesItem<V> value) : base(value) { }

        public SeriesItem32(object key, V value) : base(key, value) { }

        public SeriesItem32(long key, V value) : base(key, value) { }

        public SeriesItem32(V value) : base(value) { }

        public override long Id
        {
            get { return _key; }
            set { _key = (int)value; }
        }

        public override int CompareTo(ISeriesItem<V> other)
        {
            return (int)(Id - other.Id);
        }

        public override int CompareTo(object other)
        {
            return (int)(_key - other.UniqueKey32());
        }

        public override int CompareTo(long key)
        {
            return (int)(Id - key);
        }

        public override bool Equals(object y)
        {
            return _key.Equals(y.UniqueKey32());
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
            return (int)_key;
        }

        public unsafe override byte[] GetIdBytes()
        {
            byte[] b = new byte[4];
            fixed (byte* s = b)
                *(int*)s = _key;
            return b;
        }

        public override void Set(ISeriesItem<V> item)
        {
            this.value = item.Value;
            _key = (int)item.Id;
        }

        public override void Set(object key, V value)
        {
            this.value = value;
            _key = key.UniqueKey32();
        }

        public override void Set(V value)
        {
            this.value = value;
            _key = value.UniqueKey32();
        }
    }
}
