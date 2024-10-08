using Undersoft.SDK.Service.Data.Entity;

namespace Undersoft.GCC.Domain.Entities;

public class CurrencyRateTable : Entity
{
    public virtual CurrencyRateType Type { get; set; }

    public virtual string? Name { get => Label; set => Label = value; }

    public virtual long? ProviderId { get; set; }

    public virtual CurrencyProvider? Provider { get; set; }

    public virtual long? SourceCurrencyId { get; set; }

    public virtual Currency? SourceCurrency { get; set; }

    public virtual double SourceRate { get; set; }

    public virtual DateTime PublishDate { get; set; }

    public virtual int Decimals { get; set; } = 4;

    public virtual EntitySet<CurrencyRate>? Rates { get; set; }
}
