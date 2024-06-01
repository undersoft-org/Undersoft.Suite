namespace Undersoft.SDK.Series
{
    using System.Collections.Generic;
    using Undersoft.SDK.Uniques;
    using Base;

    public class Catalog<V> : CatalogBase<V>
    {
        public Catalog(
            IEnumerable<ISeriesItem<V>> collection,
            int capacity = 9,
            HashBits bits = HashBits.bit64
        ) : this(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Catalog(IList<ISeriesItem<V>> collection, int capacity = 9, HashBits bits = HashBits.bit64)
            : this(capacity > collection.Count ? capacity : (int)(collection.Count * 1.5), bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Catalog(IEnumerable<V> collection, int capacity = 9, HashBits bits = HashBits.bit64)
         : this(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Catalog(int capacity = 9, HashBits bits = HashBits.bit64) : base(capacity, bits) { }

        public override ISeriesItem<V> EmptyItem()
        {
            return new SeriesItem64<V>();
        }

        public override ISeriesItem<V>[] EmptyTable(int size)
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
            return new SeriesItem64<V>(value, value);
        }
    }
}
