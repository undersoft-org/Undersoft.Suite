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
    public class Term : Entity, IEntity
    {
        public virtual string? Name { get; set; }

        public virtual string? Description { get; set; }

        public virtual string? Dose { get; set; }

        public virtual DateTime? Date { get; set; }

        public virtual TimeSpan? Interval { get; set; }

        public virtual DateTime? Expiration { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual Procedure? Procedure { get; set; }

        public virtual long? PostSymptomId { get; set; }

        public virtual PostSymptom? PostSymptom { get; set; }

        public virtual long? CertificateId { get; set; }

        public virtual Certificate? Certificate { get; set; }

    }

}
