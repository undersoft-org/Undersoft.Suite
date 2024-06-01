namespace Undersoft.SDK.Series
{
    using Undersoft.SDK.Uniques;
    using Base;

    public class Catalog32<V> : CatalogBase<V>
    {
        public Catalog32(int _catalogSize = 9, HashBits bits = HashBits.bit64) : base(_catalogSize, bits)
        { }

        public override ISeriesItem<V> EmptyItem()
        {
            return new SeriesItem32<V>();
        }

        public override ISeriesItem<V>[] EmptyTable(int size)
        {
            return new SeriesItem32<V>[size];
        }

        public override ISeriesItem<V> NewItem(ISeriesItem<V> item)
        {
            return new SeriesItem32<V>(item);
        }

        public override ISeriesItem<V> NewItem(object key, V value)
        {
            return new SeriesItem32<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(long key, V value)
        {
            return new SeriesItem32<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(V value)
        {
            return new SeriesItem32<V>(value);
        }
    }
}
