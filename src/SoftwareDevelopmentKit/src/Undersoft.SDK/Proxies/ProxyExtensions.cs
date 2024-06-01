namespace Undersoft.SDK.Proxies;

public static class ProxyExtensions
{
    public static R ValueOf<T, R>(this T item, string name) where T : class
    {
        return (R)ProxyFactory.Create(item)[name];
    }

    public static R ValueOf<R>(this object item, string name)
    {
        return (R)ProxyFactory.Create(item)[name];
    }

    public static object ValueOf(this object item, string name)
    {
        return ProxyFactory.Create(item)[name];
    }

    public static R ValueOf<T, R>(this T item, int index) where T : class
    {
        return (R)ProxyFactory.Create(item)[index];
    }

    public static R ValueOf<R>(this object item, int index)
    {
        return (R)ProxyFactory.Create(item)[index];
    }

    public static object ValueOf(this object item, int index)
    {
        return ProxyFactory.Create(item)[index];
    }

    public static R ValueOf<T, R>(this T item, string name, R value) where T : class
    {
        return (R)(ProxyFactory.Create(item)[name] = value);
    }

    public static R ValueOf<R>(this object item, string name, R value)
    {
        return (R)(ProxyFactory.Create(item)[name] = value);
    }

    public static object ValueOf(this object item, string name, object value)
    {
        return ProxyFactory.Create(item)[name] = value;
    }

    public static R ValueOf<T, R>(this T item, int index, R value) where T : class
    {
        return (R)(ProxyFactory.Create(item)[index] = value);
    }

    public static R ValueOf<R>(this object item, int index, R value)
    {
        return (R)(ProxyFactory.Create(item)[index] = value);
    }

    public static object ValueOf(this object item, int index, object value)
    {
        return ProxyFactory.Create(item)[index] = value;
    }
}
