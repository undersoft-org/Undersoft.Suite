// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

namespace Undersoft.SVC.Service.Application.ViewModels.Catalogs
{
    public class Vaccine : DataObject, IViewModel
    {
        public virtual string? Notes { get; set; }

        public long? ManufacturerId { get; set; }

        public virtual Manufacturer? Manufacturer { get; set; }

        public long? SafetyId { get; set; }

        public virtual Safety? Safety { get; set; }

        public long? SpecificationId { get; set; }

        public virtual Specification? Specification { get; set; }

        public long? StockId { get; set; }
    }

}
