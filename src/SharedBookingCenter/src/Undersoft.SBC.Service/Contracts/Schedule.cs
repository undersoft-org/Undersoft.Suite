using System.Runtime.Serialization;
using Undersoft.SBC.Service.Contracts.Base;

namespace Undersoft.SBC.Service.Contracts;

[DataContract]
public class Schedule : ContractBase<Schedule, Detail, Setting, Group>
{
    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}
