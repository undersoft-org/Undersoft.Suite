namespace Undersoft.SSC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;

public class Activity : OpenEntity<Activity, Detail, Setting, Group>
{
    public virtual EntitySet<Activity>? RelatedFrom { get; set; }

    public virtual EntitySet<Activity>? RelatedTo { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public long? DefaultId { get; set; }
    public virtual Default? Default { get; set; }

    [ForeignKey(nameof(Location))]
    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
