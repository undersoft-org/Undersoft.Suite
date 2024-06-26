// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Microsoft.OData.ModelBuilder;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SVC.Domain.Entities.Enums;
using Undersoft.SVC.Service.Contracts.Catalogs;

namespace Undersoft.SVC.Service.Contracts.Vaccination
{
    public class Appointment : DataObject, IContract
    {
        public virtual string? Notes { get; set; }

        public virtual VaccinationState State { get; set; }

        public virtual long? OfficeId { get; set; }

        [AutoExpand]
        public virtual Office? Office { get; set; }

        public virtual long? PersonalId { get; set; }

        [AutoExpand]
        public virtual Personal? Personal { get; set; }

        public virtual long? ScheduleId { get; set; }

        [AutoExpand]
        public virtual Schedule? Schedule { get; set; }

        public virtual long? CampaignId { get; set; }

        [AutoExpand]
        public virtual Campaign? Campaign { get; set; }

        public virtual long? ProcedureId { get; set; }
    }
}
