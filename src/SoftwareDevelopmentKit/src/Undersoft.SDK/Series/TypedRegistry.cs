namespace Undersoft.SDK.Series
{
    using System.Collections.Generic;
    using Base;
    using Undersoft.SDK;
    using Uniques;

    public class TypedRegistry<V> : TypedRegistryBase<V> where V : IIdentifiable
    {
        public TypedRegistry(IEnumerable<V> collection, int capacity = 17, bool repeatable = false)
            : base(collection, capacity, repeatable) { }

        public TypedRegistry(IList<IUnique<V>> collection, int capacity = 17, bool repeatable = false)
            : base(collection, capacity, repeatable) { }

        public TypedRegistry(IList<V> collection, int capacity = 17, bool repeatable = false) : base(collection, capacity, repeatable) { }

        public TypedRegistry(bool repeatable = false, int capacity = 17) : base(repeatable, capacity) { }

        public override ISeriesItem<V> EmptyItem()
        {
            return new TypedSeriesItem<V>();
        }

        public override ISeriesItem<V>[] EmptyTable(int size)
        {
            return new TypedSeriesItem<V>[size];
        }

        public override ISeriesItem<V>[] EmptyVector(int size)
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
            return new TypedSeriesItem<V>(value);
        }
    }
}
