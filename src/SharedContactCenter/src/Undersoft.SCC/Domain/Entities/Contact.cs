// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Enums;

namespace Undersoft.SCC.Domain.Entities
{
    /// <summary>
    /// The contact.
    /// </summary>
    public class Contact : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>A string?</value>
        public virtual string? Name { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>A string?</value>
        public virtual string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>A ContactType</value>
        public virtual ContactType Type { get; set; }

        /// <summary>
        /// Gets or sets the personal id.
        /// </summary>
        /// <value>A long?</value>
        public long? PersonalId { get; set; }

        /// <summary>
        /// Gets or sets the personal.
        /// </summary>
        /// <value>A ContactPersonal?</value>
        public virtual ContactPersonal? Personal { get; set; } = default!;

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        /// <value>A long?</value>
        public long? AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>A ContactAddress?</value>
        public virtual ContactAddress? Address { get; set; } = default!;

        /// <summary>
        /// Gets or sets the professional id.
        /// </summary>
        /// <value>A long?</value>
        public long? ProfessionalId { get; set; }

        /// <summary>
        /// Gets or sets the professional.
        /// </summary>
        /// <value>A ContactProfessional?</value>
        public virtual ContactProfessional? Professional { get; set; } = default!;

        /// <summary>
        /// Gets or sets the organization id.
        /// </summary>
        /// <value>A long?</value>
        public long? OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>A ContactOrganization?</value>
        public virtual ContactOrganization? Organization { get; set; } = default!;

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>An EntitySet&lt;Group&gt;?</value>
        public virtual EntitySet<Group>? Groups { get; set; }
    }

}
