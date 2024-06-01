using Undersoft.SDK.Serialization;
using Undersoft.SDK.Service.Data.Object.Detail;
using Undersoft.SSC.Domain.Entities.Enums;

namespace Undersoft.SSC.Domain.Entities;

public class Detail : ObjectDetail<Detail, DetailKind>, IEntity, ISerializableJsonDocument, IDetail
{
    public virtual EntitySet<Detail>? RelatedFrom { get; set; }

    public virtual EntitySet<Detail>? RelatedTo { get; set; }

    public virtual EntitySet<Activity>? Activities { get; set; }

    public virtual EntitySet<Member>? Members { get; set; }

    public virtual EntitySet<Resource>? Resources { get; set; }

    public virtual EntitySet<Schedule>? Schedules { get; set; }

    public virtual EntitySet<Application>? Applications { get; set; }

    public virtual EntitySet<Service>? Services { get; set; }

    public virtual long DefaultId { get; set; }
    public virtual Default? Default { get; set; }
}
