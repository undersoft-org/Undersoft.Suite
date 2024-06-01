namespace Undersoft.SDK.Series
{
    using System.Collections.Generic;
    using Undersoft.SDK.Uniques;
    using Base;
    using Undersoft.SDK;

    public class TypedCatalog<V> : TypedCatalogBase<V> where V : IOrigin
    {
        public TypedCatalog(
            IEnumerable<ISeriesItem<V>> collection,
            int capacity = 9,
            HashBits bits = HashBits.bit64
        ) : this(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedCatalog(
            IList<ISeriesItem<V>> collection,
            int capacity = 9,
            HashBits bits = HashBits.bit64
        ) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedCatalog(int capacity = 9, HashBits bits = HashBits.bit64) : base(capacity, bits)
        { }

        public override ISeriesItem<V> EmptyItem()
        {
            return new TypedSeriesItem<V>();
        }

        public override ISeriesItem<V>[] EmptyTable(int size)
        {
            return new TypedSeriesItem<V>[size];
        }

        public override ISeriesItem<V> NewItem(ISeriesItem<V> item)
        {
            return new TypedSeriesItem<V>(item);
        }

        public override ISeriesItem<V> NewItem(object key, V value)
        {
            return new TypedSeriesItem<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(long key, V value)
        {
            return new TypedSeriesItem<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(V value)
        {
            return new TypedSeriesItem<V>(value, value);
        }
    }
}
