namespace Undersoft.SCC.Domain.Entities.Countries
{
    public partial class Currency : Entity
    {
        public string? Name { get; set; }

        public string? CurrencyCode { get; set; }

        public virtual EntitySet<Country>? Countries { get; set; }
    }
}
