using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Object
{

    public class ObjectSet<TDto> : KeyedCollection<long, TDto>, IFindable where TDto : class, IOrigin
    {
        public ObjectSet() { }

        public ObjectSet(IEnumerable<TDto> list) { list.ForEach(item => base.Add(item)); }

        protected override long GetKeyForItem(TDto item)
        {
            return item.Id == 0 ? item.AutoId() : item.Id;
        }

        [IgnoreDataMember]
        public TDto Single
        {
            get => this.FirstOrDefault();
        }

        [IgnoreDataMember]
        public object this[object key]
        {
            get
            {
                TryGetValue((long)key.UniqueKey64(), out TDto result);
                return result;
            }
            set
            {
                Dictionary[(long)key.UniqueKey64()] = (TDto)value;
            }
        }
    }
}