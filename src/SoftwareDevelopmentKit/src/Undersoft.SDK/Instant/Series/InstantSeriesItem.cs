namespace Undersoft.SDK.Instant.Series
{
    using SDK.Extracting;
    using SDK.Series;
    using SDK.Series.Base;
    using SDK.Uniques;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Rubrics;

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class InstantSeriesItem : SeriesItemBase<IInstant>, IInstant, IEquatable<IInstant>, IComparable<IInstant>
    {
        public InstantSeriesItem()
        {
        }

        public InstantSeriesItem(object key, IInstant value) : base(key, value)
        {
        }

        public InstantSeriesItem(ulong key, IInstant value) : base(key, value)
        {
        }

        public InstantSeriesItem(IInstant value) : base(value)
        {
        }

        public InstantSeriesItem(ISeriesItem<IInstant> value) : base(value)
        {
        }

        public object this[int fieldId]
        {
            get => value[fieldId];
            set => this.value[fieldId] = value;
        }
        public object this[string propertyName]
        {
            get => value[propertyName];
            set => this.value[propertyName] = value;
        }

        public virtual IRubrics Changes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override void Set(object key, IInstant value)
        {
            this.value = value;
            this.value.Id = key.UniqueKey();
        }

        public override void Set(IInstant value)
        {
            this.value = value;

        }

        public override void Set(ISeriesItem<IInstant> item)
        {
            this.value = item.Value;
        }

        public override bool Equals(long key)
        {
            return Id == key;
        }

        public override bool Equals(object y)
        {
            return Id.Equals(y.UniqueKey());
        }

        public bool Equals(IInstant other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Value.Id.GetBytes().BitAggregate64to32().ToInt32();
        }

        public override int CompareTo(object other)
        {
            return (int)(Id - other.UniqueKey64());
        }

        public override int CompareTo(long key)
        {
            return (int)(Id - key);
        }

        public override int CompareTo(ISeriesItem<IInstant> other)
        {
            return (int)(Id - other.Id);
        }

        public int CompareTo(IInstant other)
        {
            return (int)(Id - other.Id);
        }

        public override long Id
        {
            get => value.Id;
            set => this.value.Id = value;
        }

        public Uscn Code
        {
            get => value.Code;
            set => this.value.Code = value;
        }

        public IInstantSeries InstantSeries { get; set; }

        public override byte[] GetBytes()
        {
            return value.GetBytes();
        }

        public override byte[] GetIdBytes()
        {
            return value.Id.GetBytes();
        }
    }
}
