using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Undersoft.SDK.Service.Data.Entity;

using Identifier;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;

[DataContract]
[StructLayout(LayoutKind.Sequential)]
public class OpenEntity<TEntity, TDetail, TSetting, TGroup> : Entity
    where TEntity : IDataObject
    where TDetail : class, IDetail
    where TSetting : class, ISetting
    where TGroup : struct, Enum
{
    public OpenEntity() : base() { }

    [DataMember(Order = 12)]
    public virtual IdentifierSet<TEntity> Identifiers { get; set; }

    [Details]
    [DataMember(Order = 13)]
    public virtual EntitySet<TDetail> Details { get; set; }

    [Settings]
    [DataMember(Order = 14)]
    public virtual EntitySet<TSetting> Settings { get; set; }

    [DataMember(Order = 15)]
    public virtual TGroup Group { get; set; }
}
