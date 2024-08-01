using Undersoft.SDK.Service.Data.Object.Group;

namespace Undersoft.SSC.Domain.Entities;

public class Group : ObjectGroup<Group>, IEntity, IGroup
{
    public virtual EntitySet<Group>? RelatedFrom { get; set; }

    public virtual EntitySet<Group>? RelatedTo { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public virtual EntitySet<Application>? Applications { get; set; }

    public virtual EntitySet<Service>? Services { get; set; }
}
