// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts
{
    /// <summary>
    /// The contact address.
    /// </summary>
    public class Specification : DataObject, IContract
    {
        public string? VaccineImage { get; set; }

        public byte[]? VaccineImageData { get; set; }

        public string? Name { get; set; }

        public string? Virus { get; set; }

        public string? Dose { get; set; }

        public string? Description { get; set; }

        public string? Notes { get; set; }

        public long? VaccineId { get; set; }
    }
}