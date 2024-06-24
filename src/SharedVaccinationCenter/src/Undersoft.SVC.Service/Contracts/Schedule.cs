// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Contracts
{
    public class Schedule : DataObject, IContract
    {
        public virtual string? Notes { get; set; }

        public virtual ScheduleType? Type { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual TimeOnly? StartTime { get; set; }

        public virtual TimeOnly? EndTime { get; set; }

        public virtual TimeSpan? Interval { get; set; }

        public virtual long? AppointmentId { get; set; }
    }
}
