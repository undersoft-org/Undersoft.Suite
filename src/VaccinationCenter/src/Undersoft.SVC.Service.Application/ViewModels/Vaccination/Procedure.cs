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
using Undersoft.SVC.Service.Application.ViewModels.Catalogs;

namespace Undersoft.SVC.Service.Application.ViewModels.Vaccination
{
    public class Procedure : DataObject, IViewModel
    {
        private string? _name;

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [OpenQuery("Appointment.Campaign.Name")]
        [DisplayRubric("Campaign")]
        public virtual string? CampaignName
        {
            get => Appointment?.CampaignName;
            set => (Appointment ??= new Appointment()).CampaignName = value!;
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
            get => _name ??= $"{Appointment?.Personal?.FirstName} {Appointment?.Personal?.LastName}";
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

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(64)]
        [OpenQuery("Term.Dose")]
        [DisplayRubric("Dose")]
        public virtual string? Dose
        {
            get => Term?.Dose;
            set => (Term ??= new Term()).Dose = value!;
        }

        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [OpenQuery(typeof(DateTime), "Term.Date")]
        [DisplayRubric("Time")]
        public virtual DateTime? DateTime
        {
            get => (Term ??= new Term()).Date;
            set => (Term ??= new Term()).Date = value;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Price")]
        [Filterable]
        [Sortable]
        [OpenQuery("Price.Amount")]
        public virtual double? PriceAmount
        {
            get => (Price ??= new Price()).Amount;
            set => (Price ??= new Price()).Amount = value!;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Cost")]
        [Filterable]
        [Sortable]
        [OpenQuery("Cost.Amount")]
        public virtual double? CostAmount
        {
            get => (Cost ??= new Cost()).Amount;
            set => (Cost ??= new Cost()).Amount = value!;
        }

        public virtual long? AppointmentId { get; set; }

        [Extended]
        public virtual Appointment? Appointment { get; set; }

        public virtual long? VaccineId { get; set; }

        [Extended]
        public virtual Vaccine? Vaccine { get; set; }

        public virtual long? TermId { get; set; }

        [Extended]
        public virtual Term? Term { get; set; }

        public virtual long? CostId { get; set; }

        [Extended]
        public virtual Cost? Cost { get; set; }

        public virtual long? PriceId { get; set; }

        [Extended]
        public virtual Price? Price { get; set; }

        public virtual long? PostSymptomId { get; set; }

        public virtual long? CertificateId { get; set; }
    }
}
