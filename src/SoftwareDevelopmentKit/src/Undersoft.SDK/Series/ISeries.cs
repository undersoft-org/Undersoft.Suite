namespace Undersoft.SDK.Series
{
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using Undersoft.SDK;
    using Uniques;

    public interface ISeries<V>
        : IEnumerable<V>,
            IEnumerable,
            ICollection,
            ICollection<V>,
            IList<V>,
            IProducerConsumerCollection<V>,
            IDisposable,
            IUnique,
            IFindable<V>,
            IIdentifiable
    {
        ISeriesItem<V> First { get; }
        ISeriesItem<V> Last { get; }

        bool IsRepeatable { get; }

        ISeriesItem<V> Next(ISeriesItem<V> item);

        new int Count { get; }
        int MinCount { get; set; }

        bool ContainsKey(long key);
        bool ContainsKey(object key);
        bool ContainsKey(IIdentifiable key);

        bool Contains(ISeriesItem<V> item);
        bool Contains(long key, V item);

        V Get(object key);
        V Get(long key);
        V Get(IIdentifiable key);

        bool TryGet(object key, out ISeriesItem<V> output);
        bool TryGet(object key, out V output);
        bool TryGet(long key, out V output);
        bool TryGet(IIdentifiable key, out ISeriesItem<V> output);

        new V this[int index] { get; set; }

        ISeriesItem<V> GetItem(int id);
        ISeriesItem<V> GetItem(object key);
        ISeriesItem<V> GetItem(long key);
        ISeriesItem<V> GetItem(IIdentifiable key);

        ISeriesItem<V> Set(object key, V value);
        ISeriesItem<V> Set(long key, V value);
        ISeriesItem<V> Set(IIdentifiable key, V value);
        ISeriesItem<V> Set(V value);
        ISeriesItem<V> Set(ISeriesItem<V> value);
        int Set(IEnumerable<V> values);
        int Set(IList<V> values);
        int Set(IEnumerable<ISeriesItem<V>> values);

        ISeriesItem<V> EnsureGet(object key, V value);
        ISeriesItem<V> EnsureGet(long key, V value);
        ISeriesItem<V> EnsureGet(IIdentifiable key, V value);

        ISeriesItem<V> New();
        ISeriesItem<V> New(long key);
        ISeriesItem<V> New(object key);

        bool Add(object key, V value);
        bool Add(long key, V value);
        void Add(ISeriesItem<V> item);
        void Add(IList<ISeriesItem<V>> itemList);
        void Add(IEnumerable<ISeriesItem<V>> items);
        void Add(IList<V> items);
        void Add(IEnumerable<V> items);

        bool TryAdd(object key, V value);

        bool Enqueue(object key, V value);
        void Enqueue(ISeriesItem<V> item);
        bool Enqueue(V item);

        V Dequeue();
        bool TryDequeue(out ISeriesItem<V> item);
        bool TryDequeue(out V item);
        new bool TryTake(out V item);

        bool TryPick(int skip, out V output);

        ISeriesItem<V> Put(object key, V value);
        ISeriesItem<V> Put(long key, V value);
        ISeriesItem<V> Put(ISeriesItem<V> item);
        void Put(IList<ISeriesItem<V>> itemList);
        void Put(IEnumerable<ISeriesItem<V>> items);
        void Put(IList<V> items);
        void Put(IEnumerable<V> items);
        ISeriesItem<V> Put(V value);

        V Remove(object key);
        bool Remove(object key, V item);
        bool Remove(ISeriesItem<V> item);
        bool TryRemove(object key);

        void Renew(IEnumerable<V> items);
        void Renew(IList<V> items);
        void Renew(IList<ISeriesItem<V>> items);
        void Renew(IEnumerable<ISeriesItem<V>> items);

        new V[] ToArray();

        IEnumerable<ISeriesItem<V>> AsItems();

        IEnumerable<V> AsValues();

        new void CopyTo(Array array, int arrayIndex);

        new bool IsSynchronized { get; set; }
        new object SyncRoot { get; set; }

        ISeriesItem<V> NewItem(V value);
        ISeriesItem<V> NewItem(object key, V value);
        ISeriesItem<V> NewItem(long key, V value);
        ISeriesItem<V> NewItem(ISeriesItem<V> item);

        void CopyTo(ISeriesItem<V>[] array, int destIndex);

        new void Clear();

        void Resize(int size);

        void Flush();
    }
}
