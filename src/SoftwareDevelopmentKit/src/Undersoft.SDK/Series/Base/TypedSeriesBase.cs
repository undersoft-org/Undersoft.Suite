namespace Undersoft.SDK.Series.Base
{
    using Enumerators;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Undersoft.SDK;
    using Undersoft.SDK.Extracting;
    using Undersoft.SDK.Uniques;

    public abstract class TypedSeriesBase<V> : Identifiable, IIdentifiable, ISet<V>, IAsyncDisposable, IList, IListSource, ITypedSeries<V> where V : IIdentifiable
    {
        internal const float RESIZING_VECTOR = 2.333f;
        internal const float CONFLICTS_PERCENT_LIMIT = 0.22f;
        internal const float REMOVED_PERCENT_LIMIT = 0.45f;
        internal const ulong MAX_BIT_MASK = 0xFFFFFFFFFFFFFFFF;

        protected IUniqueKey unique = Unique.Bit64;
        protected ISeriesItem<V> first,
            last;
        protected ISeriesItem<V>[] table;
        protected int count,
            conflicts,
            removed,
            minSize,
            size,
            mincount,
            msbid;
        protected uint maxid;
        protected ulong bitmask;

        protected int nextSize()
        {
            return (((int)(size * RESIZING_VECTOR)) ^ 3);
        }

        protected int previousSize()
        {
            return (int)(size * (1 - REMOVED_PERCENT_LIMIT)) ^ 3;
        }

        protected void countIncrement()
        {
            if ((++count + 7) > size)
                Rehash(nextSize());
        }

        protected void conflictIncrement()
        {
            countIncrement();
            if (++conflicts > (size * CONFLICTS_PERCENT_LIMIT))
                Rehash(nextSize());
        }

        protected void removedIncrement()
        {
            --count;
            if (++removed > ((size * REMOVED_PERCENT_LIMIT) - 1))
            {
                if (size < (size * 0.5))
                    Rehash(previousSize());
                else
                    Rehash(size);
            }
        }

        protected void removedDecrement()
        {
            ++count;
            --removed;
        }

        public TypedSeriesBase(int capacity = 17, HashBits bits = HashBits.bit64)
        {
            if (bits != HashBits.bit64)
                unique = Unique.Bit32;

            size = capacity;
            minSize = capacity;
            maxid = (uint)(size - 1);
            table = EmptyTable(capacity);
            first = EmptyItem();
            last = first;
            ValueEquals = getValueComparer();
            Id = typeof(V).UniqueKey64();
        }

        public TypedSeriesBase(
            IList<ISeriesItem<V>> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            this.Add(collection);
        }

        public TypedSeriesBase(
            IList<V> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedSeriesBase(
            IEnumerable<ISeriesItem<V>> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : this(capacity, bits)
        {
            this.Add(collection);
        }

        public TypedSeriesBase(
            IEnumerable<V> collection,
            int capacity = 17,
            HashBits bits = HashBits.bit64
        ) : this(capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public virtual ISeriesItem<V> First
        {
            get { return first; }
        }
        public virtual ISeriesItem<V> Last
        {
            get { return last; }
        }

        public virtual int Size
        {
            get => size;
        }
        public virtual int Count
        {
            get => count;
        }
        public virtual int MinCount
        {
            get => mincount;
            set => mincount = value;
        }
        public virtual bool IsReadOnly { get; set; }
        public virtual bool IsSynchronized { get; set; }
        public virtual bool IsRepeatable
        {
            get => false;
        }
        public virtual object SyncRoot { get; set; }
        public virtual Func<V, V, bool> ValueEquals { get; }

        public virtual V this[int index]
        {
            get => GetItem(index).Value;
            set => GetItem(index).Value = value;
        }
        public V this[long hashkey]
        {
            get => InnerGet(hashkey);
            set => InnerSet(hashkey, value);
        }
        public virtual V this[object key]
        {
            get
            {
                if (key is IIdentifiable)
                {
                    IIdentifiable ukey = (IIdentifiable)key;
                    return InnerGet(unique.Key(ukey, ukey.TypeId));
                }
                else
                    throw new NotSupportedException();
            }
            set
            {
                if (key is IIdentifiable)
                {
                    IIdentifiable ukey = (IIdentifiable)key;
                    InnerSet(unique.Key(ukey, ukey.TypeId), value);
                }
                else
                    throw new NotSupportedException();
            }
        }
        object IFindable.this[object key]
        {
            get
            {
                if (key is IIdentifiable)
                {
                    IIdentifiable ukey = (IIdentifiable)key;
                    return InnerGet(unique.Key(ukey, ukey.TypeId));
                }
                else
                    throw new NotSupportedException();
            }
            set
            {
                if (key is IIdentifiable)
                {
                    IIdentifiable ukey = (IIdentifiable)key;
                    InnerPut(unique.Key(ukey, ukey.TypeId), (V)value);
                }
                else
                    throw new NotSupportedException();
            }
        }
        public virtual V this[IIdentifiable key]
        {
            get => InnerGet(unique.Key(key, key.TypeId));
            set => InnerSet(unique.Key(key, key.TypeId), value);
        }
        public virtual V this[IUnique<V> key]
        {
            get => InnerGet(unique.Key(key, key.TypeId));
            set => InnerSet(unique.Key(key, key.TypeId), value);
        }
        public virtual V this[object key, long seed]
        {
            get => InnerGet(unique.Key(key, seed));
            set => InnerSet(unique.Key(key, seed), value);
        }
        public virtual V this[IIdentifiable key, long seed]
        {
            get => InnerGet(unique.Key(key, seed));
            set => InnerSet(unique.Key(key, seed), value);
        }
        public virtual V this[IUnique<V> key, long seed]
        {
            get => InnerGet(unique.Key(key, seed));
            set => InnerSet(unique.Key(key, seed), value);
        }

        protected virtual V InnerGet(long key)
        {
            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                {
                    if (!mem.Removed)
                        return mem.Value;
                    return default(V);
                }
                mem = mem.Extended;
            }

            return default(V);
        }

        public virtual V Get(long key)
        {
            return InnerGet(key);
        }

        public virtual V Get(object key)
        {
            if (key is IUnique)
            {
                IUnique ukey = (IUnique)key;
                return InnerGet(unique.Key(ukey.Id, ukey.TypeId));
            }
            else
                throw new NotSupportedException();
        }

        public virtual V Get(object key, long seed)
        {
            return InnerGet(unique.Key(key, seed));
        }

        public virtual V Get(IIdentifiable key)
        {
            return InnerGet(unique.Key(key, key.TypeId));
        }

        public virtual V Get(IUnique<V> key)
        {
            return InnerGet(unique.Key(key, key.TypeId));
        }

        protected virtual bool InnerTryGet(long key, out ISeriesItem<V> output)
        {
            output = null;

            ISeriesItem<V> mem = table[getPosition(key)];
            while (mem != null)
            {
                if (mem.Equals(key))
                {
                    if (!mem.Removed)
                    {
                        output = mem;
                        return true;
                    }
                    return false;
                }
                mem = mem.Extended;
            }
            return false;
        }

        public virtual bool TryGet(long key, out ISeriesItem<V> output)
        {
            return InnerTryGet(key, out output);
        }

        public virtual bool TryGet(object key, out ISeriesItem<V> output)
        {
            if (key is IIdentifiable)
            {
                IUnique ukey = (IUnique)key;
                return InnerTryGet(unique.Key(ukey, ukey.TypeId), out output);
            }
            else
                throw new NotSupportedException();
        }

        public virtual bool TryGet(object key, out V output)
        {
            if (key is IIdentifiable)
            {
                output = default(V);
                ISeriesItem<V> item = null;
                IUnique ukey = (IUnique)key;
                if (InnerTryGet(unique.Key(ukey, ukey.TypeId), out item))
                {
                    output = item.Value;
                    return true;
                }
                return false;
            }
            else
                throw new NotSupportedException();
        }

        public virtual bool TryGet(object key, long seed, out ISeriesItem<V> output)
        {
            return InnerTryGet(unique.Key(key, seed), out output);
        }

        public virtual bool TryGet(object key, long seed, out V output)
        {
            output = default(V);
            ISeriesItem<V> item = null;
            if (InnerTryGet(unique.Key(key, seed), out item))
            {
                output = item.Value;
                return true;
            }
            return false;
        }

        public virtual bool TryGet(long key, out V output)
        {
            if (InnerTryGet(key, out ISeriesItem<V> item))
            {
                output = item.Value;
                return true;
            }
            output = default(V);
            return false;
        }

        public bool TryGet(IIdentifiable key, out ISeriesItem<V> output)
        {
            return InnerTryGet(unique.Key(key, key.TypeId), out output);
        }

        public bool TryGet(IUnique<V> key, out ISeriesItem<V> output)
        {
            return InnerTryGet(unique.Key(key, key.TypeId), out output);
        }

        protected virtual ISeriesItem<V> InnerGetItem(long key)
        {
            if (key == 0)
                return null;

            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                {
                    if (!mem.Removed)
                        return mem;
                    return null;
                }
                mem = mem.Extended;
            }

            return null;
        }

        public virtual ISeriesItem<V> GetItem(long key)
        {
            return InnerGetItem(key);
        }

        public ISeriesItem<V> GetItem(IIdentifiable key)
        {
            return InnerGetItem(unique.Key(key, key.TypeId));
        }

        public ISeriesItem<V> GetItem(IUnique<V> key)
        {
            return InnerGetItem(unique.Key(key, key.TypeId));
        }

        protected virtual ISeriesItem<V> InnerSet(long key, V value)
        {
            var item = InnerGetItem(key);
            if (item != null)
                item.Value = value;
            return item;
        }

        protected virtual ISeriesItem<V> InnerSet(ISeriesItem<V> value)
        {
            var item = GetItem(value);
            if (item != null)
                item.Value = value.Value;
            return item;
        }

        public ISeriesItem<V> Set(object key, V value)
        {
            return InnerSet(unique.Key(key, (long)value.TypeId), value);
        }

        public ISeriesItem<V> Set(long key, V value)
        {
            return InnerSet(key, value);
        }

        public ISeriesItem<V> Set(IIdentifiable key, V value)
        {
            return InnerSet(unique.Key(key, key.TypeId), value);
        }

        public ISeriesItem<V> Set(IUnique<V> key, V value)
        {
            return InnerSet(unique.Key(key, key.TypeId), value);
        }

        public ISeriesItem<V> Set(V value)
        {
            return InnerSet(unique.Key(value, value.TypeId), value);
        }

        public ISeriesItem<V> Set(IUnique<V> value)
        {
            return InnerSet(unique.Key(value, value.TypeId), value.UniqueValue);
        }

        public ISeriesItem<V> Set(ISeriesItem<V> value)
        {
            return InnerSet(value);
        }

        public int Set(IEnumerable<V> values)
        {
            int count = 0;
            foreach (var value in values)
            {
                if (Set(value) != null)
                    count++;
            }

            return count;
        }

        public int Set(IList<V> values)
        {
            int count = 0;
            foreach (var value in values)
            {
                if (Set(value) != null)
                    count++;
            }

            return count;
        }

        public int Set(IEnumerable<ISeriesItem<V>> values)
        {
            int count = 0;
            foreach (var value in values)
            {
                if (InnerSet(value) != null)
                    count++;
            }

            return count;
        }

        public int Set(IEnumerable<IUnique<V>> values)
        {
            int count = 0;
            foreach (var value in values)
            {
                if (Set(value) != null)
                    count++;
            }

            return count;
        }

        public virtual ISeriesItem<V> EnsureGet(object key, V value)
        {
            if (!TryGet(key, out ISeriesItem<V> item))
                return Put(key, value);
            return item;
        }
        public virtual ISeriesItem<V> EnsureGet(long key, V value)
        {
            if (!TryGet(key, out ISeriesItem<V> item))
                return Put(key, value);
            return item;
        }
        public virtual ISeriesItem<V> EnsureGet(IIdentifiable key, V value)
        {
            if (!TryGet(key, out ISeriesItem<V> item))
                return Put(key, value);
            return item;
        }

        public virtual ISeriesItem<V> GetItem(object key)
        {
            if (key is IUnique)
            {
                IUnique ukey = (IUnique)key;
                return InnerGetItem(unique.Key(ukey, ukey.TypeId));
            }
            else
                throw new NotSupportedException();
        }

        public virtual ISeriesItem<V> GetItem(object key, long seed)
        {
            return InnerGetItem(unique.Key(key, seed));
        }

        public abstract ISeriesItem<V> GetItem(int index);

        protected virtual ISeriesItem<V> InnerPut(long key, long seed, V value)
        {
            value.TypeId = (long)seed;
            value.Id = (long)key;
            return InnerPut(value);
        }

        protected abstract ISeriesItem<V> InnerPut(long key, V value);

        protected virtual ISeriesItem<V> InnerPut(V value, long seed)
        {
            value.TypeId = (long)seed;
            return InnerPut(value);
        }

        protected abstract ISeriesItem<V> InnerPut(V value);
        protected abstract ISeriesItem<V> InnerPut(ISeriesItem<V> value);

        public virtual ISeriesItem<V> Put(long key, object value)
        {
            return InnerPut(key, (V)value);
        }

        public virtual ISeriesItem<V> Put(long key, V value)
        {
            return InnerPut(key, value);
        }

        public virtual ISeriesItem<V> Put(object key, V value)
        {
            return InnerPut(unique.Key(key, (long)value.TypeId), value);
        }

        public virtual ISeriesItem<V> Put(object key, long seed, V value)
        {
            return InnerPut(unique.Key(key, seed), value);
        }

        public virtual ISeriesItem<V> Put(object key, long seed, object value)
        {
            if (value is V)
            {
                V o = (V)value;
                return InnerPut(unique.Key(key, seed), (V)value);
            }
            return null;
        }

        public virtual ISeriesItem<V> Put(ISeriesItem<V> item)
        {
            return InnerPut(item);
        }

        public virtual void Put(IList<ISeriesItem<V>> items)
        {
            int i = 0,
                c = items.Count;
            while (i < c)
                InnerPut(items[i++]);
        }

        public virtual void Put(IEnumerable<ISeriesItem<V>> items)
        {
            foreach (ISeriesItem<V> item in items)
                InnerPut(item);
        }

        public virtual ISeriesItem<V> Put(V value)
        {
            return InnerPut(value);
        }

        public virtual void Put(IList<V> items)
        {
            int i = 0,
                c = items.Count;
            while (i < c)
                InnerPut(items[i++]);
        }

        public virtual void Put(IEnumerable<V> items)
        {
            foreach (V item in items)
                Put(item);
        }

        public virtual ISeriesItem<V> Put(V value, long seed)
        {
            return InnerPut(value, seed);
        }

        public virtual void Put(object value, long seed)
        {
            if (value is IUnique)
            {
                IUnique v = (IUnique)value;
                Put(v, seed);
            }
            else if (value is V)
                Put((V)value, seed);
        }

        public virtual void Put(IList<V> items, long seed)
        {
            int c = items.Count;
            for (int i = 0; i < c; i++)
            {
                InnerPut(items[i], seed);
            }
        }

        public virtual void Put(IEnumerable<V> items, long seed)
        {
            foreach (V item in items)
                InnerPut(item, seed);
        }

        public virtual ISeriesItem<V> Put(IUnique<V> value)
        {
            return InnerPut(unique.Key(value, value.TypeId), value.UniqueValue);
        }

        public virtual void Put(IList<IUnique<V>> value)
        {
            foreach (IUnique<V> item in value)
            {
                Put(item);
            }
        }

        public virtual void Put(IEnumerable<IUnique<V>> value)
        {
            foreach (IUnique<V> item in value)
            {
                Put(item);
            }
        }

        protected virtual bool InnerAdd(long key, long seed, V value)
        {
            value.TypeId = (long)seed;
            value.Id = (long)key;
            return InnerAdd(value);
        }

        protected abstract bool InnerAdd(long key, V value);

        protected virtual bool InnerAdd(V value, long seed)
        {
            value.TypeId = (long)seed;
            return InnerAdd(value);
        }

        protected abstract bool InnerAdd(V value);
        protected abstract bool InnerAdd(ISeriesItem<V> value);

        public virtual bool Add(long key, object value)
        {
            V o = (V)value;
            return InnerAdd(key, (long)o.TypeId, o);
        }

        public virtual bool Add(long key, V value)
        {
            return InnerAdd(key, value);
        }

        public virtual bool Add(object key, V value)
        {
            return InnerAdd(unique.Key(key, (long)value.TypeId), value);
        }

        public virtual bool Add(object key, long seed, V value)
        {
            value.TypeId = (long)seed;
            return InnerAdd(unique.Key(key, seed), value);
        }

        public virtual void Add(ISeriesItem<V> item)
        {
            InnerAdd(item);
        }

        public virtual void Add(IList<ISeriesItem<V>> itemList)
        {
            int c = itemList.Count;
            for (int i = 0; i < c; i++)
            {
                InnerAdd(itemList[i]);
            }
        }

        public virtual void Add(IEnumerable<ISeriesItem<V>> itemTable)
        {
            foreach (ISeriesItem<V> item in itemTable)
                Add(item);
        }

        public virtual void Add(V value)
        {
            InnerAdd(value);
        }

        bool ISet<V>.Add(V value)
        {
            return InnerAdd(value);
        }

        public virtual void Add(IList<V> items)
        {
            int c = items.Count;
            for (int i = 0; i < c; i++)
            {
                Add(items[i]);
            }
        }

        public virtual void Add(IEnumerable<V> items)
        {
            foreach (V item in items)
                Add(item);
        }

        public virtual bool Add(V value, long seed)
        {
            return InnerAdd(value, seed);
        }

        public virtual void Add(IList<V> items, long seed)
        {
            int c = items.Count;
            for (int i = 0; i < c; i++)
            {
                Add(items[i], seed);
            }
        }

        public virtual void Add(IEnumerable<V> items, long seed)
        {
            foreach (V item in items)
                Add(item, seed);
        }

        public virtual void Add(IUnique<V> value)
        {
            InnerAdd(unique.Key(value, value.TypeId), value.UniqueValue);
        }

        public virtual void Add(IList<IUnique<V>> value)
        {
            foreach (IUnique<V> item in value)
            {
                Add(item);
            }
        }

        public virtual void Add(IEnumerable<IUnique<V>> value)
        {
            foreach (IUnique<V> item in value)
            {
                Add(item);
            }
        }

        public virtual bool TryAdd(V value)
        {
            return InnerAdd(value);
        }

        public virtual bool TryAdd(V value, long seed)
        {
            return InnerAdd(value, seed);
        }

        public virtual bool TryAdd(object key, V value)
        {
            return Add(key, value);
        }

        public virtual bool TryAdd(object key, long seed, V value)
        {
            return Add(key, seed, value);
        }

        public virtual ISeriesItem<V> New()
        {
            ISeriesItem<V> newItem = NewItem(Unique.NewId, default(V));
            if (InnerAdd(newItem))
                return newItem;
            return null;
        }

        public virtual ISeriesItem<V> New(long key)
        {
            ISeriesItem<V> newItem = NewItem(key, default(V));
            if (InnerAdd(newItem))
                return newItem;
            return null;
        }

        public virtual ISeriesItem<V> New(object key)
        {
            if (key is IUnique)
            {
                IUnique ukey = (IUnique)key;
                long _key = unique.Key(ukey, ukey.TypeId);
                ISeriesItem<V> newItem = NewItem(_key, default(V));
                if (InnerAdd(newItem))
                    return newItem;
                return null;
            }
            else
                throw new NotSupportedException();
        }

        public virtual ISeriesItem<V> New(object key, long seed)
        {
            ISeriesItem<V> newItem = NewItem(unique.Key(key, seed), default(V));
            if (InnerAdd(newItem))
                return newItem;
            return null;
        }

        protected abstract void InnerInsert(int index, ISeriesItem<V> item);

        public virtual void Insert(int index, ISeriesItem<V> item)
        {
            long key = item.Id;
            ulong pos = getPosition(key);

            ISeriesItem<V> _item = table[pos];

            if (_item == null)
            {
                _item = NewItem(item);
                table[pos] = _item;
                InnerInsert(index, _item);
                countIncrement();
                return;
            }

            for (; ; )
            {
                if (_item.Equals(key))
                {
                    if (_item.Removed)
                    {
                        var newitem = NewItem(item);
                        _item.Extended = newitem;
                        InnerInsert(index, newitem);
                        conflictIncrement();
                        return;
                    }
                    throw new Exception("SeriesItem exist");
                }

                if (_item.Extended == null)
                {
                    var newitem = NewItem(item);
                    _item.Extended = newitem;
                    InnerInsert(index, newitem);
                    conflictIncrement();
                    return;
                }
                item = item.Extended;
            }
        }

        public virtual void Insert(int index, V item)
        {
            Insert(index, NewItem(item));
        }

        public virtual bool Enqueue(V value)
        {
            return InnerAdd(value);
        }

        public virtual bool Enqueue(object key, V value)
        {
            return Add(key, value);
        }

        public virtual bool Enqueue(V value, long seed)
        {
            return InnerAdd(value, seed);
        }

        public virtual bool Enqueue(object key, long seed, V value)
        {
            return Add(key, seed, value);
        }

        public virtual void Enqueue(ISeriesItem<V> item)
        {
            InnerAdd(item);
        }

        public virtual bool TryPick(int skip, out V output)
        {
            output = default(V);
            bool check = false;
            if (check = TryPick(skip, out ISeriesItem<V> _output))
                output = _output.Value;
            return check;
        }

        public virtual bool TryPick(int skip, out ISeriesItem<V> output)
        {
            output = this.AsItems().Skip(skip).FirstOrDefault();
            if (output != null)
            {
                return true;
            }
            return false;
        }

        public virtual V Dequeue()
        {
            var item = Next(first);
            if (item != null)
            {
                item.Removed = true;
                removedIncrement();
                first = item;
                return item.Value;
            }
            return default(V);
        }

        public virtual bool TryDequeue(out V output)
        {
            output = default(V);
            if (count < mincount)
                return false;

            var item = Next(first);
            if (item != null)
            {
                item.Removed = true;
                removedIncrement();
                first = item;
                output = item.Value;
                return true;
            }
            return false;
        }

        public virtual bool TryDequeue(out ISeriesItem<V> output)
        {
            output = null;
            if (count < mincount)
                return false;

            output = Next(first);
            if (output != null)
            {
                output.Removed = true;
                removedIncrement();
                first = output;
                return true;
            }
            return false;
        }

        public virtual bool TryTake(out V output)
        {
            return TryDequeue(out output);
        }

        protected virtual void renewClear(int capacity)
        {
            if (capacity != size || count > 0)
            {
                size = capacity;
                maxid = (uint)(capacity - 1);
                conflicts = 0;
                removed = 0;
                count = 0;
                table = EmptyTable(size);
                first = EmptyItem();
                last = first;
            }
        }

        public virtual void Renew(IEnumerable<V> items)
        {
            renewClear(minSize);
            Put(items);
        }

        public virtual void Renew(IList<V> items)
        {
            int capacity = items.Count;
            capacity += (int)(capacity * CONFLICTS_PERCENT_LIMIT);
            renewClear(capacity);
            Put(items);
        }

        public virtual void Renew(IList<ISeriesItem<V>> items)
        {
            int capacity = items.Count;
            capacity += (int)(capacity * CONFLICTS_PERCENT_LIMIT);
            renewClear(capacity);
            Put(items);
        }

        public virtual void Renew(IEnumerable<ISeriesItem<V>> items)
        {
            renewClear(minSize);
            Put(items);
        }

        protected bool InnerContainsKey(long key)
        {
            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (!mem.Removed && mem.Equals(key))
                {
                    return true;
                }
                mem = mem.Extended;
            }

            return false;
        }

        public virtual bool ContainsKey(object key)
        {
            if (key is IUnique)
            {
                IUnique ukey = (IUnique)key;
                long _key = unique.Key(ukey, ukey.TypeId);
                return InnerContainsKey(_key);
            }
            else
                throw new NotSupportedException();
        }

        public virtual bool ContainsKey(object key, long seed)
        {
            return InnerContainsKey(unique.Key(key, seed));
        }

        public virtual bool ContainsKey(long key)
        {
            return InnerContainsKey(key);
        }

        public virtual bool ContainsKey(IIdentifiable key)
        {
            return InnerContainsKey(unique.Key(key, key.TypeId));
        }

        public virtual bool Contains(ISeriesItem<V> item)
        {
            return InnerContainsKey(item.Id);
        }

        public virtual bool Contains(IUnique<V> item)
        {
            return InnerContainsKey(unique.Key(item, item.TypeId));
        }

        public virtual bool Contains(V item)
        {
            return InnerContainsKey(unique.Key(item, (long)item.TypeId));
        }

        public virtual bool Contains(V item, long seed)
        {
            return InnerContainsKey(unique.Key(item, seed));
        }

        public virtual bool Contains(long key, V item)
        {
            return InnerContainsKey(key);
        }

        protected virtual Func<V, V, bool> getValueComparer()
        {
            if (typeof(V).IsValueType)
                return (o1, o2) => o1.Equals(o2);
            return (o1, o2) => ReferenceEquals(o1, o2);
        }

        protected virtual V InnerRemove(long key)
        {
            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                {
                    if (mem.Removed)
                        return default(V);

                    mem.Removed = true;
                    removedIncrement();
                    return mem.Value;
                }

                mem = mem.Extended;
            }
            return default(V);
        }

        protected virtual V InnerRemove(long key, V item)
        {
            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                {
                    if (mem.Removed)
                        return default(V);

                    if (ValueEquals(mem.Value, item))
                    {
                        mem.Removed = true;
                        removedIncrement();
                        return mem.Value;
                    }
                    return default(V);
                }
                mem = mem.Extended;
            }
            return default(V);
        }

        public virtual bool Remove(V item)
        {
            return InnerRemove(unique.Key(item, (long)item.TypeId)).Equals(default(V)) ? false : true;
        }

        public virtual V Remove(object key)
        {
            if (key is IUnique)
            {
                IUnique ukey = (IUnique)key;
                long _key = unique.Key(ukey, ukey.TypeId);
                return InnerRemove(_key);
            }
            else
                throw new NotSupportedException();
        }

        public virtual V Remove(object key, long seed)
        {
            return InnerRemove(unique.Key(key, seed));
        }

        public virtual bool Remove(ISeriesItem<V> item)
        {
            return InnerRemove(item.Id).Equals(default(V)) ? false : true;
        }

        public virtual bool Remove(IUnique<V> item)
        {
            return TryRemove(unique.Key(item, item.TypeId));
        }

        public virtual bool TryRemove(object key)
        {
            if (key is IUnique)
            {
                IUnique ukey = (IUnique)key;
                long _key = unique.Key(ukey, ukey.TypeId);
                V result = InnerRemove(unique.Key(key));
                if (result != null && !result.Equals(default(V)))
                    return true;
                return false;
            }
            else
                throw new NotSupportedException();
        }

        public virtual bool TryRemove(object key, long seed)
        {
            return InnerRemove(unique.Key(key, seed)).Equals(default(V)) ? false : true;
        }

        public virtual void RemoveAt(int index)
        {
            InnerRemove(GetItem(index).Id);
        }

        public virtual bool Remove(object key, V item)
        {
            return InnerRemove(unique.Key(key), item).Equals(default(V)) ? false : true;
        }

        public virtual void Clear()
        {
            size = minSize;
            maxid = (uint)(size - 1);
            conflicts = 0;
            removed = 0;
            count = 0;
            table = EmptyTable(size);
            first = EmptyItem();
            last = first;
        }

        public virtual void Flush()
        {
            conflicts = 0;
            removed = 0;
            count = 0;
            table = null;
            table = EmptyTable(size);
            first = EmptyItem();
            last = first;
        }

        public virtual void CopyTo(ISeriesItem<V>[] array, int index)
        {
            int c = count,
                i = index,
                l = array.Length;
            if (l - i < c)
            {
                c = l - i;
                foreach (ISeriesItem<V> ves in this.AsItems().Take(c))
                    array[i++] = ves;
            }
            else
                foreach (ISeriesItem<V> ves in this)
                    array[i++] = ves;
        }

        public virtual void CopyTo(Array array, int index)
        {
            int c = count,
                i = index,
                l = array.Length;
            if (l - i < c)
            {
                c = l - i;
                foreach (V ves in this.AsValues().Take(c))
                    array.SetValue(ves, i++);
            }
            else
                foreach (V ves in this.AsValues())
                    array.SetValue(ves, i++);
        }

        public virtual void CopyTo(V[] array, int index)
        {
            int c = count,
                i = index,
                l = array.Length;
            if (l - i < c)
            {
                c = l - i;
                foreach (V ves in this.AsValues().Take(c))
                    array[i++] = ves;
            }
            else
                foreach (V ves in this.AsValues())
                    array[i++] = ves;
        }

        public virtual V[] ToArray()
        {
            return this.AsValues().ToArray();
        }

        public virtual object[] ToObjectArray()
        {
            return this.Select((x) => (object)x).ToArray();
        }

        public virtual ISeriesItem<V> Next(ISeriesItem<V> item)
        {
            ISeriesItem<V> _item = item.Next;
            if (_item != null)
            {
                if (!_item.Removed)
                    return _item;
                return Next(_item);
            }
            return null;
        }

        public virtual void Resize(int size)
        {
            Rehash(size);
        }

        public abstract ISeriesItem<V> EmptyItem();

        public virtual ISeriesItem<V> NewItem(long key, long seed, V value)
        {
            value.TypeId = (long)seed;
            value.Id = (long)key;
            return NewItem(value);
        }

        public abstract ISeriesItem<V> NewItem(long key, V value);
        public abstract ISeriesItem<V> NewItem(object key, V value);

        public virtual ISeriesItem<V> NewItem(object key, long seed, V value)
        {
            value.TypeId = (long)seed;
            return NewItem(unique.Key(key, seed), value);
        }

        public abstract ISeriesItem<V> NewItem(ISeriesItem<V> item);

        public virtual ISeriesItem<V> NewItem(V item, long seed)
        {
            item.TypeId = (long)seed;
            return NewItem(item);
        }

        public abstract ISeriesItem<V> NewItem(V item);

        public abstract ISeriesItem<V>[] EmptyTable(int size);

        public virtual int IndexOf(ISeriesItem<V> item)
        {
            return GetItem(item).Index;
        }

        public virtual int IndexOf(V item)
        {
            return GetItem(item).Index;
        }

        protected virtual int IndexOf(long key, V value)
        {
            var item = GetItem(key);
            if (item != null && ValueEquals(item.Value, value))
                return item.Index;
            return -1;
        }

        public virtual IEnumerable<V> AsValues()
        {
            return (IEnumerable<V>)this;
        }

        public virtual IEnumerable<ISeriesItem<V>> AsItems()
        {
            foreach (ISeriesItem<V> item in this)
            {
                yield return item;
            }
        }

        public virtual IEnumerator<IUnique<V>> GetUniqueEnumerator()
        {
            return new SeriesItemKeyEnumerator<V>(this);
        }

        public virtual IEnumerator<ISeriesItem<V>> GetEnumerator()
        {
            return new SeriesItemEnumerator<V>(this);
        }

        public virtual IEnumerator<long> GetKeyEnumerator()
        {
            return new SeriesItemUniqueKeyEnumerator<V>(this);
        }

        IEnumerator<V> IEnumerable<V>.GetEnumerator()
        {
            return new SeriesItemEnumerator<V>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SeriesItemEnumerator<V>(this);
        }

        protected ulong getPosition(long key)
        {
            return ((ulong)key % maxid);
        }

        protected static ulong getPosition(long key, uint tableMaxId)
        {
            return ((ulong)key % tableMaxId);
        }

        protected virtual void Rehash(int newSize)
        {
            int finish = count;
            int newsize = newSize;
            uint newMaxId = (uint)(newsize - 1);
            ISeriesItem<V>[] newItemTable = EmptyTable(newsize);
            ISeriesItem<V> item = first;
            item = item.Next;
            if (removed > 0)
            {
                rehashAndReindex(item, newItemTable, newMaxId);
            }
            else
            {
                rehash(item, newItemTable, newMaxId);
            }

            table = newItemTable;
            maxid = newMaxId;
            size = newsize;
        }

        private void rehashAndReindex(ISeriesItem<V> item, ISeriesItem<V>[] newItemTable, uint newMaxId)
        {
            int _conflicts = 0;
            uint _newMaxId = newMaxId;
            ISeriesItem<V>[] _newItemTable = newItemTable;
            ISeriesItem<V> _firstitem = EmptyItem();
            ISeriesItem<V> _lastitem = _firstitem;
            do
            {
                if (!item.Removed)
                {
                    ulong pos = getPosition(item.Id, _newMaxId);

                    ISeriesItem<V> mem = _newItemTable[pos];

                    if (mem == null)
                    {
                        item.Extended = null;
                        _newItemTable[pos] = _lastitem = _lastitem.Next = item;
                    }
                    else
                    {
                        for (; ; )
                        {
                            if (mem.Extended == null)
                            {
                                item.Extended = null;
                                ;
                                _lastitem = _lastitem.Next = mem.Extended = item;
                                _conflicts++;
                                break;
                            }
                            else
                                mem = mem.Extended;
                        }
                    }
                }

                item = item.Next;
            } while (item != null);

            conflicts = _conflicts;
            removed = 0;
            first = _firstitem;
            last = _lastitem;
        }

        private void rehash(ISeriesItem<V> item, ISeriesItem<V>[] newItemTable, uint newMaxId)
        {
            int _conflicts = 0;
            uint _newMaxId = newMaxId;
            ISeriesItem<V>[] _newItemTable = newItemTable;
            do
            {
                if (!item.Removed)
                {
                    ulong pos = getPosition(item.Id, _newMaxId);

                    ISeriesItem<V> mem = _newItemTable[pos];

                    if (mem == null)
                    {
                        item.Extended = null;
                        _newItemTable[pos] = item;
                    }
                    else
                    {
                        for (; ; )
                        {
                            if (mem.Extended == null)
                            {
                                item.Extended = null;
                                mem.Extended = item;
                                _conflicts++;
                                break;
                            }
                            else
                                mem = mem.Extended;
                        }
                    }
                }

                item = item.Next;
            } while (item != null);
            conflicts = _conflicts;
        }

        protected ulong mapPosition(long key)
        {
            // standard hashmap method to establish position / index in table

            // return ((ulong)key % (uint)size);

            // author's algorithm to establish position / index in table            
            // based on most significant bit - BSR (or equivalent depending on the cpu type) 
            // alsow project must be compiled in x64 format (default) for x86 format proper C lib compilation of BitScan.dll is needed       

            return Submix.Map(key, maxid, bitmask, msbid);
        }

        protected ulong mapPosition(long key, uint newmaxid, ulong newbitmask, int newmsbid)
        {
            // standard hashmap method to establish position / index in table 

            // return ((ulong)key % (uint)newsize);

            // author's algorithm to establish position / index in table            
            // based on most significant bit - BSR (or equivalent depending on the cpu type)
            // alsow project must be compiled in x64 format (default) for x86 format proper C lib compilation of BitScan.dll is needed       

            return Submix.Map(key, newmaxid, newbitmask, newmsbid);
        }

        protected virtual void Remap(int newSize)
        {
            int finish = count;
            int _size = newSize;
            uint _maxid = (uint)(_size - 1);
            ISeriesItem<V>[] _table = EmptyTable(_size);
            ISeriesItem<V> item = first;
            item = item.Next;
            if (removed > 0)
            {
                remapAndReindex(item, _table, _maxid);
            }
            else
            {
                remap(item, _table, _maxid);
            }

            table = _table;
            maxid = _maxid;
            size = _size;
        }

        private void remapAndReindex(ISeriesItem<V> item, ISeriesItem<V>[] newTable, uint newMaxId)
        {
            int _conflicts = 0;
            uint _maxid = newMaxId;
            uint _size = _maxid + 1;
            ulong _bitmask = Submix.Mask(_size);
            int _msbid = Submix.MsbId(_size);
            ISeriesItem<V>[] _table = newTable;
            ISeriesItem<V> _first = EmptyItem();
            ISeriesItem<V> _last = _first;
            do
            {
                if (!item.Removed)
                {
                    ulong pos = mapPosition(item.Id, _maxid, _bitmask, _msbid);

                    ISeriesItem<V> mem = _table[pos];

                    if (mem == null)
                    {
                        item.Extended = null;
                        _table[pos] = _last = _last.Next = item;
                    }
                    else
                    {
                        for (; ; )
                        {
                            if (mem.Extended == null)
                            {
                                item.Extended = null; ;
                                _last = _last.Next = mem.Extended = item;
                                _conflicts++;
                                break;
                            }
                            else
                                mem = mem.Extended;
                        }
                    }
                }

                item = item.Next;

            } while (item != null);

            conflicts = _conflicts;
            removed = 0;
            first = _first;
            last = _last;
            bitmask = _bitmask;
            msbid = _msbid;
        }

        private void remap(ISeriesItem<V> item, ISeriesItem<V>[] newTable, uint newMaxId)
        {
            int _conflicts = 0;
            uint _maxid = newMaxId;
            uint _size = _maxid + 1;
            ulong _bitmask = Submix.Mask(_size);
            int _msbid = Submix.MsbId(_size);
            do
            {
                if (!item.Removed)
                {
                    ulong pos = mapPosition(item.Id, _maxid, _bitmask, _msbid);

                    ISeriesItem<V> mem = newTable[pos];

                    if (mem == null)
                    {
                        item.Extended = null;
                        newTable[pos] = item;
                    }
                    else
                    {
                        for (; ; )
                        {
                            if (mem.Extended == null)
                            {
                                item.Extended = null;
                                mem.Extended = item;
                                _conflicts++;
                                break;
                            }
                            else
                                mem = mem.Extended;
                        }
                    }
                }

                item = item.Next;

            } while (item != null);

            conflicts = _conflicts;
            bitmask = _bitmask;
            msbid = _msbid;
        }

        protected bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    first = null;
                    last = null;
                    table = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async ValueTask DisposeAsyncCore()
        {
            await new ValueTask(Task.Run(() =>
            {
                first = null;
                last = null;
                table = null;

            }));
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();

            Dispose(false);

            GC.SuppressFinalize(this);
        }

        public int CompareTo(IUnique other)
        {
            return Id.CompareTo(other.Id);
        }

        public bool Equals(IUnique other)
        {
            return Id.Equals(other.Id);
        }

        public byte[] GetBytes()
        {
            return Id.GetBytes();
        }

        public byte[] GetIdBytes()
        {
            return Id.GetBytes();
        }

        public IUnique Empty => Usid.Empty;

        public DateTime Created { get; set; }
        public string Creator { get; set; }
        public DateTime Modified { get; set; }
        public string Modifier { get; set; }

        public Type ElementType => typeof(V);

        public bool ContainsListCollection => true;

        public IList GetList()
        {
            return (IList)this;
        }

        public virtual void ExceptWith(IEnumerable<V> other)
        {
            this.AsItems().ForOnly(e => other.Contains(e.Value), e => Remove(e));
        }

        public virtual void IntersectWith(IEnumerable<V> other)
        {
            this.AsItems().ForOnly(e => !other.Contains(e.Value), (e) => Remove(e));
        }

        public virtual bool IsProperSubsetOf(IEnumerable<V> other)
        {
            return (this.Count < other.Count()) && this.All(e => other.Contains(e));
        }

        public virtual bool IsProperSupersetOf(IEnumerable<V> other)
        {
            return (this.Count > other.Count()) && other.All(e => this.Contains(e));
        }

        public virtual bool IsSubsetOf(IEnumerable<V> other)
        {
            return (this.Count <= other.Count()) && this.All(e => other.Contains(e));
        }

        public virtual bool IsSupersetOf(IEnumerable<V> other)
        {
            return (this.Count >= other.Count()) && other.All(e => this.Contains(e));
        }

        public virtual bool Overlaps(IEnumerable<V> other)
        {
            return this.Any(e => other.Contains(e));
        }

        public virtual bool SetEquals(IEnumerable<V> other)
        {
            return ReferenceEquals(this, other) || (this.Count == other.Count()) && this.All(e => other.Contains(e));
        }

        public virtual void SymmetricExceptWith(IEnumerable<V> other)
        {
            var toRemove = this.AsItems().ForOnly(e => other.Contains(e.Value), (e) => e).ToListing();
            other.ForOnly(e => !this.Contains(e), e => this.Add(e));
            toRemove.ForEach(r => Remove(r));
        }

        public virtual void UnionWith(IEnumerable<V> other)
        {
            this.Add(other);
        }

        bool IList.IsFixedSize => throw new NotImplementedException();

        object IList.this[int index] { get => this[index]; set => this[index] = (V)value; }

        int IList.Add(object value)
        {
            return this.InnerPut((V)value).Index;
        }

        bool IList.Contains(object value)
        {
            return this.Contains((V)value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((V)value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (V)value);
        }

        void IList.Remove(object value)
        {
            this.Remove((V)value);
        }
    }
}
