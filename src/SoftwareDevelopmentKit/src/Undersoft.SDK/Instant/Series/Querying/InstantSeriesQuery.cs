namespace Undersoft.SDK.Instant.Series.Querying
{
    using Series;
    using System.Collections.Generic;
    using System.Linq;

    public static class InstantSeriesQuery
    {
        public static IInstant[] Query(
            this IInstant[] figureArray,
            Func<IInstant, bool> queryFormula
        )
        {
            return figureArray.Where(queryFormula).ToArray();
        }

        public static IQueryable<IInstant> Query(
            this IInstantSeries series,
            Func<IInstant, bool> queryFormula
        )
        {
            return series.View.Where(queryFormula).AsQueryable();
        }

        public static IQueryable<IInstant> Query(
            this IInstantSeries series,
            IInstant[] appendseries,
            int stage = 1
        )
        {
            InstantSeriesFilter Filter = series.Filter;
            InstantSeriesSort Sort = series.Sort;
            return ResolveQuery(series, Filter, Sort, stage, appendseries);
        }

        public static IQueryable<IInstant> Query(
            this IInstantSeries series,
            IList<InstantSeriesFilterTerm> filterList,
            IList<InstantSeriesSortTerm> sortList,
            bool saveonly = false,
            bool clearonend = false,
            int stage = 1
        )
        {
            InstantSeriesFilter Filter = series.Filter;
            InstantSeriesSort Sort = series.Sort;
            if (filterList != null)
            {
                Filter.Terms.Renew(filterList);
            }

            if (sortList != null)
            {
                Sort.Terms.Renew(sortList);
            }

            if (!saveonly)
            {
                IQueryable<IInstant> result = ResolveQuery(series, Filter, Sort, stage);
                if (clearonend)
                {
                    series.Filter.Terms.Clear();
                    series.Filter.Evaluator = null;
                    series.Predicate = null;
                }

                return result;
            }

            return null;
        }

        public static IQueryable<IInstant> Query(
            this IInstantSeries series,
            int stage = 1,
            InstantSeriesFilterTerms filter = null,
            InstantSeriesSortTerms sort = null,
            bool saveonly = false,
            bool clearonend = false
        )
        {
            InstantSeriesFilter Filter = series.Filter;
            InstantSeriesSort Sort = series.Sort;

            if (filter != null)
            {
                Filter.Terms.Renew(filter.ToArray());
            }

            if (sort != null)
            {
                Sort.Terms.Renew(sort.ToArray());
            }

            if (!saveonly)
            {
                IQueryable<IInstant> result = ResolveQuery(series, Filter, Sort, stage);
                if (clearonend)
                {
                    series.Filter.Terms.Clear();
                    series.Filter.Evaluator = null;
                    series.Predicate = null;
                }

                return result;
            }

            return null;
        }

        public static IQueryable<IInstant> Query(
            this IInstantSeries series,
            out bool sorted,
            out bool filtered,
            int stage = 1
        )
        {
            InstantSeriesFilter Filter = series.Filter;
            InstantSeriesSort Sort = series.Sort;

            filtered = (Filter.Terms.Count > 0) ? true : false;
            sorted = (Sort.Terms.Count > 0) ? true : false;
            return ResolveQuery(series, Filter, Sort, stage);
        }

        private static IQueryable<IInstant> ExecuteQuery(
            IInstantSeries series,
            InstantSeriesFilter filter,
            InstantSeriesSort sort,
            int stage = 1,
            IInstant[] appendseries = null
        )
        {
            IQueryable<IInstant> table = series.AsQueryable();
            IQueryable<IInstant> _series = null;
            IQueryable<IInstant> view = series.View;

            if (appendseries == null)
                if (stage > 1)
                    _series = view;
                else if (stage < 0)
                {
                    _series = table;
                    view = series.View;
                }
                else
                {
                    _series = table;
                }

            if (filter != null && filter.Terms.Count > 0)
            {
                filter.Evaluator = filter.GetExpression(stage).Compile();
                series.Predicate = filter.Evaluator;

                if (sort != null && sort.Terms.Count > 0)
                {
                    bool isFirst = true;
                    IEnumerable<IInstant> ief = null;
                    IOrderedQueryable<IInstant> ioqf = null;
                    if (appendseries != null)
                        ief = appendseries.Where(filter.Evaluator);
                    else
                        ief = _series.Where(filter.Evaluator);

                    foreach (InstantSeriesSortTerm fcs in sort.Terms)
                    {
                        if (isFirst)
                            ioqf = ief.AsQueryable()
                                .OrderBy(
                                    o => o[fcs.RubricName],
                                    fcs.Direction,
                                    Comparer<object>.Default
                                );
                        else
                            ioqf = ioqf.ThenBy(
                                o => o[fcs.RubricName],
                                fcs.Direction,
                                Comparer<object>.Default
                            );
                        isFirst = false;
                    }

                    if (appendseries != null)
                        view = ioqf;
                    else
                    {
                        view = ioqf;
                    }
                }
                else
                {
                    if (appendseries != null)
                        view = appendseries.Where(filter.Evaluator).AsQueryable();
                    else
                    {
                        view = series.Where(filter.Evaluator).AsQueryable();
                    }
                }
            }
            else if (sort != null && sort.Terms.Count > 0)
            {
                series.Predicate = null;
                series.Filter.Evaluator = null;

                bool isFirst = true;
                IOrderedQueryable<IInstant> ioqf = null;

                foreach (InstantSeriesSortTerm fcs in sort.Terms)
                {
                    if (isFirst)
                        if (appendseries != null)
                            ioqf = appendseries
                                .AsQueryable()
                                .OrderBy(
                                    o => o[fcs.RubricName],
                                    fcs.Direction,
                                    Comparer<object>.Default
                                );
                        else
                            ioqf = _series
                                .AsQueryable()
                                .OrderBy(
                                    o => o[fcs.RubricName],
                                    fcs.Direction,
                                    Comparer<object>.Default
                                );
                    else
                        ioqf = ioqf.ThenBy(
                            o => o[fcs.RubricName],
                            fcs.Direction,
                            Comparer<object>.Default
                        );

                    isFirst = false;
                }

                if (appendseries != null)
                    view = ioqf;
                else
                {
                    view = ioqf;
                }
            }
            else
            {
                if (stage < 2)
                {
                    series.Predicate = null;
                    series.Filter.Evaluator = null;
                }
            }

            if (stage > 0)
            {
                series.View = view;
            }

            return view;
        }

        private static IQueryable<IInstant> ResolveQuery(
            IInstantSeries series,
            InstantSeriesFilter Filter,
            InstantSeriesSort Sort,
            int stage = 1,
            IInstant[] appendseries = null
        )
        {
            FilterStage filterStage = (FilterStage)Enum.ToObject(typeof(FilterStage), stage);
            int filtercount = Filter.Terms.Where(f => f.Stage.Equals(filterStage)).ToArray().Length;
            int sortcount = Sort.Terms.Count;

            if (filtercount > 0)
                if (sortcount > 0)
                    return ExecuteQuery(series, Filter, Sort, stage, appendseries);
                else
                    return ExecuteQuery(series, Filter, null, stage, appendseries);
            else if (sortcount > 0)
                return ExecuteQuery(series, null, Sort, stage, appendseries);
            else
                return ExecuteQuery(series, null, null, stage, appendseries);
        }
    }
}
