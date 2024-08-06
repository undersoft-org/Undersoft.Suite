using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Member : MemberBase
{
    [Sortable]
    [Filterable]
    [VisibleRubric]
    [DataMember(Order = 1)]
    public override long Id { get => base.Id; set => base.Id = value; }

    [Sortable]
    [Filterable]
    [VisibleRubric]
    [DataMember(Order = 11)]
    public override string? Label { get => base.Label; set => base.Label = value; }

    [DataMember(Order = 20)]
    public virtual ObjectSet<MemberBase>? RelatedFrom { get; set; }

    [DataMember(Order = 21)]
    public virtual ObjectSet<MemberBase>? RelatedTo { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}


