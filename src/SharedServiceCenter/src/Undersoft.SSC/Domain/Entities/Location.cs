using Undersoft.SSC.Domain.Entities.Enums;
using Undersoft.SSC.Domain.Entities.Locations;

namespace Undersoft.SSC.Domain.Entities
{
    public class Location : DataObject
    {
        public virtual string? Name { get; set; }

        public virtual LocaleType LocaleType { get; set; }

        public virtual string? Email { get; set; }

        public virtual PhoneType PhoneType { get; set; }

        public virtual string? PhoneNumber { get; set; }

        public virtual string? Notices { get; set; }

        public virtual EntitySet<Endpoint>? Endpoints { get; set; }

        public virtual EntitySet<Position>? Positions { get; set; }

        public virtual long? MemberId { get; set; }
        public virtual Member? Member { get; set; }

        public virtual long? ActivityId { get; set; }
        public virtual Activity? Activity { get; set; }

        public virtual long? ResourceId { get; set; }
        public virtual Resource? Resource { get; set; }

        public virtual long? ScheduleId { get; set; }
        public virtual Schedule? Schedule { get; set; }

        public virtual long? ServiceId { get; set; }
        public virtual Service? Service { get; set; }

        public virtual long? ApplicationId { get; set; }
        public virtual Application? Application { get; set; }
    }
}
