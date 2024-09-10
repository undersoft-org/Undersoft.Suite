using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Schedule : ContractBase<Schedule, Detail, Setting, Group>
{
    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}
