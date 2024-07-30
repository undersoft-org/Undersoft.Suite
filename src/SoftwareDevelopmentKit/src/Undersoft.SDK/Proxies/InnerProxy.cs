using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Proxies;

using System.ComponentModel;
using Undersoft.SDK;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class InnerProxy : Origin, IInnerProxy
{
    public InnerProxy(bool autoId) : base(autoId) { }
    public InnerProxy() : base(true) { }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    internal IProxy proxy;

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    IProxy IInnerProxy.Proxy { get => proxy ??= CreateProxy(); }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    object IInnerProxy.this[string propertyName]
    {
        get => ((IInnerProxy)this).Proxy[propertyName];
        set => ((IInnerProxy)this).Proxy[propertyName] = value;
    }

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    object IInnerProxy.this[int id]
    {
        get => ((IInnerProxy)this).Proxy[id];
        set => ((IInnerProxy)this).Proxy[id] = value;
    }

    protected virtual void CreateProxy(Func<InnerProxy, IProxy> compileAction)
    {
        proxy = compileAction.Invoke(this);
    }

    protected virtual IProxy CreateProxy()
    {
        Type type = GetType();

        if (TypeId == 0)
            TypeId = type.UniqueKey32();

        if (type.IsAssignableTo(typeof(IProxy)))
            return (IProxy)this;

        var proxy = ProxyGeneratorFactory.GetOrCreateGenerator(type, TypeId).Generate(this);
        //proxy.PropertyChanged += OnPropertyChanged;

        return proxy;
    }

    protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        var _proxy = ((IInnerProxy)sender).Proxy;
        //_proxy.Changes.TryAdd(_proxy.Rubrics[args.PropertyName]);
    }
}
