namespace Undersoft.SDK.Series
{
    using Undersoft.SDK;
    using Uniques;

    public interface ITypedSeries<V> : ISeries<V> where V : IIdentifiable
    {
        V this[object key, long seed] { get; set; }
        V this[IIdentifiable key, long seed] { get; set; }

        bool ContainsKey(object key, long seed);

        bool Contains(V item, long seed);

        V Get(object key, long seed);

        bool TryGet(object key, long seed, out ISeriesItem<V> output);
        bool TryGet(object key, long seed, out V output);

        ISeriesItem<V> GetItem(object key, long seed);

        ISeriesItem<V> New(object key, long seed);

        bool Add(object key, long seed, V value);
        bool Add(V value, long seed);
        void Add(IList<V> items, long seed);
        void Add(IEnumerable<V> items, long seed);

        bool Enqueue(object key, long seed, V value);
        bool Enqueue(V item, long seed);

        ISeriesItem<V> Put(object key, long seed, V value);
        ISeriesItem<V> Put(object key, long seed, object value);
        void Put(IList<V> items, long seed);
        void Put(IEnumerable<V> items, long seed);
        ISeriesItem<V> Put(V value, long seed);

        V Remove(object key, long seed);
        bool TryRemove(object key, long seed);

        ISeriesItem<V> NewItem(V value, long seed);
        ISeriesItem<V> NewItem(object key, long seed, V value);
        ISeriesItem<V> NewItem(long key, long seed, V value);
    }
}
