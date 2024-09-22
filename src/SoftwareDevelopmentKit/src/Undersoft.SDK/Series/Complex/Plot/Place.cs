using System.Collections.ObjectModel;
using Undersoft.SDK.Series.Base;

namespace Undersoft.SDK.Series.Complex
{
    public class Place<T> : RegistryBase<Place<T>>, IIdentifiable
        where T : IIdentifiable
    {
        public Place() : base() { }

        public Place(T value) : base()
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
            set { this[neighbor.Id] = value; }
        }

        public int Index { get; set; } = -1;
        public override long Id
        {
            get => Value.Id;
            set => Value.Id = value;
        }
        public override long TypeId
        {
            get => Value.TypeId;
            set => Value.TypeId = value;
        }
        public T Value { get; set; }

        public Registry<Metrics> Metrics { get; set; } = new Registry<Metrics>();

        public override string ToString()
        {
            return $"Spot with index {Index}: {Value}, neighbors: {Count}";
        }
    }
}
