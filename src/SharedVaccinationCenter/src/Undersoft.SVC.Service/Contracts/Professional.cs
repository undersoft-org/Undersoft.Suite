// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC
// *************************************************

using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SVC.Service.Contracts
{
    public class Professional : SDK.Service.Access.Identity.Professional, IContract
    {
        public string? ProfessionalImage { get; set; }

        public string? ProfessionalManager { get; set; }

        public string? ProfessionalName { get; set; }

        public string? ProfessionalPosition { get; set; }

        public byte[]? ProfessionalImageData { get; set; }

        public long? SupplierId { get; set; }
    }
}