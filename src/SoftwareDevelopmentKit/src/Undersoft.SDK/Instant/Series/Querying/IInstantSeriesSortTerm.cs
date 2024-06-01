namespace Undersoft.SDK.Instant.Series.Querying
{
    using System.Linq;

    public interface IInstantSeriesSortTerm
    {
        SortDirection Direction { get; set; }

        int RubricId { get; set; }

        string RubricName { get; set; }
    }
}
