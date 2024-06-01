using Microsoft.OData.ModelBuilder;
using Undersoft.SCC.Service.Contracts.Countries;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Operation;

namespace Undersoft.SCC.Service.Contracts
{
    [Validator("CountryValidator")]
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

        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Currency code")]
        public string? CurrencyCode { get => Currency?.CurrencyCode; set => Currency!.CurrencyCode = value; }

        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Language")]
        public string? LanguageName { get => Language?.Name; set => Language!.Name = value; }

        [VisibleRubric]
        [RubricSize(32)]
        [DisplayRubric("Country image")]
        [FileRubric(FileRubricType.Path, "CountryImageData")]
        public string? CountryImage { get; set; } = default!;

        public byte[]? CountryImageData { get; set; } = default!;

        public long? CurrencyId { get; set; }

        [Expand]
        public virtual Currency? Currency { get; set; }

        public long? LanguageId { get; set; }

        [Expand]
        public virtual CountryLanguage? Language { get; set; }

        [Expand]
        public virtual Listing<CountryState>? States { get; set; }

    }
}
