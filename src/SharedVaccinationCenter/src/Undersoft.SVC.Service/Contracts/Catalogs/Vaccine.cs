// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Catalogs
{
    public class Vaccine : DataObject, IContract
    {
        public virtual string? Notes { get; set; }

        public long? ManufacturerId { get; set; }

        [AutoExpand]
        public virtual Manufacturer? Manufacturer { get; set; }

        public long? SafetyId { get; set; }

        [AutoExpand]
        public virtual Safety? Safety { get; set; }

        public long? SpecificationId { get; set; }

        [AutoExpand]
        public virtual Specification? Specification { get; set; }

        public long? StockId { get; set; }
    }

}
