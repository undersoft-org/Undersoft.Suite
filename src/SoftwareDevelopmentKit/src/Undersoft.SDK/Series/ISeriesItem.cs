using System.Collections.Generic;

namespace Undersoft.SDK.Series
{
    using Uniques;

    public interface ISeriesItem<V>
        : IComparable<long>,
            IComparable<ISeriesItem<V>>,
            IEquatable<ISeriesItem<V>>,
            IEquatable<object>,
            IEquatable<long>,
            IComparable<object>,
            IDisposable,
            IIdentifiable,
            IEnumerable<V>
    {
        bool Repeated { get; set; }

        ISeriesItem<V> Extended { get; set; }

        int Index { get; set; }

        ISeriesItem<V> Next { get; set; }

        bool Removed { get; set; }

        V Value { get; set; }

        int GetHashCode();

        Type GetUniqueType();

        void Set(ISeriesItem<V> item);

        void Set(object key, V value);

        void Set(long key, V value);

        void Set(V value);

        ISeriesItem<V> MoveNext(ISeriesItem<V> item);

        IEnumerable<ISeriesItem<V>> AsItems();

        IEnumerable<V> AsValues();
    }
}
