using System.Runtime.Serialization;
using Undersoft.SSC.Domain.Entities.Enums;

namespace Undersoft.SSC.Service.Contracts.Base;


[DataContract]
public class ResourceBase : ContractBase<ResourceBase, Detail, Setting, ResourceGroup>
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
}
