using Microsoft.OData.UriParser;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Query
{
    public class QueryParameters<T> : QueryParameters, IQueryParameters<T>
    {
        public QueryParameters() { }

        public QueryParameters(IEnumerable<Filter> filters, IEnumerable<Sorter> sorters)
            : base(filters, sorters)
        {
            Filter = GetFilter<T>();
            Sort = GetSort<T>();
        }

        public Expression<Func<T, object>>[] Expanders { get; set; }

        public Expression<Func<T, bool>> Filter { get; set; }

        public Expression<Func<T, object>> Selector { get; set; }

        public SortExpression<T> Sort { get; set; }

        public new T Data
        {
            get => (T)(base.Data ??= typeof(T).New<T>());
            set => base.Data = value;
        }
    }

    public class QueryParameters : IQueryParameters
    {
        public QueryParameters() { }

        public QueryParameters(IEnumerable<Filter> filters, IEnumerable<Sorter> sorters)
        {
            if (filters != null)
                FilterItems.Add(filters);
            SortItems.Add(sorters);
        }

        public virtual int Offset { get; set; }

        public virtual int Limit { get; set; }

        public virtual int Count { get; set; }

        public virtual ODataQueryOptionParser OpenQueryParser { get; set; }

        public Listing<Filter> FilterItems { get; set; } = new();

        public Listing<Sorter> SortItems { get; set; } = new();

        public Listing<string> ExpandItems { get; set; } = new();

        public Listing<string> SelectItems { get; set; } = new();

        public Expression<Func<T, bool>> GetFilter<T>()
        {
            return FilterItems.GetFilterExpression<T>();
        }

        public SortExpression<T> GetSort<T>()
        {
            return new SortExpression<T>(SortItems);
        }

        public Expression<Func<T, object>>[] GetExpanders<T>() where T : class, IInnerProxy
        {
            return ExpandItems
                .ForEach(ei => ei.GetMemberExpression<T>())
                .ToArray();
        }

        public Expression<Func<T, object>> GetSelector<T>() where T : class, IInnerProxy
        {
            Expression<Func<T, object>> exp = a =>
                SelectItems.ForEach(item => item.GetMemberExpression<T>()).ToArray();
            return exp;
        }

        public object Data { get; set; }
    }
}
