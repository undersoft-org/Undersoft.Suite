// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Contracts.Inventory
{
    public class Traffic : DataObject, IContract
    {
        public virtual string? Notes { get; set; }

        public virtual TrafficType Type { get; set; }

        public virtual long? CostId { get; set; }

        [AutoExpand]
        public virtual Cost? Cost { get; set; }

        public virtual long? PriceId { get; set; }

        [AutoExpand]
        public virtual Price? Price { get; set; }

        public virtual float? Quantity { get; set; }

        public virtual long? StockId { get; set; }

        [AutoExpand]
        public virtual Stock? Stock { get; }
    }

}
