using Undersoft.SCC.Domain.Entities.Countries;

namespace Undersoft.SCC.Domain.Entities
{
    public partial class Country : Entity
    {
        public string? Name { get; set; }

        public string? CountryCode { get; set; }

        public string? Continent { get; set; }

        public string? TimeZone { get; set; }

        public string? CountryImage { get; set; }

        public byte[]? CountryImageData { get; set; }

        public long? CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }

        public long? LanguageId { get; set; }
        public virtual CountryLanguage? Language { get; set; }

        public virtual EntitySet<CountryState>? States { get; set; }

    }
}
