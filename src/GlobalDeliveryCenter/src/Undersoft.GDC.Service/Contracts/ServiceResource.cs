using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class ServiceResource : ResourceBase
{
    [Sortable]
    [Filterable]
    [VisibleRubric]
    [DisplayRubric("Id")]
    [DataMember(Order = 1)]
    public override long Id { get => base.Id; set => base.Id = value; }

    [Sortable]
    [Filterable]
    [VisibleRubric]
    [RubricSize(256)]
    [DisplayRubric("Label")]
    [DataMember(Order = 11)]
    public override string? Label { get => base.Label; set => base.Label = value; }

    [DataMember(Order = 25)]
    public virtual ObjectSet<ResourceBase>? RelatedFrom { get; set; }

    [DataMember(Order = 26)]
    public virtual ObjectSet<ResourceBase>? RelatedTo { get; set; }

    [DataMember(Order = 27)]
    public virtual ObjectSet<MemberBase>? Members { get; set; }

    [DataMember(Order = 28)]
    public virtual ObjectSet<ActivityBase>? Activities { get; set; }

    [DataMember(Order = 29)]
    public virtual ObjectSet<ScheduleBase>? Schedules { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}
