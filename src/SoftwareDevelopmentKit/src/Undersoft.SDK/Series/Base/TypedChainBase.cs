namespace Undersoft.SDK.Series.Base
{
    using System.Collections.Generic;
    using Undersoft.SDK;
    using Undersoft.SDK.Uniques;

    public abstract class TypedChainBase<V> : TypedSeriesBase<V> where V : IIdentifiable  
    {
        public TypedChainBase(
            IEnumerable<IUnique<V>> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : base(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedChainBase(
            IEnumerable<V> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : base(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedChainBase(
            IList<IUnique<V>> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : base(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedChainBase(IList<V> collection, int capacity = 17, HashBits bits = HashBits.bit64)
            : base(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedChainBase(int capacity = 17, HashBits bits = HashBits.bit64)
            : base(capacity, bits) { }

        public override ISeriesItem<V> GetItem(int index)
        {
            if (index < count)
            {
                if (removed > 0)
                    Reindex();

                int i = -1;
                int id = index;
                var item = first.Next;
                for (; ; )
                {
                    if (++i == id)
                        return item;
                    item = item.Next;
                }
            }
            return null;
        }

        protected ISeriesItem<V> createNew(ISeriesItem<V> value)
        {
            last.Next = value;
            last = value;
            return value;
        }

        protected ISeriesItem<V> createNew(long key, V value)
        {
            var newitem = NewItem(key, value);
            last.Next = newitem;
            last = newitem;
            return newitem;
        }

        protected override bool InnerAdd(ISeriesItem<V> value)
        {
            long key = value.Id;
            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                table[pos] = createNew(value);
                countIncrement();
                return true;
            }

            for (; ; )
            {
                if (item.Equals(key))
                {
                    if (item.Removed)
                    {
                        item.Removed = false;
                        item.Value = value.Value;
                        removedDecrement();
                        return true;
                    }
                    return false;
                }

                if (item.Extended == null)
                {
                    item.Extended = createNew(value);
                    conflictIncrement();
                    return true;
                }
                item = item.Extended;
            }
        }

        protected override bool InnerAdd(long key, V value)
        {
            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                table[pos] = createNew(key, value);
                countIncrement();
                return true;
            }

            for (; ; )
            {
                if (item.Equals(key))
                {
                    if (item.Removed)
                    {
                        item.Removed = false;
                        item.Value = value;
                        removedDecrement();
                        return true;
                    }
                    return false;
                }

                if (item.Extended == null)
                {
                    item.Extended = createNew(key, value);
                    conflictIncrement();
                    return true;
                }
                item = item.Extended;
            }
        }

        protected override bool InnerAdd(V value)
        {
            long key = unique.Key(value, value.TypeId);

            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                table[pos] = createNew(key, value);
                countIncrement();
                return true;
            }

            for (; ; )
            {
                if (item.Equals(key))
                {
                    if (item.Removed)
                    {
                        item.Removed = false;
                        item.Value = value;
                        removedDecrement();
                        return true;
                    }
                    return false;
                }

                if (item.Extended == null)
                {
                    item.Extended = createNew(key, value);
                    conflictIncrement();
                    return true;
                }
                item = item.Extended;
            }
        }

        protected override void InnerInsert(int index, ISeriesItem<V> item)
        {
            if (index < count - 1)
            {
                if (index == 0)
                {
                    item.Index = 0;
                    item.Next = first.Next;
                    first.Next = item;
                }
                else
                {
                    ISeriesItem<V> prev = GetItem(index - 1);
                    ISeriesItem<V> next = prev.Next;
                    prev.Next = item;
                    item.Next = next;
                    item.Index = index;
                }
            }
            else
            {
                last = last.Next = item;
            }
        }

        protected override ISeriesItem<V> InnerPut(ISeriesItem<V> value)
        {
            long key = value.Id;

            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                item = createNew(value);
                table[pos] = item;
                countIncrement();
                return item;
            }

            for (; ; )
            {
                if (item.Equals(key))
                {
                    if (item.Removed)
                    {
                        item.Removed = false;
                        removedDecrement();
                    }

                    item.Value = value.Value;
                    return item;
                }

                if (item.Extended == null)
                {
                    var newitem = createNew(value);
                    item.Extended = newitem;
                    conflictIncrement();
                    return newitem;
                }
                item = item.Extended;
            }
        }

        protected override ISeriesItem<V> InnerPut(long key, V value)
        {
            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                item = createNew(key, value);
                table[pos] = item;
                countIncrement();
                return item;
            }

            for (; ; )
            {
                if (item.Equals(key))
                {
                    if (item.Removed)
                    {
                        item.Removed = false;
                        removedDecrement();
                    }

                    item.Value = value;
                    return item;
                }

                if (item.Extended == null)
                {
                    var newitem = createNew(key, value);
                    item.Extended = newitem;
                    conflictIncrement();
                    return newitem;
                }
                item = item.Extended;
            }
        }

        protected override ISeriesItem<V> InnerPut(V value)
        {
            long key = unique.Key(value, value.TypeId);

            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                item = createNew(key, value);
                table[pos] = item;
                countIncrement();
                return item;
            }

            for (; ; )
            {
                if (item.Equals(key))
                {
                    if (item.Removed)
                    {
                        item.Removed = false;
                        removedDecrement();
                    }

                    item.Value = value;
                    return item;
                }

                if (item.Extended == null)
                {
                    var newitem = createNew(key, value);
                    item.Extended = newitem;
                    conflictIncrement();
                    return newitem;
                }
                item = item.Extended;
            }
        }

        protected virtual void Reindex()
        {
            ISeriesItem<V> _firstitem = EmptyItem();
            ISeriesItem<V> _lastitem = _firstitem;
            ISeriesItem<V> item = first.Next;
            do
            {
                if (!item.Removed)
                {
                    _lastitem = _lastitem.Next = item;
                }

                item = item.Next;
            } while (item != null);
            removed = 0;
            first = _firstitem;
            last = _lastitem;
        }
    }
}
