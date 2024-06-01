namespace Undersoft.SDK.Series
{
    using System.Collections.Generic;
    using Undersoft.SDK.Uniques;
    using Base;

    public class Catalog64<V> : CatalogBase<V>
    {
        public Catalog64(IEnumerable<ISeriesItem<V>> collection, int capacity = 9) : this(capacity)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Catalog64(IList<ISeriesItem<V>> collection, int capacity = 9)
            : this(capacity > collection.Count ? capacity : collection.Count)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public Catalog64(int capacity = 9) : base(capacity, HashBits.bit64) { }

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
            return new SeriesItem64<V>(value);
        }
    }
}
