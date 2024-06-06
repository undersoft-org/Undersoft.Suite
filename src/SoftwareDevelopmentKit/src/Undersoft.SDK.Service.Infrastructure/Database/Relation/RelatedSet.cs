using System.Collections.ObjectModel;

namespace Undersoft.SDK.Service.Infrastructure.Database.Relation;

using Undersoft.SDK.Proxies;

public class RelatedSet<TEntity> : KeyedCollection<long, TEntity>, IFindable where TEntity : class, IOrigin, IInnerProxy
{
    public RelatedSet() : base()
    {

    }
    public RelatedSet(IEnumerable<TEntity> list)
    {
        foreach (var item in list)
            Add(item);
    }

    protected override long GetKeyForItem(TEntity item)
    {
        return item.Id == 0 ? item.AutoId() : item.Id;
    }

    public TEntity Single
    {
        get => this.FirstOrDefault();
    }

    public object this[object key]
    {
        get
        {
            TryGetValue(key.UniqueKey64(), out TEntity result);
            return result;
        }
        set
        {
            Dictionary[key.UniqueKey64()] = (TEntity)value;
        }
    }
}