namespace Undersoft.SDK.Proxies;

using Uniques;

public static class ProxyFactoryExtensions
{
    public static ProxyCreator GetProxyCreator(this object item)
    {
        var t = item.GetType();
        var key = t.UniqueKey32();
        if (!ProxyFactory.Cache.TryGet(key, out ProxyCreator sleeve))
        {
            ProxyFactory.Cache.Add(key, sleeve = new ProxyCreator(t));
        }
        return sleeve;
    }

    public static ProxyCreator GetProxyCreator<T>(this T item)
    {
        var t = typeof(T);
        var key = t.UniqueKey32();
        if (!ProxyFactory.Cache.TryGet(key, out ProxyCreator sleeve))
        {
            ProxyFactory.Cache.Add(key, sleeve = new ProxyCreator(t));
        }
        return sleeve;
    }

    public static IProxy ToProxy(this object item)
    {
        return ProxyFactory.Create(item);
    }

    public static IProxy ToProxy<T>(this T item)
    {
        Type t = typeof(T);
        if (t.IsInterface)
            return ProxyFactory.Create((object)item);

        return ProxyFactory.Create(item);
    }

    public static IProxy ToProxy(this Type type)
    {
        return ProxyFactory.Create(type.New());
    }
}
