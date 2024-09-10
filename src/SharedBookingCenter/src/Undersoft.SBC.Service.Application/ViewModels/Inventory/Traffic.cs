// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Application.ViewModels.Inventory
{
    public class Traffic : DataObject, IViewModel
    {
        public virtual string? Notes { get; set; }

        public virtual TrafficType Type { get; set; }

        public virtual long? CostId { get; set; }

        public virtual Cost? Cost { get; set; }

        public virtual long? PriceId { get; set; }

        public virtual Price? Price { get; set; }

        public virtual float? Quantity { get; set; }

        public virtual long? StockId { get; set; }

        public virtual Stock? Stock { get; }
    }

}
