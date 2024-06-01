namespace Undersoft.SDK.Series
{
    using System.Collections.Generic;
    using Undersoft.SDK.Uniques;
    using Base;

    public class Registry64<V> : RegistryBase<V>
    {
        public Registry64(IEnumerable<V> collection, int capacity = 17) : this(capacity)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Registry64(IList<V> collection, int capacity = 17)
            : this(capacity > collection.Count ? capacity : collection.Count)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Registry64(int capacity = 17) : base(capacity, HashBits.bit64) { }

        public override ISeriesItem<V> EmptyItem()
        {
            return new SeriesItem64<V>();
        }

        public override ISeriesItem<V>[] EmptyTable(int size)
        {
            return new SeriesItem64<V>[size];
        }

        public override ISeriesItem<V>[] EmptyVector(int size)
        {
            return new SeriesItem64<V>[size];
        }

        public override ISeriesItem<V> NewItem(ISeriesItem<V> item)
        {
            return new SeriesItem64<V>(item);
        }

        public override ISeriesItem<V> NewItem(object key, V value)
        {
            return new SeriesItem64<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(long key, V value)
        {
            return new SeriesItem64<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(V value)
        {
            return new SeriesItem64<V>(value);
        }
    }
}
