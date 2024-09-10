namespace Undersoft.GDC.Domain.Entities;

using Undersoft.SDK.Service.Data.Entity;

public class Resource : OpenEntity<Resource, Detail, Setting, Group>
{
    public virtual EntitySet<Service>? Services { get; set; }

    public long? LocationId { get; set; }
    public virtual Location? Location { get; set; }
}
