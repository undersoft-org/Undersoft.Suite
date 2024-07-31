// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

namespace Undersoft.SVC.Service.Contracts
{
    public class Address : SDK.Service.Access.Identity.Address
    {
        public long? SupplierId { get; set; }
    }
}