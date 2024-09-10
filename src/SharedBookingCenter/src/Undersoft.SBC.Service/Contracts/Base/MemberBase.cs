using System.Runtime.Serialization;

namespace Undersoft.SBC.Service.Contracts.Base;

[DataContract]
public class MemberBase : ContractBase<MemberBase, Detail, Setting, Group>
{
    [DataMember(Order = 25)]
    public virtual ObjectSet<Service>? Services { get; set; }
}
