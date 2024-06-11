using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Contract;

using Identifier;
using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Group;
using Undersoft.SDK.Service.Data.Object.Setting;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class OpenContract<TContract, TDetail, TSetting, TGroup> : DataObject, IContract
    where TContract : IDataObject
    where TDetail : class, IDetail
    where TSetting : class, ISetting
    where TGroup : class, IGroup
{
    public OpenContract() : base() { }

    [Extended]
    [AutoExpand]
    [DataMember(Order = 12)]
    public virtual IdentifierSet<TContract> Identifiers { get; set; }

    [Details]
    [Extended]
    [AutoExpand]
    [DataMember(Order = 13)]
    public virtual Listing<TDetail> Details { get; set; }

    [Settings]
    [Extended]
    [AutoExpand]
    [DataMember(Order = 14)]
    public virtual Listing<TSetting> Settings { get; set; }

    [Extended]
    [AutoExpand]
    [DataMember(Order = 15)]
    public virtual Listing<TGroup> Groups { get; set; }
}
