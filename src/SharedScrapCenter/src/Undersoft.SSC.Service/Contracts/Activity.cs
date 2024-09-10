using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Activity : ContractBase<Activity, Detail, Setting, Group>
{
    [DataMember(Order = 24)]
    public virtual Location? Location { get; set; }
}
