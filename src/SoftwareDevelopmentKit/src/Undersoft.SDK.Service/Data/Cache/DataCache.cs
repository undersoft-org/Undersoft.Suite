namespace Undersoft.SDK.Service.Data.Cache;

using Invoking;
using Proxies;
using Rubrics;
using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Store;

public class DataCache : TypedCache<IIdentifiable>, IDataCache
{
    public DataCache() : this(TimeSpan.FromMinutes(15), null, 259)
    {
    }

    public DataCache(TimeSpan? lifeTime = null, IInvoker callback = null, int capacity = 259) : base(
        lifeTime,
        callback,
        capacity)
    {
        if (cache == null)
        {
            cache = this;
        }
    }

    protected override T InnerMemorize<T>(T item)
    {
        int group = typeof(T).GetDataTypeId();
        if (!cache.TryGet(group, out IIdentifiable deck))
        {
            deck = new TypedRegistry<IIdentifiable>();

            cache.Add(group, deck);

            ((ITypedSeries<IIdentifiable>)deck).Put(item, group);

            cache.Add(item, group);

            return item;
        }

        if (!cache.ContainsKey(item, group))
        {
            ((ITypedSeries<IIdentifiable>)deck).Put(item, group);

            cache.Add(item, group);
        }

        return item;
    }

    protected override T InnerMemorize<T>(T item, params string[] names)
    {
        Memorize(item);

        return item;
    }

    public static IRubrics AssignKeyRubrics(ProxyCreator proxy, IOrigin item)
    {
        if (!proxy.Rubrics.KeyRubrics.Any())
        {
            IEnumerable<bool[]>[] rk = item.GetIndentityProperties()
                .AsItems()
                .Select(
                    p => p.Id != (int)DbIdentityType.NONE
                        ? p.Value
                            .Select(
                                e => new[]
                                        {
                                            proxy.Rubrics[e.Name].IsKey = true,
                                            proxy.Rubrics[e.Name].IsIdentity = true
                                        })
                        : p.Value.Select(h => new[] { proxy.Rubrics[h.Name].IsIdentity = true }))
                .ToArray();

            proxy.Rubrics.KeyRubrics.Put(proxy.Rubrics.Where(p => p.IsIdentity == true).ToArray());
            proxy.Rubrics.Update();
        }

        return proxy.Rubrics.KeyRubrics;
    }

    public override T Memorize<T>(T item)
    {
        if (!item.IsEventType())
            return InnerMemorize(item);
        return default;
    }

    public override int GetDataTypeId(Type obj)
    {
        return obj.GetDataTypeId();
    }
    public override Type GetDataType(Type obj)
    {
        return obj.GetDataType();
    }

    public override int GetDataTypeId(object obj)
    {
        return obj.GetDataTypeId();
    }
    public override Type GetDataType(object obj)
    {
        return obj.GetDataType();
    }
}
