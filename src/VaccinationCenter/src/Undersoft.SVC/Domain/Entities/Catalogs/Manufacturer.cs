// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

namespace Undersoft.SVC.Domain.Entities.Catalogs
{
    /// <summary>
    /// The contact address.
    /// </summary>
    public class Manufacturer : Entity
    {
        public string? ManufacturerImage { get; set; }

        public byte[]? ManufacturerImageData { get; set; }

        public string? FullName { get; set; }

        public string? Name { get; set; }

        public virtual EntitySet<Vaccine>? Vaccines { get; set; }
    }
}