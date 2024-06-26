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
    public class Campaign : DataObject, IContract
    {
        public virtual string? Name { get; set; }

        public virtual long? PriceId { get; set; }

        [AutoExpand]
        public virtual Price? Price { get; set; }

        [AutoExpand]
        public virtual Listing<Vaccine>? Vaccines { get; set; }
    }
}
