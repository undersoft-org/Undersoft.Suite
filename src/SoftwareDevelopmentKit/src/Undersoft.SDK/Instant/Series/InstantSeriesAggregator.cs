namespace Undersoft.SDK.Instant.Series;

using Rubrics;

using System.Linq;
using Undersoft.SDK.Updating;

public static class InstantSeriesAggregator
{
    public static IInstant Aggregate(this IInstantSeries series, bool onlyView = false)
    {
        return ComputeAggregate(series, onlyView);
    }

    static IInstant ComputeAggregate(IInstantSeries series, bool onlyView = true)
    {
        IRubrics rubrics = series.Aggregate.Rubrics;
        if (rubrics.Count > 0)
        {
            object[] result = rubrics
                .AsValues()
                .AsParallel()
                .SelectMany(
                    s =>
                        new object[]
                        {
                            (!string.IsNullOrEmpty(s.RubricName))
                                ? (
                                    (s.AggregationOperand == AggregationOperand.Sum)
                                        ? Convert.ChangeType(
                                            series.View.Sum(
                                                j =>
                                                    (j[s.AggregationOrdinal] is DateTime)
                                                        ? (
                                                            (DateTime)j[s.AggregationOrdinal]
                                                        ).ToOADate()
                                                        : Convert.ToDouble(j[s.FieldId])
                                            ),
                                            typeof(object)
                                        )
                                        : (
                                            (s.AggregationOperand == AggregationOperand.Min)
                                                ? Convert.ChangeType(
                                                    series.View.Min(
                                                        j =>
                                                            (j[s.AggregationOrdinal] is DateTime)
                                                                ? (
                                                                    (DateTime)
                                                                        j[s.AggregationOrdinal]
                                                                ).ToOADate()
                                                                : Convert.ToDouble(j[s.FieldId])
                                                    ),
                                                    typeof(object)
                                                )
                                                : (
                                                    (s.AggregationOperand == AggregationOperand.Max)
                                                        ? Convert.ChangeType(
                                                            series.View.Max(
                                                                j =>
                                                                    (
                                                                        j[s.AggregationOrdinal]
                                                                        is DateTime
                                                                    )
                                                                        ? (
                                                                            (DateTime)
                                                                                j[
                                                                                    s.AggregationOrdinal
                                                                                ]
                                                                        ).ToOADate()
                                                                        : Convert.ToDouble(
                                                                            j[s.FieldId]
                                                                        )
                                                            ),
                                                            typeof(object)
                                                        )
                                                        : (
                                                            (
                                                                s.AggregationOperand
                                                                == AggregationOperand.Avg
                                                            )
                                                                ? Convert.ChangeType(
                                                                    series.View.Average(
                                                                        j =>
                                                                            (
                                                                                j[
                                                                                    s.AggregationOrdinal
                                                                                ] is DateTime
                                                                            )
                                                                                ? (
                                                                                    (DateTime)
                                                                                        j[
                                                                                            s.AggregationOrdinal
                                                                                        ]
                                                                                ).ToOADate()
                                                                                : Convert.ToDouble(
                                                                                    j[s.FieldId]
                                                                                )
                                                                    ),
                                                                    typeof(object)
                                                                )
                                                                : (
                                                                    (
                                                                        s.AggregationOperand
                                                                        == AggregationOperand.Concat
                                                                    )
                                                                        ? Convert.ChangeType(
                                                                            series.View
                                                                                .Select(
                                                                                    j =>
                                                                                        (
                                                                                            j[
                                                                                                s.FieldId
                                                                                            ]
                                                                                            != DBNull.Value
                                                                                        )
                                                                                            ? j[
                                                                                                s.FieldId
                                                                                            ].ToString()
                                                                                            : string.Empty
                                                                                )
                                                                                .Aggregate(
                                                                                    (x, y) =>
                                                                                        $"{x} {y}"
                                                                                ),
                                                                            typeof(object)
                                                                        )
                                                                        : null
                                                                )
                                                        )
                                                )
                                        )
                                )
                                : null
                        }
                )
                .ToArray();

            series.Total.PutFrom(result);

            return series.Total;
        }
        else
            return null;
    }
}
