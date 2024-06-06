using Undersoft.SDK.Series;

namespace Undersoft.SDK
{
    public class FilterExpression<T> : FilterExpression
    {
        public FilterExpression(IEnumerable<Filter> filters) : base(filters)
        {
        }

        public IQueryable<T> Filter(IQueryable<T> query)
        {
            return query.GetFilterQuery(FilterItems);
        }
    }

    public class FilterExpression
    {
        public ISeries<Filter> FilterItems = new Listing<Filter>();

        public FilterExpression() { }

        public FilterExpression(IEnumerable<Filter> ilters)
        {
            FilterItems.Put(ilters);
        }

        public IQueryable<T> Filter<T>(IQueryable<T> query)
        {
            return query.GetFilterQuery<T>(FilterItems);
        }
    }
}
