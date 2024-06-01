using Undersoft.SDK.Series.Base;

namespace Undersoft.SDK.Series;

public class Listing<V> : ListingBase<V>
{
    public Listing() : base() { }

    public Listing(IEnumerable<V> collection, int capacity = 17, bool repeatable = false)
        : base(collection, capacity, repeatable) { }

    public Listing(bool repeatable = false, int capacity = 17) : base(repeatable, capacity) { }

    public Listing(IEnumerable<ISeriesItem<V>> items, int capacity = 17, bool repeatable = false) : base(items, capacity) { }

    public override ISeriesItem<V> EmptyItem()
    {
        return new SeriesItem<V>();
    }

    public override ISeriesItem<V>[] EmptyTable(int size)
    {
        return new SeriesItem<V>[size];
    }

    public override ISeriesItem<V>[] EmptyVector(int size)
    {
        return new SeriesItem<V>[size];
    }

    public override ISeriesItem<V> NewItem(ISeriesItem<V> item)
    {
        return new SeriesItem<V>(item);
    }

    public override ISeriesItem<V> NewItem(object key, V value)
    {
        return new SeriesItem<V>(key, value);
    }

    public override ISeriesItem<V> NewItem(long key, V value)
    {
        return new SeriesItem<V>(key, value);
    }

    public override ISeriesItem<V> NewItem(V value)
    {
        return new SeriesItem<V>(value);
    }
}