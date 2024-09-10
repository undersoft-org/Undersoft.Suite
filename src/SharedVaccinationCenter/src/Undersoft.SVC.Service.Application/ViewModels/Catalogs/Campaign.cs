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

namespace Undersoft.SVC.Service.Application.ViewModels.Catalogs
{
    [Validator("CampaignValidator")]
    [ViewSize("400px", "auto")]
    [OpenSearch("Name")]
    public class Campaign : DataObject, IViewModel
    {
        private string? _vaccineString;

        [VisibleRubric]
        [RubricSize(32)]
        [Filterable]
        [Sortable]
        [OpenQuery("Name")]
        [DisplayRubric("Name")]
        public virtual string? Name { get; set; }

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
        [RubricSize(128)]
        [Filterable]
        [Sortable]
        [OpenQuery("Vaccines.Specification.Name")]
        [DisplayRubric("Vaccines")]
        public virtual string? AssignedVaccines
        {
            get
            {
                if (_vaccineString != null)
                    return _vaccineString;
                if (Vaccines != null && Vaccines.Any())
                    return _vaccineString = Vaccines.Select(g => g.Specification!.Name).Aggregate((a, b) => a + ", " + b);
                return null;
            }
            set => _vaccineString = value;
        }

        public virtual long? PriceId { get; set; }

        [Extended]
        public virtual Price? Price { get; set; }

        public virtual Listing<Vaccine>? Vaccines { get; set; }
    }

}
