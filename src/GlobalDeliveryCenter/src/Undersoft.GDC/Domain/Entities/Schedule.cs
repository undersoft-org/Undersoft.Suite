namespace Undersoft.GDC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;

public class Schedule : OpenEntity<Schedule, Detail, Setting, Group>
{
    public virtual EntitySet<Schedule>? RelatedFrom { get; set; }

    public virtual EntitySet<Schedule>? RelatedTo { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    [ForeignKey(nameof(Location))]
    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
