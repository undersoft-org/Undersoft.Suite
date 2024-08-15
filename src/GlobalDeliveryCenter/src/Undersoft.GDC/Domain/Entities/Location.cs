using Undersoft.GDC.Domain.Entities.Enums;

namespace Undersoft.GDC.Domain.Entities
{
    public class Location : SDK.Service.Access.Identity.Location, IEntity
    {
        public virtual LocaleType LocaleType { get; set; }

        public virtual string? Email { get; set; }

        public virtual PhoneType PhoneType { get; set; }

        public virtual string? PhoneNumber { get; set; }

        public virtual string? Notices { get; set; }

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

        public virtual Listing<Address>? Addresses { get; set; }

        public virtual Listing<Place>? Places { get; set; }
    }
}
