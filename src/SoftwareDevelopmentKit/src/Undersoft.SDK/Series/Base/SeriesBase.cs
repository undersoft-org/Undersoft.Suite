
namespace Undersoft.SDK.Series.Base
{
    using Enumerators;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Undersoft.SDK;
    using Undersoft.SDK.Extracting;
    using Undersoft.SDK.Uniques;

    public abstract class SeriesBase<V> : Identifiable, IIdentifiable, ISeries<V>, ISet<V>, IList, IAsyncDisposable, IListSource
    {

        internal const float RESIZING_VECTOR = 2.333F;
        internal const float CONFLICTS_PERCENT_LIMIT = 0.22F;
        internal const float REMOVED_PERCENT_LIMIT = 0.45F;
        internal const ulong MAX_BIT_MASK = 0xFFFFFFFFFFFFFFFF;

        protected IUniqueKey unique = Unique.Bit64;

        protected ISeriesItem<V> first, last;
        protected ISeriesItem<V>[] table;
        protected readonly int minsize;
        protected int count, conflicts, removed, size, mincount, msbid;
        protected uint maxid;
        protected ulong bitmask;

        private int nextSize()
        {
            return (((int)(size * RESIZING_VECTOR)) | 3);
        }
        private int previousSize()
        {
            return (int)(size * (1 - REMOVED_PERCENT_LIMIT)) | 3;
        }

        protected void countIncrement()
        {
            if ((++count + 3) > size)
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

        protected SeriesBase(int capacity = 17, HashBits bits = HashBits.bit64)
        {
            if (bits != HashBits.bit64)
                unique = Unique.Bit32;

            size = capacity;
            minsize = capacity;
            maxid = (uint)(capacity - 1);
            table = EmptyTable(capacity);
            first = EmptyItem();
            last = first;
            Equal = getValueComparer();
        }
        protected SeriesBase(IList<ISeriesItem<V>> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            if (collection != null)
                this.Add(collection);
        }
        protected SeriesBase(IList<V> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity > collection.Count ? capacity : collection.Count, bits)
        {
            if (collection != null)
                foreach (var c in collection)
                    this.Add(c);
        }
        protected SeriesBase(IEnumerable<ISeriesItem<V>> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity, bits)
        {
            if (collection != null)
                this.Add(collection);
        }
        protected SeriesBase(IEnumerable<V> collection, int capacity = 17, HashBits bits = HashBits.bit64) : this(capacity, bits)
        {
            if (collection != null)
                foreach (var c in collection)
                    this.Add(c);
        }

        public virtual ISeriesItem<V> First
        { get { return first; } }
        public virtual ISeriesItem<V> Last
        { get { return last; } }

        public virtual int Size => size;
        public virtual int Count => count;
        public virtual int MinCount
        {
            get => mincount;
            set => mincount = value;
        }
        public virtual bool IsReadOnly { get; set; }
        public virtual bool IsSynchronized { get; set; }
        public virtual bool IsRepeatable { get => false; }
        public virtual object SyncRoot { get; set; }
        public virtual Func<V, V, bool> Equal { get; }

        public virtual V this[int index]
        {
            get => GetItem(index).Value;
            set => GetItem(index).Value = value;
        }
        protected V this[long hashkey]
        {
            get => InnerGet(hashkey);
            set => InnerSet(hashkey, value);
        }
        public virtual V this[object key]
        {
            get => InnerGet(unique.Key(key));
            set => InnerSet(unique.Key(key), value);
        }
        public virtual V this[IIdentifiable key]
        {
            get => InnerGet(key.Id);
            set => InnerSet(key.Id, value);
        }
        object IFindable.this[object key]
        {
            get => InnerGet(unique.Key(key));
            set => InnerSet(unique.Key(key), (V)value);
        }
        object IList.this[int index] 
        { 
            get => this[index]; 
            set => this[index] = (V)value; 
        }

        protected virtual V InnerGet(long key)
        {
            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                    return (!mem.Removed)
                        ? mem.Value
                        : default(V);

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
            return InnerGet(unique.Key(key));
        }
        public virtual V Get(IIdentifiable key)
        {
            return InnerGet(unique.Key(key));
        }

        protected virtual bool InnerTryGet(long key, out ISeriesItem<V> output)
        {
            output = table[getPosition(key)];

            while (output != null)
            {
                if (output.Equals(key))
                    return (output.Removed) ? false : true;

                output = output.Extended;
            }
            return false;
        }
        
        public virtual bool TryGet(long key, out ISeriesItem<V> output)
        {
            return InnerTryGet(key, out output);
        }
        public virtual bool TryGet(object key, out ISeriesItem<V> output)
        {
            return InnerTryGet(unique.Key(key), out output);
        }
        public virtual bool TryGet(IIdentifiable key, out ISeriesItem<V> output)
        {
            return InnerTryGet(key.Id, out output);
        }

        public virtual bool TryGet(object key, out V output)
        {
            output = default(V);
            if (InnerTryGet(unique.Key(key), out ISeriesItem<V> item))
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
        public virtual bool TryGet(IIdentifiable key, out V output)
        {
            output = default;
            if (!InnerTryGet(key.Id, out ISeriesItem<V> _output))
                return false;
            output = _output.Value;
            return true;
        }

        protected virtual ISeriesItem<V> InnerGetItem(long key)
        {
            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                    return (!mem.Removed)
                        ? mem
                        : null;

                mem = mem.Extended;
            }

            return mem;
        }
        
        public virtual ISeriesItem<V> GetItem(long key)
        {
            return InnerGetItem(key);
        }
        public virtual ISeriesItem<V> GetItem(object key)
        {
            return InnerGetItem(unique.Key(key));
        }
        public virtual ISeriesItem<V> GetItem(IIdentifiable key)
        {
            return InnerGetItem(unique.Key(key, key.TypeId));
        }
        public abstract ISeriesItem<V> GetItem(int index);

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

        protected virtual ISeriesItem<V> InnerSet(long key, V value)
        {
            var item = InnerGetItem(key);
            if (item != null) item.Value = value;
            return item;
        }
        protected virtual ISeriesItem<V> InnerSet(ISeriesItem<V> value)
        {
            var item = InnerGetItem(value.Id);
            if (item != null) item.Value = value.Value;
            return item;
        }
        protected virtual ISeriesItem<V> InnerSet(V value)
        {
            var item = InnerGetItem(unique.Key(value));
            if (item != null) item.Value = value;
            return item;
        }
        
        public virtual ISeriesItem<V> Set(object key, V value)
        {
            return InnerSet(unique.Key(key), value);
        }
        public virtual ISeriesItem<V> Set(long key, V value)
        {
            return InnerSet(key, value);
        }
        public virtual ISeriesItem<V> Set(IIdentifiable key, V value)
        {
            return InnerSet(unique.Key(key), value);
        }
        public virtual ISeriesItem<V> Set(V value)
        {
            return InnerSet(value);
        }
        public virtual ISeriesItem<V> Set(ISeriesItem<V> value)
        {
            return InnerSet(value);
        }
        public virtual int Set(IEnumerable<V> values)
        {
            int count = 0;
            foreach (var value in values)
            {
                if (Set(value) != null)
                    count++;
            }

            return count;
        }
        public virtual int Set(IList<V> values)
        {
            int i = 0, c = values.Count;
            while (--c > -1)
            {
                if (Set(values[c]) != null)
                    i++;
            }
            return i;
        }
        public virtual int Set(IEnumerable<ISeriesItem<V>> values)
        {
            int count = 0;
            foreach (var value in values)
            {
                if (InnerSet(value) != null)
                    count++;
            }

            return count;
        }

        protected abstract ISeriesItem<V> InnerPut(long key, V value);
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
            return InnerPut(unique.Key(key), value);
        }
        public virtual ISeriesItem<V> Put(object key, object value)
        {
            if (value is V)
                return InnerPut(unique.Key(key), (V)value);
            return null;
        }
        public virtual ISeriesItem<V> Put(ISeriesItem<V> item)
        {
            return InnerPut(item);
        }
        public virtual void Put(IList<ISeriesItem<V>> items)
        {
            int i = 0, c = items.Count;
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
        public virtual void Put(object value)
        {
            if (value is V)
                Put((V)value);
            else if (value is ISeriesItem<V>)
                Put((ISeriesItem<V>)value);
        }
        public virtual void Put(IList<V> items)
        {
            int i = 0, c = items.Count;
            while (i < c)
                InnerPut(items[i++]);
        }
        public virtual void Put(IEnumerable<V> items)
        {
            foreach (V item in items)
                InnerPut(item);
        }

        protected abstract bool InnerAdd(long key, V value);
        protected abstract bool InnerAdd(V value);
        protected abstract bool InnerAdd(ISeriesItem<V> value);
        
        public virtual bool Add(long key, object value)
        {
            return InnerAdd(key, (V)value);
        }
        public virtual bool Add(long key, V value)
        {
            return InnerAdd(key, value);
        }
        public virtual bool Add(object key, V value)
        {
            return InnerAdd(unique.Key(key), value);
        }
        public virtual void Add(ISeriesItem<V> item)
        {
            InnerAdd(item);
        }
        public virtual void Add(IList<ISeriesItem<V>> itemList)
        {
            int i = 0, c = itemList.Count;
            while (i < c)
                InnerAdd(itemList[i++]);
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
        public virtual void Add(IList<V> items)
        {
            int i = 0, c = items.Count;
            while (i < c)
                InnerAdd(items[i++]);
        }
        public virtual void Add(IEnumerable<V> items)
        {
            foreach (V item in items)
                Add(item);
        }
        bool ISet<V>.Add(V value)
        {
            return InnerAdd(value);
        }

        public virtual bool TryAdd(V value)
        {
            return InnerAdd(value);
        }
        public virtual bool TryAdd(object key, V value)
        {
            return InnerAdd(unique.Key(key), value);
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
            ISeriesItem<V> newItem = NewItem(unique.Key(key), default(V));
            if (InnerAdd(newItem))
                return newItem;
            return null;
        }

        protected abstract void InnerInsert(int index, ISeriesItem<V> item);
        
        public virtual void Insert(int index, ISeriesItem<V> seriesItem)
        {

            long key = seriesItem.Id;
            V value = seriesItem.Value;
            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                item = NewItem(value);
                table[pos] = item;
                InnerInsert(index, item);
                countIncrement();
                return;
            }

            for (; ; )
            {

                if (item.Equals(key))
                {

                    if (item.Removed)
                    {
                        var newitem = NewItem(item);
                        item.Extended = newitem;
                        InnerInsert(index, newitem);
                        conflictIncrement();
                        return;
                    }
                    throw new Exception("SeriesItem exist");

                }

                if (item.Extended == null)
                {
                    var newitem = NewItem(item);
                    item.Extended = newitem;
                    InnerInsert(index, newitem);
                    conflictIncrement();
                    return;
                }
                item = item.Extended;
            }
        }
        public virtual void Insert(int index, V value)
        {

            long key = unique.Key(value);
            ulong pos = getPosition(key);

            ISeriesItem<V> item = table[pos];

            if (item == null)
            {
                item = NewItem(value);
                table[pos] = item;
                InnerInsert(index, item);
                countIncrement();
                return;
            }

            for (; ; )
            {

                if (item.Equals(key))
                {

                    if (item.Removed)
                    {
                        var newitem = NewItem(item);
                        item.Extended = newitem;
                        InnerInsert(index, newitem);
                        conflictIncrement();
                        return;
                    }
                    throw new Exception("SeriesItem exist");

                }

                if (item.Extended == null)
                {
                    var newitem = NewItem(item);
                    item.Extended = newitem;
                    InnerInsert(index, newitem);
                    conflictIncrement();
                    return;
                }
                item = item.Extended;
            }
        }

        public virtual bool Enqueue(V value)
        {
            return InnerAdd(value);
        }
        public virtual bool Enqueue(object key, V value)
        {
            return InnerAdd(unique.Key(key), value);
        }
        public virtual void Enqueue(ISeriesItem<V> item)
        {
            InnerAdd(item);
        }

        public virtual void SetMinCount(int minCount)
        {
            mincount = minCount;
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

        public virtual bool TryPick(int skip, out V output)
        {
            output = this.Skip(skip).FirstOrDefault();
            if (output != null)
            {
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
            renewClear(minsize);
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
            renewClear(minsize);
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
            return InnerContainsKey(unique.Key(key));
        }
        public virtual bool ContainsKey(long key)
        {
            return InnerContainsKey(key);
        }
        public virtual bool ContainsKey(IIdentifiable key)
        {
            return InnerContainsKey(unique.Key(key));
        }

        public virtual bool Contains(ISeriesItem<V> item)
        {
            return InnerContainsKey(item.Id);
        }
        public virtual bool Contains(V item)
        {
            return InnerContainsKey(unique.Key(item));
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

                    if (Equals(mem.Value, item))
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
            return InnerRemove(unique.Key(item)).Equals(default(V)) ? false : true;
        }
        public virtual V Remove(object key)
        {
            return InnerRemove(unique.Key(key));
        }
        public virtual bool Remove(ISeriesItem<V> item)
        {
            return InnerRemove(item.Id).Equals(default(V)) ? false : true;
        }
        public virtual bool TryRemove(object key)
        {
            V result = InnerRemove(unique.Key(key));
            if (result != null &&
                !result.Equals(default(V)))
                return true;
            return false;
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
            size = minsize;
            maxid = (uint)(minsize - 1);
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
            int c = count, i = index, l = array.Length;
            if (l - i < c)
            {
                c = l - i;
                foreach (ISeriesItem<V> ves in this.Take(c))
                    array[i++] = ves;
            }
            else
                foreach (ISeriesItem<V> ves in this)
                    array[i++] = ves;
        }
        public virtual void CopyTo(Array array, int index)
        {
            int c = count, i = index, l = array.Length;
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
            int c = count, i = index, l = array.Length;
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
            return AsValues().Cast<object>().ToArray();
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

        public abstract ISeriesItem<V> NewItem(long key, V value);
        public abstract ISeriesItem<V> NewItem(object key, V value);
        public abstract ISeriesItem<V> NewItem(ISeriesItem<V> item);
        public abstract ISeriesItem<V> NewItem(V item);

        public abstract ISeriesItem<V>[] EmptyTable(int size);

        public virtual int IndexOf(ISeriesItem<V> item)
        {
            if (item != null)
                return item.Index;
            return -1;
        }
        public virtual int IndexOf(V value)
        {
            var item = GetItem(value);
            if (item != null)
                return item.Index;
            return -1;
        }
        protected virtual int IndexOf(long key, V value)
        {
            var item = GetItem(key);
            if (item != null && Equals(item.Value, value))
                return item.Index;
            return -1;
        }

        public virtual IEnumerable<V> AsValues()
        {
            foreach (var item in (IEnumerable<V>)this)
            {
                yield return item;
            }
        }
        public virtual IEnumerable<ISeriesItem<V>> AsItems()
        {
            foreach (var item in this)
            {
                yield return item;
            }
        }

        public virtual IEnumerator<ISeriesItem<V>> GetEnumerator()
        {
            return new SeriesItemEnumerator<V>(this);
        }
        public virtual IAsyncEnumerator<V> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new SeriesItemAsyncEnumerator<V>(this);
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
            int _size = newSize;
            uint _maxid = (uint)(_size - 1);
            ISeriesItem<V>[] _table = EmptyTable(_size);
            ISeriesItem<V> item = first;
            item = item.Next;
            if (removed > 0)
            {
                rehashAndReindex(item, _table, _maxid);
            }
            else
            {
                rehash(item, _table, _maxid);
            }

            table = _table;
            maxid = _maxid;
            size = _size;
        }

        private void rehashAndReindex(ISeriesItem<V> item, ISeriesItem<V>[] newTable, uint newMaxId)
        {
            int _conflicts = 0;
            uint _maxid = newMaxId;
            ISeriesItem<V>[] _table = newTable;
            ISeriesItem<V> _first = EmptyItem();
            ISeriesItem<V> _last = _first;
            do
            {
                if (!item.Removed)
                {
                    ulong pos = getPosition(item.Id, _maxid);

                    ISeriesItem<V> ex_item = _table[pos];

                    if (ex_item == null)
                    {
                        item.Extended = null;
                        _table[pos] = _last = _last.Next = item;
                    }
                    else
                    {
                        for (; ; )
                        {
                            if (ex_item.Extended == null)
                            {
                                item.Extended = null; ;
                                _last = _last.Next = ex_item.Extended = item;
                                _conflicts++;
                                break;
                            }
                            else
                                ex_item = ex_item.Extended;
                        }
                    }
                }

                item = item.Next;

            } while (item != null);

            conflicts = _conflicts;
            removed = 0;
            first = _first;
            last = _last;
        }
        private void rehash(ISeriesItem<V> item, ISeriesItem<V>[] newTable, uint newMaxId)
        {
            int _conflicts = 0;
            uint _maxid = newMaxId;
            ISeriesItem<V>[] _table = newTable;
            do
            {
                if (!item.Removed)
                {
                    ulong pos = getPosition(item.Id, _maxid);

                    ISeriesItem<V> ex_item = _table[pos];

                    if (ex_item == null)
                    {
                        item.Extended = null;
                        _table[pos] = item;
                    }
                    else
                    {
                        for (; ; )
                        {
                            if (ex_item.Extended == null)
                            {
                                item.Extended = null;
                                ex_item.Extended = item;
                                _conflicts++;
                                break;
                            }
                            else
                                ex_item = ex_item.Extended;
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
                renewClear(minsize);

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
                renewClear(minsize);

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

        public DateTime Created { get; set; }
        public string Creator { get; set; }

        public DateTime Modified { get; set; }
        public string Modifier { get; set; }

        public Type ElementType => typeof(V);

        public bool ContainsListCollection => true;

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

        bool IList.IsFixedSize => false;

        public IList GetList()
        {
            return (IList)this;
        }
        int IList.Add(object value)
        {
            return InnerPut((V)value).Index;
        }
        bool IList.Contains(object value)
        {
            return Contains((V)value);
        }
        int IList.IndexOf(object value)
        {
            return IndexOf((V)value);
        }
        void IList.Insert(int index, object value)
        {
            Insert(index, (V)value);
        }
        void IList.Remove(object value)
        {
            Remove((V)value);
        }
    }
}
