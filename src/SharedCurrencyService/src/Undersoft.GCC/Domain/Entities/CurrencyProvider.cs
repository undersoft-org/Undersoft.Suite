using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.GCC.Domain.Entities;

public class CurrencyProvider : Entity, IFormattable
{
    public string? Name { get => Label; set => Label = value; }

    public string? FullName { get; set; }

    public CurrencyProviderType Type { get; set; }

    public long? BaseCurrencyId { get; set; }
    public virtual Currency? BaseCurrency { get; set; }

    public string? BaseUri { get; set; }

    public int UpdateHour { get; set; }

    public int UpdateMinute { get; set; }

    public DateTime HistorySince { get; set; }

    public virtual EntitySet<CurrencyRate>? Rates { get; set; }

    public virtual EntitySet<CurrencyRateTable>? Tables { get; set; }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"Base: {BaseCurrency!} {Name}";
    }
}
