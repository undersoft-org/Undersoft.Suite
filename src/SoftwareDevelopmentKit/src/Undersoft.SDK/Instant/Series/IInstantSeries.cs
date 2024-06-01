namespace Undersoft.SDK.Instant.Series
{
    using Proxies;
    using Math;
    using Querying;
    using Rubrics;
    using SDK.Series;
    using System.Linq;
    using Undersoft.SDK.Proxies;

    public interface IInstantSeries : ISeries<IInstant>, IInstant
    {
        IInstantCreator Instant { get; set; }

        bool Prime { get; set; }

        new IInstant this[int index] { get; set; }

        object this[int index, string propertyName] { get; set; }

        object this[int index, int fieldId] { get; set; }

        IRubrics Rubrics { get; set; }

        IRubrics KeyRubrics { get; set; }

        IInstant NewInstant();

        IProxy NewProxy();

        Type InstantType { get; set; }

        int InstantSize { get; set; }

        Type Type { get; set; }

        IQueryable<IInstant> View { get; set; }

        IInstant Total { get; set; }

        InstantSeriesFilter Filter { get; set; }

        InstantSeriesSort Sort { get; set; }

        Func<IInstant, bool> Predicate { get; set; }
         
        InstantSeriesAggregate Aggregate { get; set; }

        ISeries<IInstantMath> Computations { get; set; }
    }
}
