// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Domain.Entities
{
    /// <summary>
    /// The contact organization.
    /// </summary>
    public class Organization : SDK.Service.Access.Identity.Organization, IEntity
    {
        public IdentifierType OrganizationIdentifierType { get; set; }

        public string? OrganizationIdentifier { get; set; }

        public long? SupplierId { get; set; }
        public virtual Supplier? Supplier { get; set; }

    }
}
