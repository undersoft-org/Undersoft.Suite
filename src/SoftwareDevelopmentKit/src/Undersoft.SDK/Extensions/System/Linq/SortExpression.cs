using Undersoft.SDK.Series;

namespace Undersoft.SDK
{
    public class SortExpression<T> : SortExpression
    {
        public SortExpression(IEnumerable<Sorter> sorters) : base(sorters)
        {
        }

        public IQueryable<T> Sort(IQueryable<T> query)
        {
            return query.GetSortQuery(SortItems);
        }
    }

    public class SortExpression
    {
        public ISeries<Sorter> SortItems = new Listing<Sorter>();

        public SortExpression() { }

        public SortExpression(IEnumerable<Sorter> sorters)
        {
            if (sorters != null)
                SortItems.Put(sorters);
        }

        public IQueryable<T> Sort<T>(IQueryable<T> query)
        {
            return query.GetSortQuery(SortItems);
        }
    }
}
