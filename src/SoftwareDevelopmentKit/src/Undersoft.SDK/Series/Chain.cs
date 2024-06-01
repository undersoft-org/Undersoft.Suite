using Undersoft.SDK.Series.Base;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Series;

public class Chain<V> : ChainBase<V>
{
    public Chain(
        IEnumerable<ISeriesItem<V>> collection,
        int capacity = 17,
        HashBits bits = HashBits.bit64
    ) : this(capacity, bits)
    {
        foreach (var c in collection)
            this.Add(c);
    }

    public Chain(IList<ISeriesItem<V>> collection, int capacity = 17, HashBits bits = HashBits.bit64)
        : this(capacity > collection.Count ? capacity : (int)(collection.Count * 1.5), bits)
    {
        foreach (var c in collection)
            this.Add(c);
    }

    public Chain(IEnumerable<V> collection, int capacity = 17, HashBits bits = HashBits.bit64)
     : this(capacity, bits)
    {
        foreach (var c in collection)
            this.Add(c);
    }

    public Chain(int capacity = 17, HashBits bits = HashBits.bit64) : base(capacity, bits) { }

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