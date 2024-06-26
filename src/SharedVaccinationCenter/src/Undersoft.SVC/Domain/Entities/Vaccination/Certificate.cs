// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************


// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;

namespace Undersoft.SVC.Domain.Entities.Vaccination
{
    public class Certificate : Entity, IEntity
    {
        public virtual string? Title { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual Procedure? Procedure { get; set; }

        public virtual long? PersonalId { get; set; }

        public virtual Personal? Personal { get; set; }

        public virtual long? VaccineId { get; set; }

        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        public virtual Term? Term { get; set; }

        public virtual long? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }
    }
}
