namespace Undersoft.SSC.Domain.Entities;

using Undersoft.SDK.Service.Data.Entity;

public class Resource : OpenEntity<Resource, Detail, Setting, Group>
{
    public virtual EntitySet<Resource>? RelatedFrom { get; set; }

    public virtual EntitySet<Resource>? RelatedTo { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public long? DefaultId { get; set; }
    public virtual Default? Default { get; set; }

    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
