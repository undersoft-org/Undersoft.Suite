// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SVC.Domain.Entities.Catalogs;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Domain.Entities.Vaccination
{
    public class Appointment : Entity, IEntity
    {
        public virtual string? Notes { get; set; }

        public virtual VaccinationState State { get; set; }

        public virtual long? OfficeId { get; set; }

        public virtual Office? Office { get; set; }

        public virtual long? PersonalId { get; set; }

        public virtual Personal? Personal { get; set; }

        public virtual long? ScheduleId { get; set; }

        public virtual Schedule? Schedule { get; set; }

        public virtual long? CampaignId { get; set; }

        public virtual Campaign? Campaign { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual Procedure? Procedure { get; set; }
    }
}
