// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Campaign : DataObject, IViewModel
    {
        public virtual string? Name { get; set; }

        public virtual long? PriceId { get; set; }

        public virtual Price? Price { get; set; }

        public virtual Listing<Vaccine>? Vaccines { get; set; }
    }

}
