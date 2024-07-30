namespace Undersoft.SDK.Proxies;

using Uniques;

public static class ProxyFactory
{
    public static IProxy CreateProxy(object item)
    {
        var t = item.GetType();
        if (!TryGetInnerProxy(item, t, out var proxy))
        {
            var key = t.UniqueKey32();
            if (!ProxyGeneratorFactory.Cache.TryGet(key, out ProxyGenerator _proxy))
                ProxyGeneratorFactory.Cache.Add(key, _proxy = new ProxyGenerator(t));

            return _proxy.Generate(item);
        }
        return proxy;
    }

    public static IProxy CreateProxy<T>(T item)
    {
        var t = typeof(T);
        if (!TryGetInnerProxy(item, t, out var proxy))
        {
            var key = t.UniqueKey32();
            if (!ProxyGeneratorFactory.Cache.TryGet(key, out ProxyGenerator _proxy))
                ProxyGeneratorFactory.Cache.Add(key, _proxy = new ProxyGenerator<T>());

            return _proxy.Generate(item);
        }
        return proxy;
    }

    public static IProxy CreateProxy<T>(object item)
    {
        var t = typeof(T);
        if (!TryGetInnerProxy(item, t, out var proxy))
        {
            var key = t.UniqueKey32();
            if (!ProxyGeneratorFactory.Cache.TryGet(key, out ProxyGenerator _proxy))
                ProxyGeneratorFactory.Cache.Add(key, _proxy = new ProxyGenerator<T>());

            return _proxy.Generate(item);
        }
        return proxy;
    }

    private static bool TryGetInnerProxy(object item, Type type, out IProxy proxy)
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
