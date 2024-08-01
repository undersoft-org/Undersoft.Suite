using System.Runtime.Serialization;
using Undersoft.SSC.Domain.Entities;

namespace Undersoft.SSC.Service.Contracts.Base;

[DataContract]
public class ApplicationBase : ContractBase<ApplicationBase, Detail, Setting, Group>
{
    [DataMember(Order = 22)]
    public virtual ObjectSet<Member>? Members { get; set; }
}
