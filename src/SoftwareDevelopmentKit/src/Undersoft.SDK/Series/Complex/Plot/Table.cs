using System.Collections.ObjectModel;

namespace Undersoft.SDK.Series.Complex
{
    public class Table<T> : KeyedCollection<long, T> where T : class, IIdentifiable
    {
        public Table() { }

        public Table(IEnumerable<T> list) { list.ForEach(item => base.Add(item)); }

        protected override long GetKeyForItem(T item)
        {
            return item.Id == 0 ? DateTime.UtcNow.Ticks.ToString().GetHashCode() : item.Id;
        }

        public T this[T item]
        {
            get
            {
                return this[item.Id];
            }
            set
            {
                Dictionary[item.Id] = value;
            }
        }
    }
}