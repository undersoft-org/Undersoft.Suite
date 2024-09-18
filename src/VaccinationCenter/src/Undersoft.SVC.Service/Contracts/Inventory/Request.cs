// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Inventory
{
    public class Request : DataObject, IContract
    {
        public virtual string? Notes { get; set; }

        public long? StockId { get; set; }

        [AutoExpand]
        public virtual Stock? Stock { get; set; }

        public virtual float? Quentity { get; set; }
    }

}
