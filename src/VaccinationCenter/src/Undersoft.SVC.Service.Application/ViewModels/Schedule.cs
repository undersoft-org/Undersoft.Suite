// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************

using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Schedule : DataObject, IViewModel
    {
        public virtual string? Notes { get; set; }

        [VisibleRubric]
        [DisplayRubric("Type")]
        public virtual ScheduleType Type { get; set; }

        [VisibleRubric]
        [DisplayRubric("Post date")]
        public virtual DateTime? StartDate { get; set; } = DateTime.Now.Date;

        [VisibleRubric]
        [DisplayRubric("Post Time")]
        public virtual TimeOnly? StartTime { get; set; }

        [VisibleRubric]
        [DisplayRubric("End date")]
        public virtual DateTime? EndDate { get; set; } = DateTime.Now.Date;

        [VisibleRubric]
        [DisplayRubric("End Time")]
        public virtual TimeOnly? EndTime { get; set; }

        [VisibleRubric]
        [DisplayRubric("Interval")]
        public virtual TimeSpan? Interval { get; set; }

        public virtual long? AppointmentId { get; set; }
    }
}
