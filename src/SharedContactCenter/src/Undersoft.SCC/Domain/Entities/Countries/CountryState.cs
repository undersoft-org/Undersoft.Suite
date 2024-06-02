namespace Undersoft.SCC.Domain.Entities.Countries
{
    public class CountryState : Entity
    {
        public string? Name { get; set; }

        public string? StateCode { get; set; }

        public string? TimeZone { get; set; }

        public long? CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
