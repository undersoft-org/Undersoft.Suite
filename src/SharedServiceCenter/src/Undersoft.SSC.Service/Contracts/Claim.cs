using System.Runtime.Serialization;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SSC.Service.Contracts;

[DataContract]
public class Claim : InnerProxy, IClaim
{
    [DataMember(Order = 6)]
    public virtual string? ClaimType { get; set; }

    [DataMember(Order = 7)]
    public virtual string? ClaimValue { get; set; }
}
