using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Query
{
    public class QueryParameters<T> : QueryParameters, IQueryParameters<T>
    {
        public Expression<Func<T, object>>[] Expanders { get; set; }

        public Expression<Func<T, bool>> Filter { get; set; }

        public Expression<Func<T, object>> Selector { get; set; }

        public SortExpression<T> Sort { get; set; }

        public new T Data { get => (T)(base.Data ??= typeof(T).New<T>()); set => base.Data = value; }
    }

    public class QueryParameters : IQueryParameters
    {
        public virtual int Offset { get; set; }

        public virtual int Limit { get; set; }

        public virtual int Count { get; set; }

        public Listing<FilterItem> FilterItems { get; set; } = new();

        public Listing<SortItem> SortItems { get; set; } = new();

        public Listing<string> ExpandItems { get; set; } = new();

        public Listing<string> SelectItems { get; set; }

        public Expression<Func<T, bool>> GetFilter<T>()
        {
            return new FilterExpression<T>(FilterItems).Create();
        }

        public SortExpression<T> GetSort<T>()
        {
            return new SortExpression<T>(SortItems);
        }

        public Expression<Func<T, object>>[] GetExpanders<T>() where T : class, IInnerProxy
        {
            return ExpandItems
                .ForEach(ei =>
                {
                    Expression<Func<T, object>> exp = a => a.Proxy[ei];
                    return exp;
                })
                .ToArray();
        }

        public Expression<Func<T, object>> GetSelector<T>() where T : class, IInnerProxy
        {
            Expression<Func<T, object>> exp = a => SelectItems.ForEach(item => a.Proxy[item]).ToArray();
            return exp;
        }

        public object Data { get; set; }
    }
}
