using System.Runtime.Serialization;
using Undersoft.SCC.Service.Contracts.Countries;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels
{
    [DataContract]
    public partial class Country : DataObject, IViewModel
    {
        [VisibleRubric]
        public string? Name { get; set; }

        [VisibleRubric]
        [DisplayRubric("Country code")]
        public string? CountryCode { get; set; }

        [VisibleRubric]
        public string? Continent { get; set; }

        [VisibleRubric]
        [DisplayRubric("Time zone UTC")]
        public string? TimeZone { get; set; }

        [VisibleRubric]
        [DisplayRubric("Currency code")]
        public string? CurrencyCode { get => Currency?.CurrencyCode; set => Currency!.CurrencyCode = value; }

        [VisibleRubric]
        [DisplayRubric("Language")]
        public string? LanguageName { get => Language?.Name; set => Language!.Name = value; }

        [VisibleRubric]
        [DisplayRubric("Country image")]
        [FileRubric(FileRubricType.Path, "CountryImageData")]
        public string CountryImage { get; set; } = default!;

        public byte[] CountryImageData { get; set; } = default!;

        public long? CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; } = new();

        public long? LanguageId { get; set; }
        public virtual CountryLanguage? Language { get; set; } = new();

        public virtual Listing<CountryState>? States { get; set; }

    }
}
