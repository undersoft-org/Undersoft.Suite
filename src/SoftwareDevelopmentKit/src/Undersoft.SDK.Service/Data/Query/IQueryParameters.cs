using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Query
{
    public interface IQueryParameters<T> : IQueryParameters
    {
        Expression<Func<T, object>>[] Expanders { get; set; }

        Expression<Func<T, bool>> Filter { get; set; }

        Expression<Func<T, object>> Selector { get; set; }

        SortExpression<T> Sort { get; set; }

        new T Data { get; set; }
    }

    public interface IQueryParameters
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Count { get; set; }

        public Listing<Filter> FilterItems { get; set; }

        public Listing<Sorter> SortItems { get; set; }

        public Listing<string> ExpandItems { get; set; }

        public Listing<string> SelectItems { get; set; }

        public Expression<Func<T, bool>> GetFilter<T>();

        public SortExpression<T> GetSort<T>()
        {
            return new SortExpression<T>(SortItems);
        }

        public Expression<Func<T, object>>[] GetExpanders<T>() where T : class, IInnerProxy;

        public Expression<Func<T, object>> GetSelector<T>() where T : class, IInnerProxy;

        public object Data { get; set; }

    }
}