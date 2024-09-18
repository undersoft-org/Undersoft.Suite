// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Inventory;
using Undersoft.SVC.Domain.Entities.Vaccination;

namespace Undersoft.SVC.Domain.Entities.Catalogs
{
    public class Vaccine : Entity, IEntity
    {
        public virtual string? Notes { get; set; }

        public long? ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public long? SafetyId { get; set; }

        public virtual Safety? Safety { get; set; }

        public long? SpecificationId { get; set; }

        public virtual Specification? Specification { get; set; }

        public long? StockId { get; set; }

        public virtual Stock? Stock { get; set; }

        public virtual EntitySet<Campaign>? Campaigns { get; set; }

        public virtual EntitySet<Procedure>? Procedures { get; set; }

        public virtual EntitySet<Certificate>? Certificates { get; set; }

        public virtual EntitySet<PostSymptom>? PostSymptoms { get; set; }
    }

}
