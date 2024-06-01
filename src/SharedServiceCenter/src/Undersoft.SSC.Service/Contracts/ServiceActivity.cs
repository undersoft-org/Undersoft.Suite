using System.Runtime.Serialization;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SSC.Service.Contracts.Base;

namespace Undersoft.SSC.Service.Contracts;

[DataContract]
public class ServiceActivity : ActivityBase
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

    [DataMember(Order = 23)]
    public virtual Default? Default { get; set; }

    [DataMember(Order = 24)]
    public virtual Location? Location { get; set; }
}
