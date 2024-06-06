using System.Linq.Expressions;

namespace Undersoft.SDK
{
    public interface ISorter : IIdentifiable
    {
        SortDirection Direction { get; set; }
        string Member { get; set; }

        Expression<Func<TEntity, object>> GetSortMemberExpression<TEntity>();
    }
}