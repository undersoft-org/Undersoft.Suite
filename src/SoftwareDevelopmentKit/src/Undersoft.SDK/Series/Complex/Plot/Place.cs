using System.Collections.ObjectModel;

namespace Undersoft.SDK.Series.Complex
{
    public class Place<T> : KeyedCollection<long, Place<T>>, IIdentifiable
        where T : IIdentifiable
    {
        public Place() { }

        public Place(T value)
        {
            Value = value;
            if (value.Id == 0)
                value.Id = DateTime.UtcNow.Ticks.ToString().GetHashCode();
        }

        public Place(int index, T value)
            : this(value)
        {
            Index = index;
        }

        public Place(int index, long id, T value)
        {
            Index = index;
            Value = value;
            if (id != 0 && id != value.Id)
                Id = id;
            else if (value.Id == 0)
                value.Id = DateTime.UtcNow.Ticks.ToString().GetHashCode();
        }

        public Place<T> this[T neighbor]
        {
            get { return this[neighbor.Id]; }
            set { Dictionary[neighbor.Id] = value; }
        }

        public int Index { get; set; } = -1;
        public long Id
        {
            get => Value.Id;
            set => Value.Id = value;
        }
        public long TypeId
        {
            get => Value.TypeId;
            set => Value.TypeId = value;
        }
        public T Value { get; set; }

        public Table<Metrics> Metrics { get; set; } = new Table<Metrics>();

        protected override long GetKeyForItem(Place<T> item)
        {
            return item.Id == 0 ? DateTime.UtcNow.Ticks.ToString().GetHashCode() : item.Id;
        }

        public int IndexOf(T item)
        {
            int index = -1;
            index = base.IndexOf(base[item.Id]);
            return index;
        }

        public override string ToString()
        {
            return $"Spot with index {Index}: {Value}, neighbors: {Count}";
        }
    }
}
