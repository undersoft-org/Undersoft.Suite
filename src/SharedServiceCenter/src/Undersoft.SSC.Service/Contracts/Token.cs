using System.Runtime.Serialization;
using Undersoft.SDK.Proxies;

namespace Undersoft.SSC.Service.Contracts;

[DataContract]
public class Token : InnerProxy
{
    [DataMember(Order = 6)]
    public virtual long UserId { get; set; }

    [DataMember(Order = 7)]
    public virtual string? LoginProvider { get; set; }

    [DataMember(Order = 8)]
    public virtual string? Name { get; set; }

    [DataMember(Order = 9)]
    public virtual string? Value { get; set; }
}