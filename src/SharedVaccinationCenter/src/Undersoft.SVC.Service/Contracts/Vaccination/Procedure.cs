// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SVC.Service.Contracts.Catalogs;

namespace Undersoft.SVC.Service.Contracts.Vaccination
{
    public class Procedure : DataObject, IContract
    {
        public virtual string? Title { get; set; }

        public virtual long? AppointmentId { get; set; }

        [AutoExpand]
        public virtual Appointment? Appointment { get; set; }

        public virtual long? VaccineId { get; set; }

        [AutoExpand]
        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        [AutoExpand]
        public virtual Term? Term { get; set; }

        public virtual long? CostId { get; set; }

        [AutoExpand]
        public virtual Cost? Cost { get; set; }

        public virtual long? PriceId { get; set; }

        [AutoExpand]
        public virtual Price? Price { get; set; }

        public virtual long? PostSymptomId { get; set; }

        public virtual long? CertificateId { get; set; }
    }
}
