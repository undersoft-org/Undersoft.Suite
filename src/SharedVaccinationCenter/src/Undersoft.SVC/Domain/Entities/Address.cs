// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service
// *************************************************

using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Domain.Entities;

public class Address : Entity
{
    public AddressType AddressType { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public string? Postcode { get; set; }

    public string? Street { get; set; }

    public string? Building { get; set; }

    public string? Apartment { get; set; }

    public string? Notes { get; set; }

    public long? SupplierId { get; set; }

    public virtual Supplier? Supplier { get; set; }

}