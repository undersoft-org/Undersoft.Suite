namespace Undersoft.SDK.Proxies;

using Undersoft.SDK.Series;
using Uniques;

public static class ProxyGeneratorFactory
{
    public static ISeries<ProxyGenerator> Cache = new Registry<ProxyGenerator>();

    private static ProxyGenerator GetOrCreateGenerator<T>()
    {
        return GetOrCreateGenerator(typeof(T));
    }

    private static ProxyGenerator GetOrCreateGenerator(Type type)
    {
        return GetOrCreateGenerator(type, type.UniqueKey32());
    }

    public static ProxyGenerator GetOrCreateGenerator(Type type, long key)
    {
        if (!Cache.TryGet(key, out ProxyGenerator proxy))
        {
            Cache.Add(key, proxy = new ProxyGenerator(type));
        }
        return proxy;
    }

    public static ProxyGenerator CreateGenerator<T>()
    {
        var proxy = GetOrCreateGenerator<T>();
        proxy.Generate();
        return proxy;
    }

    public static ProxyGenerator CreateGenerator(Type type)
    {
        var proxy = GetOrCreateGenerator(type);
        proxy.Generate();
        return proxy;
    }

    public static ProxyGenerator CreateGenerator(object item)
    {
        var proxy = item.GetProxyGenerator();
        proxy.Generate(item);
        return proxy;
    }

    public static ProxyGenerator CreateGenerator<T>(T item)
    {
        var proxy = item.GetProxyGenerator();
        proxy.Generate(item);
        return proxy;
    }
}
