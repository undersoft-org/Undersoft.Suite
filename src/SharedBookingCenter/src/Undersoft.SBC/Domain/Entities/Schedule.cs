namespace Undersoft.SBC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;

public class Schedule : OpenEntity<Schedule, Detail, Setting, Group>
{
    public virtual EntitySet<Service>? Services { get; set; }

    [ForeignKey(nameof(Location))]
    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
