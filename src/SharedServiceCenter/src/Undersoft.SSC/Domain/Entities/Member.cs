namespace Undersoft.SSC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SSC.Domain.Entities.Enums;

public class Member : OpenEntity<Member, Detail, Setting, MemberGroup>
{
    public virtual EntitySet<Member>? RelatedFrom { get; set; }

    public virtual EntitySet<Member>? RelatedTo { get; set; }

    public virtual EntitySet<Application>? Applications { get; set; }

    public virtual EntitySet<Service>? Services { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public long? DefaultId { get; set; }
    public virtual Default? Default { get; set; }

    [ForeignKey(nameof(Location))]
    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
