namespace Undersoft.GDC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;

public class Service : OpenEntity<Service, Detail, Setting, Group>
{
    public virtual EntitySet<Service>? RelatedFrom { get; set; }

    public virtual EntitySet<Service>? RelatedTo { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    [ForeignKey(nameof(Location))]
    public virtual long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
