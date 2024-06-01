namespace Undersoft.SDK.Series.Base
{
    using System.Collections.Generic;
    using Undersoft.SDK;
    using Undersoft.SDK.Series;
    using Undersoft.SDK.Uniques;

    public abstract class TypedListingBase<V> : TypedSeriesBase<V> where V : IIdentifiable
    {
        protected ISeriesItem<V>[] vector;
        protected bool repeatable;
        protected int repeated;

        public TypedListingBase() : this(17, HashBits.bit64) { }

        public TypedListingBase(
            IEnumerable<IUnique<V>> collection,
            int capacity = 17,
            bool repeatable = false,
            HashBits bits = HashBits.bit64
        ) : this(repeatable, capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedListingBase(
            IEnumerable<V> collection,
            int capacity = 17,
            bool repeatable = false,
            HashBits bits = HashBits.bit64
        ) : this(repeatable, capacity, bits)
        {
            foreach (var c in collection)
                this.Add(c);
        }

        public TypedListingBase(bool repeatable, int capacity = 17, HashBits bits = HashBits.bit64)
            : base(capacity, bits)
        {
            this.repeatable = repeatable;
            vector = EmptyVector(capacity);
        }

        public TypedListingBase(int capacity = 17, HashBits bits = HashBits.bit64)
            : base(capacity, bits)
        {
            vector = EmptyVector(capacity);
        }

        public override ISeriesItem<V> First
        {
            get { return first; }
        }

        public override ISeriesItem<V> Last
        {
            get { return vector[(count + removed) - 1]; }
        }

        public override bool IsRepeatable
        {
            get => repeatable;
        }

        public override void Clear()
        {
            base.Clear();
            vector = EmptyVector(minSize);
        }

        public override void CopyTo(Array array, int index)
        {
            int c = count,
                i = index,
                l = array.Length;

            if (l - i < c)
                c = l - i;

            for (int j = 0; j < c; j++)
                array.SetValue(GetItem(j).Value, i++);
        }

        public override void CopyTo(ISeriesItem<V>[] array, int index)
        {
            int c = count,
                i = index,
                l = array.Length;

            if (l - i < c)
                c = l - i;

            for (int j = 0; j < c; j++)
            {
                array[i++] = GetItem(j);
            }
        }

        public override void CopyTo(V[] array, int index)
        {
            int c = count,
                i = index,
                l = array.Length;

            if (l - i < c)
                c = l - i;

            for (int j = 0; j < c; j++)
                array[i++] = GetItem(j).Value;
        }

        public abstract ISeriesItem<V>[] EmptyVector(int size);

        public override void Flush()
        {
            base.Flush();
            vector = EmptyVector(size);
        }

        protected virtual ISeriesItem<V> GetItem(long key, V item)
        {
            if (key == 0)
                return null;

            ISeriesItem<V> mem = table[getPosition(key)];

            while (mem != null)
            {
                if (mem.Equals(key))
                {
                    if (repeatable)
                        while (mem != null || !ValueEquals(mem.Value, item))
                            mem = mem.Next;

                    if (!mem.Removed)
                        return mem;
                    return null;
                }
                mem = mem.Extended;
            }

            return mem;
        }

        public override ISeriesItem<V> GetItem(int index)
        {
            if (index < count)
            {
                if (removed > 0)
                    Reindex();

                return vector[index];
            }
            throw new IndexOutOfRangeException("Index out of range");
        }

        public override ISeriesItem<V> Next(ISeriesItem<V> item)
        {
            var _item = vector[item.Index + 1];
            if (_item != null)
            {
                if (!_item.Removed)
                    return _item;
                return Next(_item);
            }
            return null;
        }

        public override V[] ToArray()
        {
            V[] array = new V[count];
            CopyTo(array, 0);
            return array;
        }

        protected ISeriesItem<V> createNew(ISeriesItem<V> item)
        {
            int id = count + removed;
            item.Index = id;
            vector[id] = item;
            return item;
        }

        protected ISeriesItem<V> createNew(long key, V value)
        {
            int id = count + removed;
            var newitem = NewItem(key, value);
            newitem.Index = id;
            vector[id] = newitem;
            return newitem;
        }

        protected void createRepeated(ISeriesItem<V> item, V value)
        {
            var _item = createNew(item.Id, item.Value);
            item.Value = value;
            _item.Next = item.Next;
            item.Next = _item;
            _item.Repeated = true;
        }

        protected void createRepeated(ISeriesItem<V> item, ISeriesItem<V> newitem)
        {
            var _item = createNew(newitem);
            var val = item.Value;
            item.Value = _item.Value;
            _item.Value = val;
            _item.Next = item.Next;
            item.Next = _item;
            _item.Repeated = true;
        }

        protected ISeriesItem<V> swapRepeated(ISeriesItem<V> item)
        {
            var value = item.Value;
            var _item = item.Next;
            item.Value = _item.Value;
            _item.Value = value;
            item.Next = _item.Next;
            _item.Next = _item;
            return _item;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    first = null;
                    last = null;
                }
                table = null;
                vector = null;

                disposedValue = true;
            }
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

                    if (repeatable)
                    {
                        createRepeated(item, value);
                        countIncrement();
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

                    if (repeatable)
                    {
                        createRepeated(item, value);
                        countIncrement();
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

                    if (repeatable)
                    {
                        createRepeated(item, value);
                        countIncrement();
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
            int c = count - index;
            if (c > 0)
            {
                if (removed > 0)
                    reindexWithInsert(index, item);
                else
                {
                    var replaceItem = GetItem(index);

                    while (replaceItem != null)
                    {
                        int id = ++replaceItem.Index;
                        var _replaceItem = vector[id];
                        vector[id] = replaceItem;
                        replaceItem = _replaceItem;
                    }

                    item.Index = index;
                    vector[index] = item;
                }
            }
            else
            {
                int id = count + removed;
                item.Index = id;
                vector[id] = item;
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
                    item.Value = value.Value;

                    if (item.Removed)
                    {
                        item.Removed = false;
                        removedDecrement();
                    }
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
                    item.Value = value;
                    if (item.Removed)
                    {
                        item.Removed = false;
                        removedDecrement();
                    }

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
                    item.Value = value;
                    if (item.Removed)
                    {
                        item.Removed = false;
                        removedDecrement();
                    }

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

        protected override V InnerRemove(long key)
        {
            ISeriesItem<V> _item = table[getPosition(key)];

            while (_item != null)
            {
                if (_item.Equals(key))
                {
                    if (_item.Removed)
                        return default(V);

                    if (repeatable && _item.Next != null)
                        _item = swapRepeated(_item);

                    _item.Removed = true;
                    removedIncrement();
                    return _item.Value;
                }
                _item = _item.Extended;
            }
            return default(V);
        }

        protected override V InnerRemove(long key, V item)
        {
            ISeriesItem<V> _item = table[getPosition(key)];

            while (_item != null)
            {
                if (_item.Equals(key))
                {
                    if (_item.Removed)
                        return default(V);
                    do
                    {
                        if (ValueEquals(_item.Value, item))
                        {
                            if (_item.Next != null)
                                _item = swapRepeated(_item);

                            _item.Removed = true;
                            removedIncrement();
                            return _item.Value;
                        }
                        _item = _item.Next;
                    } while (_item != null);
                    return default(V);
                }
                _item = _item.Extended;
            }
            return default(V);
        }

        public virtual bool TryRemove(long key, V item)
        {
            var output = InnerRemove(key, item);
            return (output != null) ? true : false;
        }

        public override int IndexOf(V item)
        {
            return IndexOf(unique.Key(item), item);
        }

        protected override int IndexOf(long key, V value)
        {
            if (!repeatable)
                return base.IndexOf(key, value);

            var item = GetItem(key);
            if (item == null)
                return -1;

            do
            {
                if (ValueEquals(item.Value, value))
                    return item.Index;

                item = item.Next;
            } while (item != null);

            return -1;
        }

        public override bool Contains(ISeriesItem<V> item)
        {
            return IndexOf(item.Id, item.Value) > -1;
        }

        public override bool Contains(IUnique<V> item)
        {
            return IndexOf(unique.Key(item), item.UniqueValue) > -1;
        }

        public override bool Contains(V item)
        {
            return IndexOf(item) > -1;
        }

        public override bool Contains(long key, V item)
        {
            return IndexOf(key, item) > -1;
        }

        public override V Dequeue()
        {
            var _output = Next(first);
            if (_output == null)
                return default(V);

            if (repeatable && _output.Next != null)
                _output = swapRepeated(_output);
            else
                first = _output;

            _output.Removed = true;
            removedIncrement();
            return _output.Value;
        }

        public override bool TryDequeue(out V output)
        {
            output = default(V);
            if (count < mincount)
                return false;

            var _output = Next(first);
            if (_output == null)
                return false;

            if (repeatable && _output.Next != null)
                _output = swapRepeated(_output);
            else
                first = _output;

            _output.Removed = true;
            removedIncrement();
            output = _output.Value;
            return true;
        }

        public override bool TryDequeue(out ISeriesItem<V> output)
        {
            output = null;
            if (count < mincount)
                return false;

            output = Next(first);
            if (output == null)
                return false;

            if (repeatable && output.Next != null)
                output = swapRepeated(output);
            else
                first = output;

            output.Removed = true;
            removedIncrement();
            return true;
        }

        protected override void Rehash(int newsize)
        {
            int finish = count;
            int _newsize = newsize;
            uint newMaxId = (uint)(_newsize - 1);
            ISeriesItem<V>[] newItemTable = EmptyTable(_newsize);
            if (removed != 0)
            {
                ISeriesItem<V>[] newBaseDeck = EmptyVector(_newsize);
                rehashAndReindex(newItemTable, newBaseDeck, newMaxId);
                vector = newBaseDeck;
            }
            else
            {
                ISeriesItem<V>[] newBaseDeck = EmptyVector(_newsize);
                rehash(newItemTable, newMaxId);
                Array.Copy(vector, 0, newBaseDeck, 0, finish);
                vector = newBaseDeck;
            }
            table = newItemTable;
            maxid = newMaxId;
            size = newsize;
        }

        protected virtual void Reindex()
        {
            ISeriesItem<V> item = null;
            first = EmptyItem();
            int total = count + removed;
            int _counter = 0;
            for (int i = 0; i < total; i++)
            {
                item = vector[i];
                if (item != null && !item.Removed)
                {
                    item.Index = _counter;
                    vector[_counter++] = item;
                }
            }
            removed = 0;
        }

        protected override void renewClear(int capacity)
        {
            if (capacity != size || count > 0)
            {
                size = capacity;
                maxid = (uint)(capacity - 1);
                conflicts = 0;
                removed = 0;
                count = 0;
                table = EmptyTable(size);
                vector = EmptyVector(size);
                first = EmptyItem();
                last = first;
            }
        }

        private void rehash(ISeriesItem<V>[] newItemTable, uint newMaxId)
        {
            int _conflicts = 0;
            int total = count + removed;
            uint _newMaxId = newMaxId;
            ISeriesItem<V>[] _newItemTable = newItemTable;
            ISeriesItem<V> item = null;
            ISeriesItem<V> mem = null;
            for (int i = 0; i < total; i++)
            {
                item = vector[i];

                if (item == null || item.Removed || (repeatable && item.Repeated))
                    continue;

                ulong pos = getPosition(item.Id, _newMaxId);

                mem = _newItemTable[pos];

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
                        mem = mem.Extended;
                    }
                }
            }
            conflicts = _conflicts;
        }

        private void rehashAndReindex(
            ISeriesItem<V>[] newItemTable,
            ISeriesItem<V>[] newBaseDeck,
            uint newMaxId
        )
        {
            int _conflicts = 0;
            int _counter = 0;
            int total = count + removed;
            uint _newMaxId = newMaxId;
            ISeriesItem<V>[] _newItemTable = newItemTable;
            ISeriesItem<V>[] _newBaseDeck = newBaseDeck;
            ISeriesItem<V> item = null;
            ISeriesItem<V> mem = null;
            for (int i = 0; i < total; i++)
            {
                item = vector[i];

                if (item == null || item.Removed)
                    continue;

                if (item.Repeated)
                {
                    item.Index = _counter;
                    _newBaseDeck[_counter++] = item;
                    continue;
                }

                ulong pos = getPosition(item.Id, _newMaxId);

                mem = _newItemTable[pos];

                if (mem == null)
                {
                    item.Extended = null;
                    item.Index = _counter;
                    _newItemTable[pos] = item;
                    _newBaseDeck[_counter++] = item;
                }
                else
                {
                    for (; ; )
                    {
                        if (mem.Extended == null)
                        {
                            item.Extended = null;
                            mem.Extended = item;
                            item.Index = _counter;
                            _newBaseDeck[_counter++] = item;
                            _conflicts++;
                            break;
                        }
                        mem = mem.Extended;
                    }
                }
            }
            first = EmptyItem();
            conflicts = _conflicts;
            removed = 0;
        }

        private void reindexWithInsert(int index, ISeriesItem<V> item)
        {
            ISeriesItem<V> _item = null;
            first = EmptyItem();
            int _counter = 0;
            int total = count + removed;
            for (int i = 0; i < total; i++)
            {
                _item = vector[i];
                if (_item != null && !_item.Removed)
                {
                    _item.Index = _counter;
                    vector[_counter++] = _item;
                    if (_counter == index)
                    {
                        item.Index = _counter;
                        vector[_counter++] = item;
                    }
                }
            }
            removed = 0;
        }
    }
}
