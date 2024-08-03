using System.Runtime.Serialization;
using Undersoft.GDC.Domain.Entities;

namespace Undersoft.GDC.Service.Contracts.Base;

[DataContract]
public class ApplicationBase : ContractBase<ApplicationBase, Detail, Setting, Group>
{
    [DataMember(Order = 22)]
    public virtual ObjectSet<Member>? Members { get; set; }
}
