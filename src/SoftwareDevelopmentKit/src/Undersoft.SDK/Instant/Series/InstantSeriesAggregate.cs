namespace Undersoft.SDK.Instant.Series
{
    using System.Linq;
    using Rubrics;

    public class InstantSeriesAggregate
    {
        IInstantSeries series;
        MemberRubrics rubrics;

        public InstantSeriesAggregate(IInstantSeries series)
        {
            this.series = series;
        }

        MemberRubrics CreateRubrics()
        {
            AggregationOperand parsed = new AggregationOperand();
            rubrics = new MemberRubrics();
            InstantCreator summaryInstantCreator = new InstantCreator(
                series.Rubrics
                    .AsValues()
                    .Where(
                        c =>
                            (c.RubricName.Split('=').Length > 1)
                            || (c.AggregationOperand != AggregationOperand.None)
                    )
                    .Select(
                        c =>
                            new MemberRubric(c)
                            {
                                AggregateRubric =
                                    (c.AggregateRubric != null)
                                        ? c.AggregateRubric
                                        : (
                                            (c.RubricName.Split('=').Length > 1)
                                                ? (
                                                    new MemberRubric(c)
                                                    {
                                                        RubricName = c.RubricName.Split('=')[1]
                                                    }
                                                )
                                                : null
                                        ),
                                AggregationOperand = Enum.TryParse(
                                    c.RubricName.Split('=')[0],
                                    true,
                                    out parsed
                                )
                                    ? parsed
                                    : c.AggregationOperand
                            }
                    )
                    .ToArray(),
                $"Aggregate_{series.GetType().Name}"
            );
            series.Total = summaryInstantCreator.Create();
            rubrics = (MemberRubrics)summaryInstantCreator.Rubrics;
            return rubrics;
        }

        public IRubrics Rubrics
        {
            get
            {
                if (rubrics == null)
                {
                    CreateRubrics();
                }
                return rubrics;
            }
        }
    }
}
