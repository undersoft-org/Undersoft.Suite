using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Object;

using Proxies;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class DataObject : InnerProxy, IDataObject
{
    public DataObject() : base() { }

    protected override IProxy CreateProxy()
    {
        Type type = this.GetDataType();
  
        TypeId = type.UniqueKey32();

        if (type.IsAssignableTo(typeof(IProxy)))
            return (IProxy)this;

        return ProxyGeneratorFactory.GetOrCreateGenerator(type, TypeId).Generate(this);
    }

}
