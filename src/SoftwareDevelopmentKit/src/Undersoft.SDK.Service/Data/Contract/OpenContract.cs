using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Contract;

using Identifier;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class OpenContract<TContract, TDetail, TSetting, TGroup> : DataObject, IContract
    where TContract : IDataObject
    where TDetail : class, IDetail
    where TSetting : class, ISetting
    where TGroup : struct, Enum
{
    public OpenContract() : base() { }

    [DataMember(Order = 12)]
    public virtual IdentifierSet<TContract> Identifiers { get; set; }

    [Details]
    [DataMember(Order = 13)]
    public virtual ObjectSet<TDetail> Details { get; set; }

    [Settings]
    [DataMember(Order = 14)]
    public virtual ObjectSet<TSetting> Settings { get; set; }

    [DataMember(Order = 15)]
    public virtual TGroup Group { get; set; }
}
