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
using Undersoft.SVC.Service.Application.ViewModels.Catalogs;

namespace Undersoft.SVC.Service.Application.ViewModels.Vaccination
{
    [Validator("CertificateValidator")]
    [OpenSearch("Procedure.Appointment.Campaign.Name", "Personal.LastName", "Personal.FirstName", "Vaccine.Specification.Name")]
    public class Certificate : DataObject, IViewModel
    {
        private string? _name;
        private string? _effectiveDate;

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [OpenQuery("Procedure.Appointment.Campaign.Name")]
        [DisplayRubric("Campaign")]
        public virtual string? CampaignName
        {
            get => Procedure?.Appointment?.CampaignName;
            set => ((Procedure ??= new Procedure()).Appointment ??= new Appointment()).CampaignName = value!;
        }

        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Title")]
        [DisplayRubric("Title")]
        public virtual string? Title { get; set; }

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

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery("Vaccine.Specification.Name")]
        [DisplayRubric("Vaccine name")]
        public virtual string? VaccineName
        {
            get => Vaccine?.Specification?.Name;
            set => ((Vaccine ??= new Vaccine()).Specification ??= new Specification()).Name = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery(typeof(DateTime), "Term.Date", "Term.Expiration")]
        [DisplayRubric("Effective date")]
        public virtual string? EffectiveDate
        {
            get => _effectiveDate ??= $"{Term?.Date!.Value.ToString("yyyy-MM-dd")}-{Term?.Expiration!.Value.ToString("yyyy-MM-dd")}";
            set => _effectiveDate = value;
        }


        public virtual long? ProcedureId { get; set; }

        [Extended]
        public virtual Procedure? Procedure { get; set; }

        public virtual long? PersonalId { get; set; }

        [Extended]
        public virtual Personal? Personal { get; set; }

        public virtual long? VaccineId { get; set; }

        [Extended]
        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        [Extended]
        public virtual Term? Term { get; set; }

        public virtual long? PaymentId { get; set; }

        [Extended]
        public virtual Payment? Payment { get; set; }
    }
}
