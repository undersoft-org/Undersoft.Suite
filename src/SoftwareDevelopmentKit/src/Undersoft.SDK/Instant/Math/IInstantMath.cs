using Undersoft.SDK.Instant.Series;

namespace Undersoft.SDK.Instant.Math;

using Uniques;

public interface IInstantMath : IUnique
{
    IInstantSeries Compute();
}
