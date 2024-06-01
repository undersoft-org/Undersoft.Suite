
namespace Undersoft.GCC.Domain.Aggregates
{
    public interface ICurrencyConversion
    {
        string? BaseCurrency { get; set; }
        DateTime? EndDate { get; set; }
        CurrencyConversionModes Mode { get; set; }
        DateTime? PublishDate { get; set; }
        double? Rate { get; set; }
        double SourceAmount { get; set; }
        string? SourceCurrency { get; set; }
        double? SourceRate { get; set; }
        DateTime? StartDate { get; set; }
        double? TargetAmount { get; set; }
        string? TargetCurrency { get; set; }
        double? TargetRate { get; set; }
    }
}