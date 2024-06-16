// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Request : DataObject, IViewModel
    {
        public virtual string? Notes { get; set; }

        public long? StockId { get; set; }

        public virtual Stock? Stock { get; set; }

        public virtual float? Quentity { get; set; }
    }

}
