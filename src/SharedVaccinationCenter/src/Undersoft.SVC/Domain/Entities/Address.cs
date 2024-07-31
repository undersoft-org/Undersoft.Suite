// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;

namespace Undersoft.SVC.Domain.Entities
{
    public class Address : SDK.Service.Access.Identity.Address, IEntity
    {
        public long? SupplierId { get; set; }

        public virtual Supplier? Supplier { get; set; }

    }
}