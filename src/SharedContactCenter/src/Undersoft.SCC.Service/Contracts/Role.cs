using System.Runtime.Serialization;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts;

[DataContract]
public class Role : InnerProxy, IRole, IContract
{
    [DataMember(Order = 6)]
    public virtual string? Name { get; set; }

    [DataMember(Order = 7)]
    public virtual string? NormalizedName { get; set; }

    [DataMember(Order = 8)]
    public ObjectSet<Claim>? Claims { get; set; }
}
