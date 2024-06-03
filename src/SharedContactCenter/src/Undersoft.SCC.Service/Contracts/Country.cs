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
        [RubricSize(64)]
        [DisplayRubric("Country Name")]
        public string? Name { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Country code")]
        public string? CountryCode { get; set; }

        [VisibleRubric]
        [RubricSize(64)]
        [DisplayRubric("Continent")]
        public string? Continent { get; set; }

        [VisibleRubric]
        [RubricSize(8)]
        [DisplayRubric("Time zone UTC")]
        public string? TimeZone { get; set; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Currency code")]
        public string? CurrencyCode { get => Currency?.CurrencyCode; set => (Currency ??= new Currency()).CurrencyCode = value!; }

        [IgnoreDataMember]
        [JsonIgnore]
        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Language")]
        public string? LanguageName { get => Language?.Name; set => (Language ??= new CountryLanguage()).Name = value!; }

        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Country image")]
        [ViewImage(ViewImageMode.Regular, "30px", "30px")]
        [FileRubric(FileRubricType.Property, "CountryImageData")]
        public string? CountryImage { get; set; } = default!;

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
