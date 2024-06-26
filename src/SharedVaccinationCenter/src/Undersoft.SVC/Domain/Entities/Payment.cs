// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Vaccination;

namespace Undersoft.SVC.Domain.Entities
{
    public class Payment : Entity, IEntity
    {
        public virtual string? Name { get; set; }

        public virtual double? Value { get; set; }

        public virtual double? Tax { get; set; }

        public virtual double? Amount { get; set; }

        public virtual long? CertificateId { get; set; }

        public virtual Certificate? Certificate { get; set; }
    }

}
