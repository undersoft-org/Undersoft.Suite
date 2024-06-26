// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************


// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts.Catalogs
{
    /// <summary>
    /// The contact address.
    /// </summary>
    public class Manufacturer : DataObject, IContract
    {
        public string? ManufacturerImage { get; set; }

        public byte[]? ManufacturerImageData { get; set; }

        public string? FullName { get; set; }

        public string? Name { get; set; }
    }
}