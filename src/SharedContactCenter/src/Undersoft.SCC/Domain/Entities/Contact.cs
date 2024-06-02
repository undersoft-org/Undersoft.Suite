using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;

namespace Undersoft.SCC.Domain.Entities
{
    public class Contact : Entity
    {
        public virtual string? Name { get; set; }

        public virtual string? Notes { get; set; }

        public virtual ContactType Type { get; set; }

        public long? PersonalId { get; set; }

        public virtual ContactPersonal? Personal { get; set; } = default!;

        public long? AddressId { get; set; }

        public virtual ContactAddress? Address { get; set; } = default!;

        public long? ProfessionalId { get; set; }

        public virtual ContactProfessional? Professional { get; set; } = default!;

        public long? OrganizationId { get; set; }

        public virtual ContactOrganization? Organization { get; set; } = default!;

        public virtual EntitySet<Group>? Groups { get; set; }
    }

}
