// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

namespace Undersoft.SVC.Domain.Entities.Catalogs
{
    /// <summary>
    /// The contact address.
    /// </summary>
    public class Supplier : Entity
    {
        public virtual string? Notes { get; set; }

        public virtual long? OrganizationId { get; set; }

        public virtual Organization? Organization { get; set; }

        public virtual long? AddresslId { get; set; }

        public virtual Address? Address { get; set; }

        public virtual long? ProfessionalId { get; set; }

        public virtual Professional? Professional { get; set; }
    }
}