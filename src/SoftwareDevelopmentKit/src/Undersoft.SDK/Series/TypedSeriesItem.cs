namespace Undersoft.SDK.Series
{
    using Undersoft.SDK.Extracting;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Uniques;
    using Base;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class TypedSeriesItem<V> : SeriesItemBase<V> where V : IIdentifiable
    {
        private long _key;

        public TypedSeriesItem() : base() { }

        public TypedSeriesItem(ISeriesItem<V> value) : base(value) { }

        public TypedSeriesItem(object key, V value) : base(key, value) { }

        public TypedSeriesItem(ulong key, V value) : base(key, value) { }

        public TypedSeriesItem(V value) : base(value) { }

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
            return (int)(Id - other.UniqueKey64(value.TypeId));
        }

        public override int CompareTo(long key)
        {
            return (int)(Id - key);
        }

        public override bool Equals(object y)
        {
            return Id.Equals(y.UniqueKey64(TypeId));
        }

        public override bool Equals(long key)
        {
            return Id == key;
        }

        public override byte[] GetBytes()
        {
            return this.value.GetBytes();
        }

        public override int GetHashCode()
        {
            return (int)Id.UniqueKey32();
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
            value = item.Value;
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
            _key = value.Id.UniqueKey64(value.TypeId);
        }
    }
}
