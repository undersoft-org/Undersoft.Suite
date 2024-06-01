namespace Undersoft.SDK.Series.Base
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Uniques;
    using Enumerators;

    [StructLayout(LayoutKind.Sequential)]
    public abstract class SeriesItemBase<V> : ISeriesItem<V>
    {
        protected V value;
        protected bool? isUnique;
        private bool disposedValue = false;
        private ISeriesItem<V> extended;
        private ISeriesItem<V> next;

        public SeriesItemBase() { }

        public SeriesItemBase(ISeriesItem<V> value) : base()
        {
            Set(value);
        }

        public SeriesItemBase(object key, V value) : base()
        {
            Set(key, value);
        }

        public SeriesItemBase(long key, V value) : base()
        {
            Set(key, value);
        }

        public SeriesItemBase(V value) : base()
        {
            Set(value);
        }

        public virtual IUnique Empty => throw new NotImplementedException();

        public virtual ISeriesItem<V> Extended
        {
            get => extended;
            set => extended = value;
        }

        public virtual int Index { get; set; } = -1;

        public abstract long Id { get; set; }

        public virtual ISeriesItem<V> Next
        {
            get => next;
            set => next = value;
        }

        public virtual bool Removed { get; set; }

        public virtual bool Repeated { get; set; }

        public virtual bool IsUnique
        {
            get => isUnique ??= typeof(V).IsAssignableTo(typeof(IIdentifiable));
            set => isUnique = value;
        }

        public virtual V UniqueValue
        {
            get => value;
            set => this.value = value;
        }

        public virtual long TypeId
        {
            get
            {
                if ((((IIdentifiable)value).TypeId != 0))
                    return ((IIdentifiable)value).TypeId;

                return typeof(V).UniqueKey32();
            }
            set
            {
                if (IsUnique)
                {
                    ((IIdentifiable)this.value).TypeId = value;
                }
            }
        }

        public virtual V Value
        {
            get => value;
            set => this.value = value;
        }  

        public virtual int CompareTo(ISeriesItem<V> other)
        {
            return (int)(Id - other.Id);
        }

        public virtual int CompareTo(IUnique other)
        {
            return (int)(Id - other.Id);
        }

        public abstract int CompareTo(object other);

        public virtual int CompareTo(long key)
        {
            return (int)(Id - key);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual bool Equals(ISeriesItem<V> y)
        {
            return this.Equals(y.Id);
        }

        public virtual bool Equals(IUnique other)
        {
            return Id == other.Id;
        }

        public override abstract bool Equals(object y);

        public virtual bool Equals(long key)
        {
            return Id == key;
        }

        public abstract byte[] GetBytes();

        public override abstract int GetHashCode();

        public abstract byte[] GetIdBytes();

        public virtual Type GetUniqueType()
        {
            return typeof(V);
        }

        public abstract void Set(ISeriesItem<V> item);

        public abstract void Set(object key, V value);

        public virtual void Set(long key, V value)
        {
            this.value = value;
            Id = key;
        }

        public abstract void Set(V value);

        public virtual ISeriesItem<V> MoveNext(ISeriesItem<V> item)
        {
            ISeriesItem<V> _item = item.Next;
            if (_item != null)
            {
                if (!_item.Removed)
                    return _item;
                return MoveNext(_item);
            }
            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Value = default(V);
                }

                disposedValue = true;
            }
        }

        IEnumerator<V> IEnumerable<V>.GetEnumerator()
        {
            return new SeriesItemSubEnumerator<V>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SeriesItemSubEnumerator<V>(this);
        }

        public virtual IEnumerable<V> AsValues()
        {
            return this;
        }

        public virtual IEnumerable<ISeriesItem<V>> AsItems()
        {
            foreach (ISeriesItem<V> item in this)
            {
                yield return item;
            }
        }

        public virtual IEnumerator<ISeriesItem<V>> GetEnumerator()
        {
            return new SeriesItemSubEnumerator<V>(this);
        }

        public bool Equals(IIdentifiable other)
        {
            return Id == other.Id && TypeId == other.TypeId;
        }

        public int CompareTo(IIdentifiable other)
        {
            return (int)((Id - other.Id) - (TypeId - other.TypeId));
        }
    }
}
