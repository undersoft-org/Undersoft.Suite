// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;

namespace Undersoft.SVC.Domain.Entities
{
    /// <summary>
    /// The contact professional.
    /// </summary>
    public class Professional : SDK.Service.Access.Identity.Professional, IEntity
    {
        public string? ProfessionalImage { get; set; }

        public string? ProfessionalManager { get; set; }

        public string? ProfessionalName { get; set; }

        public string? ProfessionalPosition { get; set; }

        public byte[]? ProfessionalImageData { get; set; }

        public long? SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }
    }
}
