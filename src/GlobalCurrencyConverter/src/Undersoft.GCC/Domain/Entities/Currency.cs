using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Uniques;

namespace Undersoft.GCC.Domain.Entities;

public class Currency : Entity, IFormattable
{
    private string? _currencyCode;

    public virtual string? CurrencyCode { get => _currencyCode; set => Id = (_currencyCode = value).UniqueKey64(); }

    public virtual string? Symbol { get; set; }

    public virtual string? Name { get => Label; set => Label = value; }

    public virtual bool IsDecimal { get; set; }

    public virtual EntitySet<CurrencyProvider>? Providers { get; set; }

    public virtual EntitySet<CurrencyRateTable>? RateTables { get; set; }

    public virtual EntitySet<CurrencyRate>? SourceRates { get; set; }

    public virtual EntitySet<CurrencyRate>? TargetRates { get; set; }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return CurrencyCode!;
    }
}
