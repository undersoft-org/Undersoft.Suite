using System.Runtime.Serialization;
using Undersoft.GDC.Service.Contracts.Base;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.GDC.Service.Contracts;

[DataContract]
public class Service : ContractBase<Service, Detail, Setting, Group>, IContract
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

    [DataMember(Order = 22)]
    public virtual ObjectSet<Activity>? Activities { get; set; }

    [DataMember(Order = 23)]
    public virtual ObjectSet<Resource>? Resources { get; set; }

    [DataMember(Order = 24)]
    public virtual ObjectSet<Schedule>? Schedules { get; set; }

    [DataMember(Order = 19)]
    public virtual Location? Location { get; set; }
}


