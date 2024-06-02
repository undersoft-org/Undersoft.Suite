namespace Undersoft.SCC.Domain.Entities.Countries
{
    public class CountryLanguage : Entity
    {
        public string? Name { get; set; }

        public string? LanguageCode { get; set; }

        public virtual EntitySet<Country>? Countries { get; set; }
    }
}
