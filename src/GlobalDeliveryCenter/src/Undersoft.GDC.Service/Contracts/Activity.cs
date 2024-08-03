using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Activity : ActivityBase
{
    [DataMember(Order = 18)]
    public virtual ObjectSet<ActivityBase>? RelatedFrom { get; set; }

    [DataMember(Order = 19)]
    public virtual ObjectSet<ActivityBase>? RelatedTo { get; set; }

    [DataMember(Order = 20)]
    public virtual ObjectSet<MemberBase>? Members { get; set; }

    [DataMember(Order = 21)]
    public virtual ObjectSet<ResourceBase>? Resources { get; set; }

    [DataMember(Order = 22)]
    public virtual ObjectSet<ScheduleBase>? Schedules { get; set; }

    [DataMember(Order = 24)]
    public virtual Location? Location { get; set; }
}
