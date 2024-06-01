namespace Undersoft.SDK.Series.Base
{
    using System.Collections.Generic;
    using Undersoft.SDK.Uniques;

    public abstract class ListingBase<V> : SeriesBase<V>
    {
        protected ISeriesItem<V>[] vector;
        protected bool repeatable;
        protected int repeated;

        public ListingBase() : this(17, HashBits.bit64) { }

        public ListingBase(int capacity = 17, HashBits bits = HashBits.bit64) : base(capacity, bits)
        {
            vector = EmptyVector(capacity);
        }

        public ListingBase(bool repeatable, int capacity = 17, HashBits bits = HashBits.bit64)
            : base(capacity, bits)
        {
            this.repeatable = repeatable;
            vector = EmptyVector(capacity);
        }

        public ListingBase(
            IEnumerable<V> collection,
            int capacity = 17,
            bool repeatable = false,
            HashBits bits = HashBits.bit64
        ) : this(repeatable, capacity, bits)
        {
            if (collection != null)
                foreach (V c in collection)
                    InnerAdd(c);
        }

        public ListingBase(
           IEnumerable<ISeriesItem<V>> collection,
           int capacity = 17,
           bool repeatable = false,
           HashBits bits = HashBits.bit64
       ) : this(repeatable, capacity, bits)
        {
            if (collection != null)
                foreach (var c in collection)
                    InnerAdd(c);
        }

        void rehash(ISeriesItem<V>[] newItemTable, uint newMaxId)
        {
            int _conflicts = 0;
            int total = count + removed;
            uint _newMaxId = newMaxId;
            ISeriesItem<V>[] _newItemTable = newItemTable;
            ISeriesItem<V> item = null;
            ISeriesItem<V> _item = null;
            for (int i = 0; i < total; i++)
            {
                item = vector[i];

                if ((item != null) && !item.Removed && !item.Repeated)
                {
                    ulong pos = getPosition(item.Id, _newMaxId);

                    _item = _newItemTable[pos];

                    if (_item == null)
                    {
                        item.Extended = null;
                        _newItemTable[pos] = item;
                    }
                    else
                    {
                        while (_item.Extended != null)
                            _item = _item.Extended;

                        item.Extended = null;
                        _item.Extended = item;
                        _conflicts++;
                    }
                }
            }
            conflicts = _conflicts;
        }

        void rehashAndReindex(ISeriesItem<V>[] newItemTable, ISeriesItem<V>[] newBaseDeck, uint newMaxId)
        {
            int _conflicts = 0;
            int _counter = 0;
            int total = count + removed;
            uint _newMaxId = newMaxId;
            ISeriesItem<V>[] _newItemTable = newItemTable;
            ISeriesItem<V>[] _newBaseDeck = newBaseDeck;
            ISeriesItem<V> item = null;
            ISeriesItem<V> _item = null;
            for (int i = 0; i < total; i++)
            {
                item = vector[i];

                if ((item != null) && !item.Removed)
                {
                    if (item.Repeated)
                    {
                        item.Index = _counter;
                        _newBaseDeck[_counter++] = item;
                    }
                    else
                    {
                        ulong pos = getPosition(item.Id, _newMaxId);

                        _item = _newItemTable[pos];

                        if (_item == null)
                        {
                            item.Extended = null;
                            item.Index = _counter;
                            _newItemTable[pos] = item;
                            _newBaseDeck[_counter++] = item;
                        }
                        else
                        {
                            while (_item.Extended != null)
                                _item = _item.Extended;

                            item.Extended = null;
                            _item.Extended = item;
                            item.Index = _counter;
                            _newBaseDeck[_counter++] = item;
                            _conflicts++;
                        }
                    }
                }
            }
            first = EmptyItem();
            conflicts = _conflicts;
            removed = 0;
        }

        void reindexWithInsert(int index, ISeriesItem<V> item)
        {
            ISeriesItem<V> _item = null;
            first = EmptyItem();
            int _counter = 0;
            int total = count + removed;
            for (int i = 0; i < total; i++)
            {
                _item = vector[i];
                if ((_item != null) && !_item.Removed)
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
            ISeriesItem<V> newitem = NewItem(key, value);
            newitem.Index = id;
            vector[id] = newitem;
            return newitem;
        }

        protected void createRepeated(ISeriesItem<V> item, V value)
        {
            ISeriesItem<V> _item = createNew(item.Id, item.Value);
            item.Value = value;
            _item.Next = item.Next;
            item.Next = _item;
            _item.Repeated = true;
        }

        protected void createRepeated(ISeriesItem<V> item, ISeriesItem<V> newitem)
        {
            ISeriesItem<V> _item = createNew(newitem);
            V val = item.Value;
            item.Value = _item.Value;
            _item.Value = val;
            _item.Next = item.Next;
            item.Next = _item;
            _item.Repeated = true;
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
                        while ((mem != null) || !Equals(mem.Value, item))
                            mem = mem.Next;

                    if (!mem.Removed)
                        return mem;
                    return null;
                }
                mem = mem.Extended;
            }

            return mem;
        }

        protected override int IndexOf(long key, V item)
        {
            if (!repeatable)
                return base.IndexOf(key, item);

            ISeriesItem<V> _item = GetItem(key);

            if (_item == null)
                return -1;

            do
            {
                if (Equals(_item.Value, item))
                    return _item.Index;

                _item = _item.Next;
            } while (_item != null);

            return -1;
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
                    ISeriesItem<V> replaceItem = GetItem(index);

                    while (replaceItem != null)
                    {
                        int id = ++replaceItem.Index;
                        ISeriesItem<V> _replaceItem = vector[id];
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
                    ISeriesItem<V> newitem = createNew(value);
                    item.Extended = newitem;
                    conflictIncrement();
                    return newitem;
                }
                item = item.Extended;
            }
        }

        protected override ISeriesItem<V> InnerPut(V value)
        {
            long key = unique.Key(value);
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
                    ISeriesItem<V> newitem = createNew(key, value);
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
                    ISeriesItem<V> newitem = createNew(key, value);
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

                    if (repeatable && (_item.Next != null))
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
                        if (Equals(_item.Value, item))
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

        protected override void Rehash(int newsize)
        {
            int finish = count;
            int _newsize = newsize;
            uint newMaxId = (uint)(_newsize - 1);
            ISeriesItem<V>[] newItemTable = EmptyTable(_newsize);
            ISeriesItem<V>[] newBaseDeck = EmptyVector(_newsize);
            if (removed != 0)
            {
                rehashAndReindex(newItemTable, newBaseDeck, newMaxId);
                vector = newBaseDeck;
            }
            else
            {
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
                if ((item != null) && !item.Removed)
                {
                    item.Index = _counter;
                    vector[_counter++] = item;
                }
            }
            removed = 0;
        }

        protected override void renewClear(int capacity)
        {
            if ((capacity != size) || (count > 0))
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

        protected void repeatedDecrement()
        {
            removedDecrement();
            --repeated;
        }

        protected void repeatedIncrement()
        {
            countIncrement();
            ++repeated;
        }

        protected virtual ISeriesItem<V> swapRepeated(ISeriesItem<V> item)
        {
            V value = item.Value;
            ISeriesItem<V> _item = item.Next;
            item.Value = _item.Value;
            _item.Value = value;
            item.Next = _item.Next;
            _item.Next = _item;
            return _item;
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

                    if (!repeatable)
                        return false;

                    createRepeated(item, value);
                    countIncrement();
                    return true;
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

        protected override bool InnerAdd(V value)
        {
            long key = unique.Key(value);

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

                    if (!repeatable)
                        return false;

                    createRepeated(item, value);
                    countIncrement();
                    return true;
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

                    if (!repeatable)
                        return false;

                    createRepeated(item, value);
                    countIncrement();
                    return true;
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

        public override void Clear()
        {
            base.Clear();
            vector = EmptyVector(minsize);
        }

        public override bool Contains(ISeriesItem<V> item)
        {
            return IndexOf(item.Id, item.Value) > -1;
        }

        public override bool Contains(V item)
        {
            return IndexOf(item) > -1;
        }

        public override bool Contains(long key, V item)
        {
            return IndexOf(key, item) > -1;
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

        public override V Dequeue()
        {
            ISeriesItem<V> _output = Next(first);
            if (_output == null)
                return default(V);

            if (repeatable && (_output.Next != null))
                _output = swapRepeated(_output);
            else
                first = _output;

            _output.Removed = true;
            removedIncrement();
            return _output.Value;
        }

        public override ISeriesItem<V> EmptyItem()
        {
            return new SeriesItem<V>();
        }

        public override ISeriesItem<V>[] EmptyTable(int size)
        {
            return new SeriesItem<V>[size];
        }

        public virtual ISeriesItem<V>[] EmptyVector(int size)
        {
            return new SeriesItem<V>[size];
        }

        public override void Flush()
        {
            base.Flush();
            vector = EmptyVector(size);
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

        public override int IndexOf(V item)
        {
            return IndexOf(unique.Key(item), item);
        }

        public override ISeriesItem<V> NewItem(ISeriesItem<V> item)
        {
            return new SeriesItem<V>(item);
        }

        public override ISeriesItem<V> NewItem(V value)
        {
            return new SeriesItem<V>(value);
        }

        public override ISeriesItem<V> NewItem(object key, V value)
        {
            return new SeriesItem<V>(key, value);
        }

        public override ISeriesItem<V> NewItem(long key, V value)
        {
            return new SeriesItem<V>(key, value);
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

        public override bool TryDequeue(out V output)
        {
            output = default(V);
            if (count < mincount)
                return false;

            ISeriesItem<V> _output = Next(first);
            if (_output == null)
                return false;

            if (repeatable && (_output.Next != null))
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

            if (repeatable && (output.Next != null))
            {
                output = swapRepeated(output);
            }
            else
                first = output;

            output.Removed = true;
            removedIncrement();
            return true;
        }

        public virtual bool TryRemove(long key, V item)
        {
            V output = InnerRemove(key, item);
            return (output != null) ? true : false;
        }

        public override ISeriesItem<V> First => first;

        public override bool IsRepeatable => repeatable;

        public override ISeriesItem<V> Last => vector[(count + removed) - 1];
    }
}
