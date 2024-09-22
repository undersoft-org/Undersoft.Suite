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
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Operation;
using Undersoft.SVC.Domain.Entities.Enums;
using Undersoft.SVC.Service.Application.ViewModels.Catalogs;

namespace Undersoft.SVC.Service.Application.ViewModels.Vaccination
{
    [Validator("AppointmentValidator")]
    [ViewSize("380px", "550px")]
    [OpenSearch("Campaign.Name", "Personal.LastName", "Personal.FirstName", "Personal.PhoneNumber")]
    public class Appointment : DataObject, IViewModel
    {
        private string? _name;
        private string? _office;
        private string? _time;
        private string? _date;

        public virtual string? Notes { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(32)]
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
        [OpenQuery("RefreshAsync")]
        [DisplayRubric("RefreshAsync")]
        public virtual VaccinationState State { get; set; }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [Filterable]
        [Sortable]
        [OpenQuery("Office.Number", "Office.Name")]
        [DisplayRubric("Office")]
        public virtual string? OfficeName
        {
            get => _office ??= $"{Office?.Number}<br/>{Office?.Name}";
            set => _office = value;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [Filterable]
        [Sortable]
        [OpenQuery("Personal.LastName", "Personal.FirstName")]
        [DisplayRubric("Person")]
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
        [RubricSize(16)]
        [OpenQuery("Personal.PhoneNumber")]
        [DisplayRubric("Phone")]
        public virtual string? PhoneNumber
        {
            get => Personal?.PhoneNumber;
            set => (Personal ??= new Personal()).PhoneNumber = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(16)]
        [Filterable]
        [Sortable]
        [OpenQuery("Personal.Birthdate")]
        [DisplayRubric("Day of birth")]
        public virtual DateTime? Birthdate
        {
            get => Personal?.Birthdate;
            set => (Personal ??= new Personal()).Birthdate = value!.Value;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(16)]
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
        [RubricSize(32)]
        [Filterable]
        [Sortable]
        [OpenQuery(typeof(DateTime?), "Schedule.StartDate", "Schedule.EndDate")]
        [DisplayRubric("Date")]
        public virtual string? Date
        {
            get => _date ??= $"Post:{Schedule?.StartDate} End:{Schedule?.EndDate}";
            set => _date = value;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [Filterable]
        [Sortable]
        [OpenQuery(typeof(TimeOnly?), "Schedule.StartTime", "Schedule.EndTime")]
        [DisplayRubric("Time")]
        public virtual string? TimeFrame
        {
            get => _time ??= $"Post:{Schedule?.StartTime} End:{Schedule?.EndTime}";
            set => _time = value;
        }

        public virtual long? OfficeId { get; set; }

        [Extended]
        public virtual Office? Office { get; set; }

        public virtual long? PersonalId { get; set; }

        [Extended]
        public virtual Personal? Personal { get; set; }

        public virtual long? ScheduleId { get; set; }

        [Extended]
        public virtual Schedule? Schedule { get; set; }

        public virtual long? CampaignId { get; set; }

        [Extended]
        public virtual Campaign? Campaign { get; set; }

        public virtual long? ProcedureId { get; set; }

        public virtual Procedure? Procedure { get; set; }
    }
}
