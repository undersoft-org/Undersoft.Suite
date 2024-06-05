using Microsoft.OData.ModelBuilder;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Undersoft.SCC.Service.Contracts.Countries;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts
{
    [Validator("CountryValidator")]
    [ViewSize(width: "400px", height: "700px")]
    public partial class Country : DataObject, IContract
    {
        [VisibleRubric]
        [RubricSize(3)]
        [DisplayRubric("Flag")]
        [ViewImage(ViewImageMode.Regular, "30px", "20px")]
        [FileRubric(FileRubricType.Property, "CountryImageData")]
        public string? CountryImage { get; set; } = default!;

        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Country Name")]
        public string? Name { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Country code")]
        public string? CountryCode { get; set; }

        [VisibleRubric]
        [RubricSize(64)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Continent")]
        public string? Continent { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [Filterable]
        [Sortable]
        [DisplayRubric("Time zone UTC")]
        public string? TimeZone { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(32)]
        [DisplayRubric("Currency code")]
        public string? CurrencyCode { get => Currency?.CurrencyCode; set => (Currency ??= new Currency()).CurrencyCode = value!; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [Filterable]
        [Sortable]
        [RubricSize(32)]
        [DisplayRubric("Language")]
        public string? LanguageName { get => Language?.Name; set => (Language ??= new CountryLanguage()).Name = value!; }

        public byte[]? CountryImageData { get; set; } = default!;

        public long? CurrencyId { get; set; }

        [Extended]
        [AutoExpand]
        public virtual Currency? Currency { get; set; }

        public long? LanguageId { get; set; }

        [Extended]
        [AutoExpand]
        public virtual CountryLanguage? Language { get; set; }

        [Extended]
        [AutoExpand]
        public virtual Listing<CountryState>? States { get; set; }

    }
}
