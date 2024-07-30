namespace Undersoft.SDK.Proxies;

using Undersoft.SDK.Utilities;
using Uniques;

public static class ProxyFactoryExtensions
{
    public static ProxyGenerator GetProxyGenerator(this object item)
    {
        var t = item.GetType();
        var key = t.UniqueKey32();
        if (!ProxyGeneratorFactory.Cache.TryGet(key, out ProxyGenerator proxy))
        {
            ProxyGeneratorFactory.Cache.Add(key, proxy = new ProxyGenerator(t));
        }
        return proxy;
    }

    public static ProxyGenerator GetProxyGenerator<T>(this T item)
    {
        var t = typeof(T);
        var key = t.UniqueKey32();
        if (!ProxyGeneratorFactory.Cache.TryGet(key, out ProxyGenerator proxy))
        {
            ProxyGeneratorFactory.Cache.Add(key, proxy = new ProxyGenerator(t));
        }
        return proxy;
    }

    public static IProxy ToProxy(this object item)
    {
        return ProxyFactory.CreateProxy(item);
    }

    public static IProxy ToProxy<T>(this T item)
    {
        Type t = typeof(T);
        if (t.IsInterface)
            return ProxyFactory.CreateProxy((object)item);

        return ProxyFactory.CreateProxy(item);
    }

    public static IProxy ToProxy(this Type type)
    {
        return ProxyFactory.CreateProxy(type.New());
    }
}
