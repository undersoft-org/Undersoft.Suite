using System.Runtime.Serialization;
using Undersoft.SSC.Service.Contracts.Base;

namespace Undersoft.SSC.Service.Contracts;

[DataContract]
public class Schedule : ScheduleBase
{
    [DataMember(Order = 20)]
    public virtual ObjectSet<ScheduleBase>? RelatedFrom { get; set; }

    [DataMember(Order = 21)]
    public virtual ObjectSet<ScheduleBase>? RelatedTo { get; set; }

    [DataMember(Order = 22)]
    public virtual ObjectSet<MemberBase>? Members { get; set; }

    [DataMember(Order = 23)]
    public virtual ObjectSet<ActivityBase>? Activities { get; set; }

    [DataMember(Order = 24)]
    public virtual ObjectSet<ResourceBase>? Resources { get; set; }

    [DataMember(Order = 17)]
    public virtual Default? Default { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}
