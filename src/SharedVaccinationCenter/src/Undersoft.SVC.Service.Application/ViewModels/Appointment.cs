// *************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC
// *************************************************


using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SVC.Domain.Entities.Enums;

namespace Undersoft.SVC.Service.Application.ViewModels
{
    public class Appointment : DataObject, IViewModel
    {
        private string? _name;
        private string? _office;
        private string? _time;

        public virtual string? Notes { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [OpenQuery("Campaign.Name")]
        [DisplayRubric("Campaign")]
        public virtual string? CampaignName
        {
            get => Campaign?.Name;
            set => (Campaign ??= new Campaign()).Name = value!;
        }

        [VisibleRubric]
        [RubricSize(16)]
        [Filterable]
        [Sortable]
        [DisplayRubric("State")]
        public virtual VaccinationState State { get; set; }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Office.Number", "Office.Name")]
        [DisplayRubric("Office")]
        public virtual string? OfficeName
        {
            get => _office ??= $"{Office?.Number} {Office?.Name}";
            set => _office = value;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Personal.LastName", "Personal.FirstName")]
        [DisplayRubric("Name")]
        public virtual string? Name
        {
            get => _name ??= $"{Personal?.FirstName} {Personal?.LastName}";
            set => _name = value;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [OpenQuery("Personal.PhoneNumber")]
        [DisplayRubric("Phone")]
        public virtual string? PhoneNumber
        {
            get => Personal?.PhoneNumber;
            set => (Personal ??= new Personal()).PhoneNumber = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Schedule.Type")]
        [DisplayRubric("Type")]
        public virtual ScheduleType Type
        {
            get => (Schedule ??= new Schedule()).Type;
            set => (Schedule ??= new Schedule()).Type = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Schedule.Date")]
        [DisplayRubric("Date")]
        public virtual DateTime? Date
        {
            get => Schedule?.Date;
            set => (Schedule ??= new Schedule()).Date = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Schedule.StartTime", "Schedule.EndTime")]
        [DisplayRubric("Time")]
        public virtual string? TimeFrame
        {
            get => _time ??= $"{Schedule?.StartTime}-{Schedule?.EndTime}";
            set => _time = value;
        }

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
