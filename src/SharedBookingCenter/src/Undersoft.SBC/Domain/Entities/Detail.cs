using Undersoft.SBC.Domain.Entities.Enums;
using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SBC.Domain.Entities;

public class Detail : ObjectDetail<Detail, DetailKind>, IEntity
{
    public virtual EntitySet<Detail>? RelatedFrom { get; set; }

    public virtual EntitySet<Detail>? RelatedTo { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public virtual EntitySet<Service>? Services { get; set; }
}
