// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application
// *************************************************

using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Contracts
{
    public class Personal : DataObject, IContract
    {
        public IdentifierType IdentifierType { get; set; }

        public string? Identifier { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public DateTime Birthdate { get; set; } = DateTime.UtcNow;

        public long? AppointmentId { get; set; }

        public virtual long? CertificateId { get; set; }

        public virtual long? PostSymptomId { get; set; }
    }
}