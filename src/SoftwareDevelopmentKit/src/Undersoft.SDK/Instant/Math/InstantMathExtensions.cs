using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Instant.Math;

using Rubrics;
using System.Collections.Generic;
using System.Linq;

public static class InstantMathExtensions
{
    public static IInstantSeries Compute(this IInstantSeries series)
    {
        series.Computations.Select(c => c.Compute()).ToArray();
        return series;
    }

    public static IInstantSeries Compute(this IInstantSeries series, IList<MemberRubric> rubrics)
    {
        IInstantMath[] ic = rubrics
            .Select(
                r =>
                    series.Computations
                        .Where(c => ((InstantMath)c).ContainsFirst(r))
                        .FirstOrDefault()
            )
            .Where(c => c != null)
            .ToArray();
        ic.Select(c => c.Compute()).ToArray();
        return series;
    }

    public static IInstantSeries Compute(this IInstantSeries series, IList<string> rubricNames)
    {
        IInstantMath[] ic = rubricNames
            .Select(
                r =>
                    series.Computations
                        .Where(c => ((InstantMath)c).ContainsFirst(r))
                        .FirstOrDefault()
            )
            .Where(c => c != null)
            .ToArray();
        ic.Select(c => c.Compute()).ToArray();
        return series;
    }

    public static IInstantSeries Compute(this IInstantSeries series, MemberRubric rubric)
    {
        IInstantMath ic = series.Computations
            .Where(c => ((InstantMath)c).ContainsFirst(rubric))
            .FirstOrDefault();
        if (ic != null)
            ic.Compute();
        return series;
    }

    public static IInstantSeries ComputeParallel(this IInstantSeries series)
    {
        series.Computations.AsParallel().Select(c => c.Compute()).ToArray();
        return series;
    }

    public static IInstantSeries ComputeParallel(this IInstantSeries series, IList<MemberRubric> rubrics)
    {
        IInstantMath[] ic = rubrics
            .Select(
                r =>
                    series.Computations
                        .Where(c => ((InstantMath)c).ContainsFirst(r))
                        .FirstOrDefault()
            )
            .Where(c => c != null)
            .ToArray();
        ic.AsParallel().Select(c => c.Compute()).ToArray();
        return series;
    }

    public static IInstantSeries ComputeParallel(this IInstantSeries series, IList<string> rubricNames)
    {
        IInstantMath[] ic = rubricNames
            .Select(
                r =>
                    series.Computations
                        .Where(c => ((InstantMath)c).ContainsFirst(r))
                        .FirstOrDefault()
            )
            .Where(c => c != null)
            .ToArray();
        ic.AsParallel().Select(c => c.Compute()).ToArray();
        return series;
    }
}
