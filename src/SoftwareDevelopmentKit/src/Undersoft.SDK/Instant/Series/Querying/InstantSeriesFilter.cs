namespace Undersoft.SDK.Instant.Series.Querying
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class InstantSeriesFilter
    {
        public Func<IInstant, bool> Evaluator;

        private InstantSeriesFilterExpression expression;

        private IInstantSeries series;

        private InstantSeriesFilterTerms termsBuffer;

        private InstantSeriesFilterTerms termsReducer;

        public InstantSeriesFilter(IInstantSeries series)
        {
            this.series = series;
            expression = new InstantSeriesFilterExpression();
            Reducer = new InstantSeriesFilterTerms(series);
            Terms = new InstantSeriesFilterTerms(series);
            termsBuffer = expression.Conditions;
            termsReducer = new InstantSeriesFilterTerms(series);
        }

        public IInstantSeries InstantSeriesGenerator
        {
            get { return series; }
            set { series = value; }
        }

        public InstantSeriesFilterTerms Reducer { get; set; }

        public InstantSeriesFilterTerms Terms { get; set; }

        public Expression<Func<IInstant, bool>> GetExpression(int stage = 1)
        {
            termsReducer.Clear();
            termsReducer.Add(Reducer.AsEnumerable().Concat(Terms.AsEnumerable()).ToArray());
            expression.Conditions = termsReducer;
            termsBuffer = termsReducer;
            return expression.CreateExpression(stage);
        }

        public IInstant[] Query(ICollection<IInstant> toQuery, int stage = 1)
        {
            termsReducer.Clear();
            termsReducer.Add(Reducer.AsEnumerable().Concat(Terms.AsEnumerable()).ToArray());
            expression.Conditions = termsReducer;
            termsBuffer = termsReducer;
            return toQuery
                .AsQueryable()
                .Where(expression.CreateExpression(stage).Compile())
                .ToArray();
        }

        public IInstant[] Query(int stage = 1)
        {
            termsReducer.Clear();
            termsReducer.Add(Reducer.AsEnumerable().Concat(Terms.AsEnumerable()).ToArray());
            expression.Conditions = termsReducer;
            termsBuffer = termsReducer;
            return series
                .AsEnumerable()
                .AsQueryable()
                .Where(expression.CreateExpression(stage).Compile())
                .ToArray();
        }
    }
}
