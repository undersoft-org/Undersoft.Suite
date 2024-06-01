namespace Undersoft.SDK.Instant.Math.Set
{
    using System.Runtime.InteropServices;
    using SDK.Series;
    using SDK.Uniques;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class MathSetItem : SeriesItem<MathSetFormula>
    {
        private long _key;

        public MathSetItem() { }

        public MathSetItem(ISeriesItem<MathSetFormula> value) : base(value) { }

        public MathSetItem(long key, MathSetFormula value) : base(key, value) { }

        public MathSetItem(MathSetFormula value) : base(value) { }

        public MathSetItem(object key, MathSetFormula value) : base(key.UniqueKey64(), value) { }

        public override long Id
        {
            get { return _key; }
            set { _key = value; }
        }

        public override int CompareTo(ISeriesItem<MathSetFormula> other)
        {
            return (int)(_key - other.Id);
        }

        public override int CompareTo(object other)
        {
            return (int)(_key - other.UniqueKey64());
        }

        public override int CompareTo(long key)
        {
            return (int)(_key - key);
        }

        public override bool Equals(object y)
        {
            return _key.Equals(y.UniqueKey64());
        }

        public override bool Equals(long key)
        {
            return _key == key;
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
            byte[] b = new byte[8];
            fixed (byte* s = b)
                *(long*)s = _key;
            return b;
        }

        public override void Set(ISeriesItem<MathSetFormula> item)
        {
            value = item.Value;
            _key = item.Id;
        }

        public override void Set(MathSetFormula value)
        {
            this.value = value;
            _key = value.Id;
        }

        public override void Set(object key, MathSetFormula value)
        {
            this.value = value;
            _key = key.UniqueKey64();
        }
    }
}
