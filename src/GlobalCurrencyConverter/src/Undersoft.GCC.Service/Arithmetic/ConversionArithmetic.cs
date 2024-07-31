using Undersoft.GCC.Domain.Aggregates;
using Undersoft.SDK.Instant;
using Undersoft.SDK.Instant.Math;
using Undersoft.SDK.Instant.Math.Set;
using Undersoft.SDK.Instant.Series;

namespace Undersoft.GCC.Service.Arithmetic;

public class ConversionArithmetic<T> where T : class, ICurrencyConversion
{
    InstantMath<T> _math;
    IInstantSeries _series;
    InstantSeriesGenerator<T> _creator;

    public ConversionArithmetic(IEnumerable<T> rates)
    {
        _creator = new InstantSeriesGenerator<T>(InstantType.Derived, threadSafe: true);
        _series = _creator.Generate();
        _math = new InstantMath<T>(_series);
    }


    public void MathFormula(IInstantSeries series)
    {
        MathSet<T> rateMath = _math[r => r.Rate];

        rateMath.Formula = rateMath[p => p.TargetRate] / rateMath[p => p.SourceRate];

    }
}
