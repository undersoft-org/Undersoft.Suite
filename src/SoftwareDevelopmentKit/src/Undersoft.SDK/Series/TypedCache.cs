using Undersoft.SDK.Uniques;
using System.Runtime.Caching;

namespace Undersoft.SDK.Series;

using Base;
using Invoking;
using Rubrics;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;

public class TypedCache<V> : TypedRegistryBase<V> where V : IIdentifiable
{
    private readonly Catalog<Timer> timers = new Catalog<Timer>();

    private TimeSpan duration;
    private IInvoker callback;

    private void setupExpiration(TimeSpan? lifetime, IInvoker callback)
    {
        duration = (lifetime != null) ? lifetime.Value : TimeSpan.FromMinutes(15);
        if (callback != null)
            this.callback = callback;
    }

    public TypedCache(
        IEnumerable<V> collection,
        TimeSpan? lifeTime = null,
        IInvoker callback = null,
        int capacity = 17
    ) : base(collection, capacity)
    {
        setupExpiration(lifeTime, callback);
    }

    public TypedCache(
        IList<V> collection,
        TimeSpan? lifeTime = null,
        IInvoker callback = null,
        int capacity = 17
    ) : base(collection, capacity)
    {
        setupExpiration(lifeTime, callback);
    }

    public TypedCache(TimeSpan? lifeTime = null, IInvoker callback = null, int capacity = 17)
        : base(capacity)
    {
        setupExpiration(lifeTime, callback);
    }

    public override ISeriesItem<V> EmptyItem()
    {
        return new CacheSeriesItem<V>();
    }

    public override ISeriesItem<V>[] EmptyTable(int size)
    {
        return new CacheSeriesItem<V>[size];
    }

    public override ISeriesItem<V>[] EmptyVector(int size)
    {
        return new CacheSeriesItem<V>[size];
    }

    public override ISeriesItem<V> NewItem(ISeriesItem<V> item)
    {
        return new CacheSeriesItem<V>(item, duration, callback);
    }

    public override ISeriesItem<V> NewItem(object key, V value)
    {
        return new CacheSeriesItem<V>(key, value, duration, callback);
    }

    public override ISeriesItem<V> NewItem(long key, V value)
    {
        return new CacheSeriesItem<V>(key, value, duration, callback);
    }

    public override ISeriesItem<V> NewItem(V value)
    {
        return new CacheSeriesItem<V>(value, duration, callback);
    }

    protected virtual ITypedSeries<IIdentifiable> cache { get; set; }

    protected virtual T InnerMemorize<T>(T item) where T : IIdentifiable
    {
        int group = GetDataTypeId(typeof(T));
        if (!cache.TryGet(group, out IIdentifiable catalog))
        {
            ProxyGenerator proxy = ProxyGeneratorFactory.GetOrCreateGenerator(GetDataType(typeof(T)), group);
            proxy.Generate();

            IRubrics keyrubrics = proxy.Rubrics.KeyRubrics;

            IProxy iproxy = item.ToProxy();

            catalog = new Registry<T>();

            foreach (MemberRubric keyRubric in keyrubrics)
            {
                Registry<IIdentifiable> subcatalog = new Registry<IIdentifiable>();

                subcatalog.Add(item);

                ((ITypedSeries<IIdentifiable>)catalog).Put(
                    iproxy[keyRubric.RubricId],
                    keyRubric.RubricName.UniqueKey32(),
                    subcatalog
                );
            }

            cache.Add(group, catalog);

            cache.Add(item);

            return item;
        }

        if (!cache.ContainsKey(item))
        {
            ITypedSeries<IIdentifiable> _catalog = (ITypedSeries<IIdentifiable>)catalog;

            IProxy iproxy = item.ToProxy();

            foreach (MemberRubric keyRubric in iproxy.Rubrics.KeyRubrics)
            {
                if (
                    !_catalog.TryGet(
                        iproxy[keyRubric.RubricId],
                        keyRubric.RubricName.UniqueKey32(),
                        out IIdentifiable outcatalog
                    )
                )
                {
                    outcatalog = new Registry<IIdentifiable>();

                    ((ISeries<IIdentifiable>)outcatalog).Put(item);

                    _catalog.Put(
                        iproxy[keyRubric.RubricId],
                        keyRubric.RubricName.UniqueKey32(),
                        outcatalog
                    );
                }
                else
                {
                    ((ISeries<IIdentifiable>)outcatalog).Put(item);
                }
            }
            cache.Add(item);
        }

        return item;
    }

    protected virtual T InnerMemorize<T>(T item, params string[] names) where T : IIdentifiable
    {
        Memorize(item);

        IProxy proxy = item.ToProxy();

        MemberRubric[] keyrubrics = proxy.Rubrics
            .Where(p => names.Contains(p.RubricName))
            .ToArray();

        ITypedSeries<IIdentifiable> _catalog =
            (ITypedSeries<IIdentifiable>)cache.Get((ulong)item.TypeId);

        foreach (MemberRubric keyRubric in keyrubrics)
        {
            if (
                !_catalog.TryGet(
                    proxy[keyRubric.RubricId],
                    keyRubric.RubricName.UniqueKey32(),
                    out IIdentifiable outcatalog
                )
            )
            {
                outcatalog = new Registry<IIdentifiable>();

                ((ISeries<IIdentifiable>)outcatalog).Put(item);

                _catalog.Put(
                    proxy[keyRubric.RubricId],
                    keyRubric.RubricName.UniqueKey32(),
                    outcatalog
                );
            }
            else
            {
                ((ISeries<IIdentifiable>)outcatalog).Put(item);
            }
        }

        return item;
    }

    public virtual ITypedSeries<IIdentifiable> CacheSet<T>() where T : IIdentifiable
    {
        if (cache.TryGet(GetDataTypeId(typeof(T)), out IIdentifiable catalog))
            return (ITypedSeries<IIdentifiable>)catalog;
        return null;
    }

    public virtual T Lookup<T>(object key) where T : IIdentifiable
    {
        var seed = GetDataTypeId(typeof(T));

        if (cache.TryGet(key, seed, out IIdentifiable output))
            return (T)output;

        return default;
    }

    public virtual T Lookup<T>(params object[] keys) where T : IIdentifiable
    {
        var seed = GetDataTypeId(typeof(T));
        object key = keys;
        while (true)
        {
            if (!cache.TryGet(key, seed, out IIdentifiable output))
            {
                if (key.GetType().IsArray && ((object[])key).Length == 1)
                {
                    key = ((object[])key)[0];
                }
                else
                    break;
            }
            else
                return (T)output;
        }

        return default;
    }

    public virtual ISeries<IIdentifiable> Lookup<T>(Tuple<string, object> valueNamePair)
        where T : IIdentifiable
    {
        return Lookup<T>(
            (m) =>
                (ISeries<IIdentifiable>)
                    m.Get(valueNamePair.Item2, valueNamePair.Item2.UniqueKey32())
        );
    }

    public virtual ISeries<IIdentifiable> Lookup<T>(
        Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>> selector
    ) where T : IIdentifiable
    {
        return selector(CacheSet<T>());
    }

    public virtual T Lookup<T>(T item) where T : IIdentifiable
    {
        IProxy shell = item.ToProxy();
        IRubrics mrs = shell.Rubrics.KeyRubrics;
        T[] result = new T[mrs.Count];
        int i = 0;
        if (cache.TryGet(GetDataTypeId(typeof(T)), out IIdentifiable catalog))
        {
            foreach (MemberRubric mr in mrs)
            {
                if (
                    ((ITypedSeries<IIdentifiable>)catalog).TryGet(
                        shell[mr.RubricId],
                        mr.RubricName.UniqueKey32(),
                        out IIdentifiable outcatalog
                    )
                )
                    if (((ISeries<IIdentifiable>)outcatalog).TryGet(item, out IIdentifiable output))
                        result[i++] = (T)output;
            }
        }

        if (result.Any(r => r == null))
            return default;
        return result[0];
    }

    public virtual T[] Lookup<T>(object[] key, params Tuple<string, object>[] valueNamePairs)
        where T : IIdentifiable
    {
        return Lookup<T>(
            (k) => k[key],
            valueNamePairs
                .ForEach(
                    (vnp) =>
                        new Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>>(
                            (m) => (ISeries<IIdentifiable>)m.Get(vnp.Item2, vnp.Item1.UniqueKey32())
                        )
                )
                .ToArray()
        );
    }

    public virtual T[] Lookup<T>(
        Func<ISeries<IIdentifiable>, IIdentifiable> key,
        params Func<ITypedSeries<IIdentifiable>, ISeries<IIdentifiable>>[] selectors
    ) where T : IIdentifiable
    {
        if (cache.TryGet(GetDataTypeId(typeof(T)), out IIdentifiable catalog))
        {
            T[] result = new T[selectors.Length];
            for (int i = 0; i < selectors.Length; i++)
            {
                result[i] = (T)key(selectors[i]((ITypedSeries<IIdentifiable>)catalog));
            }
            return result;
        }

        return default;
    }

    public virtual ISeries<IIdentifiable> Lookup<T>(object key, string propertyNames)
        where T : IIdentifiable
    {
        if (CacheSet<T>().TryGet(key, propertyNames.UniqueKey32(), out IIdentifiable outcatalog))
            return (ISeries<IIdentifiable>)outcatalog;
        return default;
    }

    public virtual T Lookup<T>(T item, params string[] propertyNames) where T : IIdentifiable
    {
        IProxy ilValuator = item.ToProxy();
        MemberRubric[] mrs = ilValuator.Rubrics
            .Where(p => propertyNames.Contains(p.RubricName))
            .ToArray();
        T[] result = new T[mrs.Length];

        if (cache.TryGet(GetDataTypeId(typeof(T)), out IIdentifiable catalog))
        {
            int i = 0;
            foreach (MemberRubric mr in mrs)
            {
                if (
                    ((ITypedSeries<IIdentifiable>)catalog).TryGet(
                        ilValuator[mr.RubricId],
                        mr.RubricName.UniqueKey32(),
                        out IIdentifiable outcatalog
                    )
                )
                    if (((ISeries<IIdentifiable>)outcatalog).TryGet(item, out IIdentifiable output))
                        result[i++] = (T)output;
            }
        }

        if (result.Any(r => r == null))
            return default;
        return result[0];
    }

    public virtual IEnumerable<T> Memorize<T>(IEnumerable<T> items) where T : IIdentifiable
    {
        return items.ForEach(p => Memorize(p));
    }

    public virtual T Memorize<T>(T item) where T : IIdentifiable
    {
        return InnerMemorize(item);
    }

    public virtual T Memorize<T>(T item, params string[] names) where T : IIdentifiable
    {
        if (InnerMemorize(item) != null)
            return InnerMemorize(item, names);
        return default(T);
    }

    public virtual async Task<T> MemorizeAsync<T>(T item) where T : IIdentifiable
    {
        return await Task.Run(() => Memorize(item));
    }

    public virtual async Task<T> MemorizeAsync<T>(T item, params string[] names)
        where T : IIdentifiable
    {
        return await Task.Run(() => Memorize(item, names));
    }

    public virtual ITypedSeries<IIdentifiable> Catalog => cache;

    public virtual Type GetDataType(object obj)
    {
        return obj.GetType();
    }

    public virtual Type GetDataType(Type obj)
    {
        return obj;
    }

    public virtual int GetDataTypeId(object obj)
    {
        return obj.GetType().UniqueKey32();
    }

    public virtual int GetDataTypeId(Type obj)
    {
        return obj.UniqueKey32();
    }
}
