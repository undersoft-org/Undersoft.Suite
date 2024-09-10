namespace Undersoft.GDC.Domain.Entities;

using System.ComponentModel.DataAnnotations.Schema;
using Undersoft.SDK.Service.Data.Entity;

public class Member : OpenEntity<Member, Detail, Setting, Group>
{
    public virtual EntitySet<Member>? RelatedFrom { get; set; }

    public virtual EntitySet<Member>? RelatedTo { get; set; }

    public virtual EntitySet<Service>? Services { get; set; }

    [ForeignKey(nameof(Location))]
    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
