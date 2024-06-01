namespace Undersoft.SDK.Rubrics
{
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Uniques;
    using Undersoft.SDK.Series.Base;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class RubricItem : SeriesItemBase<MemberRubric>
    {
        private long _key;

        public RubricItem() { }

        public RubricItem(ISeriesItem<MemberRubric> value) : base(value) { }

        public RubricItem(MemberRubric value) : base(value) { }

        public RubricItem(object key, MemberRubric value) : base(key, value) { }

        public RubricItem(ulong key, MemberRubric value) : base(key, value) { }

        public override long Id
        {
            get { return _key; }
            set { _key = value; }
        }

        public override int CompareTo(ISeriesItem<MemberRubric> other)
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

        public override void Set(ISeriesItem<MemberRubric> item)
        {
            value = item.Value;
            _key = item.Id;
        }

        public override void Set(MemberRubric value)
        {
            this.value = value;
            _key = value.Id;
        }

        public override void Set(object key, MemberRubric value)
        {
            this.value = value;
            _key = key.UniqueKey64();
        }
    }
}
