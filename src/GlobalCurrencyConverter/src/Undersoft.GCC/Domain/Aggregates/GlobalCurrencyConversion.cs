using Undersoft.SDK.Series;

namespace Undersoft.GCC.Domain.Aggregates;

public class GlobalCurrencyConversion
{
    public CurrencyConversionModes Mode { get; set; }

    public string? BaseCurrency { get; set; }

    public double SourceAmount { get; set; }

    public string? SourceCurrency { get; set; }

    public double? SourceRate { get; set; }

    public double? Rate { get; set; }

    public double? TargetAmount { get; set; }

    public string? TargetCurrency { get; set; }

    public double? TargetRate { get; set; }

    public DateTime? PublishDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Listing<CurrencyConversion>? Conversions { get; set; }
}
