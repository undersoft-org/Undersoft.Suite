using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.GCC.Service.Contracts;

public class CurrencyRate : DataObject, IContract, IFormattable
{
    public double _rate;

    public virtual CurrencyRateType Type { get; set; }

    public virtual string? Name
    {
        get => Label;
        set => Label = value;
    }

    public virtual long? ProviderId { get; set; }

    public virtual CurrencyProvider? Provider { get; set; }

    public virtual long? TableId { get; set; }

    public virtual CurrencyRateTable? Table { get; set; }

    public virtual long? SourceCurrencyId { get; set; }

    public virtual Currency? SourceCurrency { get; set; }

    public virtual double SourceRate { get; set; }

    public virtual long? TargetCurrencyId { get; set; }

    public virtual Currency? TargetCurrency { get; set; }

    public virtual double TargetRate { get; set; }

    public virtual double Rate
    {
        get => _rate == 0 ? _rate = ComputeRate() : _rate;
        set => _rate = value;
    }

    public virtual DateTime PublishDate { get; set; }

    public virtual int Decimals { get; set; } = 4;

    public double ComputeRate()
    {
        return TargetRate / SourceRate;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"{Provider} {SourceCurrency} {TargetCurrency} {Rate} {PublishDate}";
    }
}
