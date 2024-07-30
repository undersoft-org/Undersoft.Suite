using Microsoft.OData.UriParser;
using System.Linq.Expressions;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Data.Query
{
    public class QueryParameters<T> : QueryParameters, IQueryParameters<T>
        where T : class, IInnerProxy
    {
        Expression<Func<T, object>>[] _expanders;

        Expression<Func<T, bool>> _filter;

        Expression<Func<T, object>> _selector;

        public SortExpression<T> _sort;

        public QueryParameters() { }

        public QueryParameters(IEnumerable<Filter> filters, IEnumerable<Sorter> sorters)
            : base(filters, sorters) { }

        public QueryParameters(IEnumerable<Filter> filters, IEnumerable<Sorter> sorters, IEnumerable<string> expanders)
            : base(filters, sorters, expanders) { }

        public QueryParameters(IQueryParameters parameters) : base(parameters) { }

        public Expression<Func<T, object>>[] Expanders { get => _expanders ??= GetExpanders<T>(); set => _expanders = value; }

        public Expression<Func<T, bool>> Filter { get => _filter ??= GetFilter<T>(); set => _filter = value; }

        public Expression<Func<T, object>> Selector { get => _selector ??= GetSelector<T>(); set => _selector = value; }

        public SortExpression<T> Sort { get => _sort ??= GetSort<T>(); set => _sort = value; }

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
        public QueryParameters(IEnumerable<Filter> filters, IEnumerable<Sorter> sorters, IEnumerable<string> expanders) : this(filters, sorters)
        {
            ExpandItems.Add(expanders);
        }

        public QueryParameters(IQueryParameters parameters)
        {
            if (parameters != null)
            {
                if (parameters.FilterItems != null)
                    FilterItems.Add(parameters.FilterItems);
                if (parameters.SelectItems != null)
                    SelectItems.Add(parameters.SelectItems);
                if (parameters.ExpandItems != null)
                    ExpandItems.Add(parameters.ExpandItems);
                SortItems.Add(parameters.SortItems);
            }
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
            return ExpandItems.ForEach(ei => ei.GetMemberExpression<T>()).ToArray();
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
