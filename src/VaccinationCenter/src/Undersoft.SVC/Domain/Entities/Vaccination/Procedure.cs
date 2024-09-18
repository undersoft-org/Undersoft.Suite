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
    public class Procedure : Entity, IEntity
    {
        public virtual string? Title { get; set; }

        public virtual long? AppointmentId { get; set; }

        public virtual Appointment? Appointment { get; set; }

        public virtual long? VaccineId { get; set; }

        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        public virtual Term? Term { get; set; }

        public virtual long? CostId { get; set; }

        public virtual Cost? Cost { get; set; }

        public virtual long? PriceId { get; set; }

        public virtual Price? Price { get; set; }

        public virtual long? PostSymptomId { get; set; }

        public virtual PostSymptom? PostSymptom { get; set; }

        public virtual long? CertificateId { get; set; }

        public virtual Certificate? Certificate { get; set; }
    }
}
