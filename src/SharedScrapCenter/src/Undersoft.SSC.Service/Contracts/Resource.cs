using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Resource : ContractBase<Resource, Detail, Setting, Group>
{
    [DataMember(Order = 20)]
    public virtual string? Path { get; set; }

    [DataMember(Order = 21)]
    public virtual string? Name { get; set; }

    [DataMember(Order = 22)]
    public virtual string? Type { get; set; }

    [DataMember(Order = 23)]
    public virtual byte[]? Data { get; set; }

    [DataMember(Order = 24)]
    public virtual string? DataUri { get; set; }

    [DataMember(Order = 25)]
    public virtual string? Info { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}
