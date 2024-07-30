using System.ComponentModel;
using System.Linq.Expressions;
using Undersoft.SDK.Rubrics;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Proxies;

public class Proxy<T> : Proxy, IProxy<T> where T : class
{
    public Proxy(T target)
    {
        this.target = target;
        CreateProxy();
    }

    public Proxy(T target, Action<IInnerProxy, T> compilationAction)
    {
        this.target = target;
        CreateProxy(compilationAction);
    }

    public virtual object this[Expression<Func<T, object>> member]
    {
        get => this[member.GetMemberName()];
        set => this[member.GetMemberName()] = value;
    }

    public new T Target { get => (T)proxy.Target; set => proxy.Target = value; }

    protected virtual void CreateProxy(Action<IInnerProxy, T> compilationAction)
    {
        compilationAction.Invoke(this, (T)target);
    }

    protected override IProxy CreateProxy()
    {
        Type type = typeof(T);

        if (type.IsAssignableTo(typeof(IProxy)))
            return (IProxy)target;

        return proxy ??= ProxyFactory.CreateProxy<T>(target);
    }
}

public class Proxy : InnerProxy, IProxy
{
    protected object target;

    public Proxy() { }

    public Proxy(object target)
    {
        this.target = target;
        CreateProxy();
    }

    public Proxy(object target, Func<InnerProxy, IProxy> compilationAction)
    {
        this.target = target;
        CreateProxy(compilationAction);
    }

    public object this[int fieldId]
    {
        get => proxy[fieldId];
        set => proxy[fieldId] = value;
    }

    public object this[string propertyName]
    {
        get => proxy[propertyName];
        set => proxy[propertyName] = value;
    }

    public IRubrics Rubrics
    {
        get => proxy.Rubrics;
        set => proxy.Rubrics = value;
    }

    public IRubrics Changes
    {
        get => proxy.Changes;
        set => proxy.Changes = value;
    }

    public virtual object Target { get => proxy.Target; set => proxy.Target = value; }

    public Uscn Code
    {
        get => ((IInnerProxy)this).Proxy.Code;
        set => ((IInnerProxy)this).Proxy.Code = value;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected override void CreateProxy(Func<InnerProxy, IProxy> compilationAction)
    {
        proxy = compilationAction.Invoke(this);
    }

    protected override IProxy CreateProxy()
    {
        Type type = target.GetType();

        if (type.IsAssignableTo(typeof(IProxy)))
            return (IProxy)target;

        return proxy ??= ProxyFactory.CreateProxy(target);
    }
}
