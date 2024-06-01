namespace Undersoft.SDK.Proxies;

using Undersoft.SDK.Series;
using Uniques;

public static class ProxyFactory
{
    public static ISeries<ProxyCreator> Cache = new Registry<ProxyCreator>();

    public static ProxyCreator GetCreator<T>()
    {
        return GetCreator(typeof(T));
    }

    public static ProxyCreator GetCreator(Type type)
    {
        return GetCreator(type, type.UniqueKey32());
    }

    public static ProxyCreator GetCreator(Type type, long key)
    {
        if (!Cache.TryGet(key, out ProxyCreator sleeve))
        {
            Cache.Add(key, sleeve = new ProxyCreator(type));
        }
        return sleeve;
    }

    public static ProxyCreator GetCompiledCreator<T>()
    {
        var sleeve = GetCreator<T>();
        sleeve.Create();
        return sleeve;
    }

    public static ProxyCreator GetCompiledCreator(Type type)
    {
        var sleeve = GetCreator(type);
        sleeve.Create();
        return sleeve;
    }

    public static ProxyCreator GetCompiledCreator(object item)
    {
        var sleeve = item.GetProxyCreator();
        sleeve.Create(item);
        return sleeve;
    }

    public static ProxyCreator GetCompiledCreator<T>(T item)
    {
        var sleeve = item.GetProxyCreator();
        sleeve.Create(item);
        return sleeve;
    }

    public static IProxy Create(object item)
    {
        var t = item.GetType();
        if (!TryGetProxy(item, t, out var proxy))
        {
            var key = t.UniqueKey32();
            if (!Cache.TryGet(key, out ProxyCreator sleeve))
                Cache.Add(key, sleeve = new ProxyCreator(t));

            return sleeve.Create(item);
        }
        return proxy;
    }

    public static IProxy Create<T>(T item)
    {
        var t = typeof(T);
        if (!TryGetProxy(item, t, out var proxy))
        {
            var key = t.UniqueKey32();
            if (!Cache.TryGet(key, out ProxyCreator sleeve))
                Cache.Add(key, sleeve = new ProxyCreator<T>());

            return sleeve.Create(item);
        }
        return proxy;
    }

    private static bool TryGetProxy(object item, Type type, out IProxy proxy)
    {
        var t = type;
        if (t.IsAssignableTo(typeof(IProxy)))
        {
            proxy = (IProxy)item;
            return true;
        }
        else if (t.IsAssignableTo(typeof(IInnerProxy)))
        {
            proxy = ((IInnerProxy)item).Proxy;
            if (proxy != null)
                return true;
        }
        proxy = null;
        return false;
    }
}
