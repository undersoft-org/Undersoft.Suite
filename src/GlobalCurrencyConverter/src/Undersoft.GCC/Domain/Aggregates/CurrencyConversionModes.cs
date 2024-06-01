namespace Undersoft.GCC.Domain.Aggregates;

[Flags]
public enum CurrencyConversionModes
{
    Mid = 1,
    Ask = 2,
    Bid = 4,
    Max = 8,
    Min = 16,
    Avg = 32
}