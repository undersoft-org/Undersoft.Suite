using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SDK.Service.Data.Object.Setting;
using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Contracts.Base;

[DataContract]
public class ServiceBase : ContractBase<ServiceBase, Detail, Setting, MemberGroup>
{
    [DataMember(Order = 22)]
    public virtual ObjectSet<Member>? Members { get; set; }
}
