namespace Undersoft.SBC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;

public class Service : OpenEntity<Service, Detail, Setting, Group>
{
    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    [ForeignKey(nameof(Location))]
    public virtual long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
