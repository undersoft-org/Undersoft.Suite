using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryIndexer<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate] { get; }
        IQueryable<TEntity> this[Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<object> this[Expression<Func<TEntity, object>> selector] { get; }
        TEntity this[params object[] keys] { get; set; }
        IQueryable<TEntity> this[SortExpression<TEntity> sortTerms] { get; }
        TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate] { get; }
        TEntity this[bool reverse, Expression<Func<TEntity, object>>[] expanders] { get; }
        object this[bool reverse, Expression<Func<TEntity, object>> selector] { get; }
        TEntity this[bool reverse, SortExpression<TEntity> sortTerms] { get; }
        IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms] { get; }
        IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate] { get; }
        IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<TEntity> this[Expression<Func<TEntity, object>> selector, IEnumerable<object> values] { get; }
        IQueryable<IGrouping<dynamic, TEntity>> this[Func<IQueryable<TEntity>, IQueryable<IGrouping<dynamic, TEntity>>> groupByObject, Expression<Func<TEntity, bool>> predicate] { get; }
        IQueryable<TEntity> this[IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate] { get; }
        IQueryable<object> this[IQueryable<TEntity> query, Expression<Func<TEntity, int, object>> selector] { get; }
        IQueryable<object> this[IQueryable<TEntity> query, Expression<Func<TEntity, object>> selector] { get; }
        IQueryable<TEntity> this[IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] expanders] { get; }
        TEntity this[object[] keys, Expression<Func<TEntity, object>>[] expanders] { get; set; }
        IQueryable<TEntity> this[SortExpression<TEntity> sortTerms, Expression<Func<TEntity, object>>[] expanders] { get; }
        TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] { get; }
        TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms] { get; }
        object this[bool reverse, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, object>>[] expanders] { get; }
        TEntity this[bool reverse, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<TEntity> this[Expression<Func<TEntity, object>> selector, IEnumerable<object> values, params Expression<Func<TEntity, object>>[] expanders] { get; }
        object this[Expression<Func<TEntity, object>> selector, object[] keys, params Expression<Func<TEntity, object>>[] expanders] { get; set; }
        ISeries<TEntity> this[int skip, int take, Expression<Func<TEntity, bool>> predicate] { get; }
        ISeries<TEntity> this[int skip, int take, Expression<Func<TEntity, object>>[] expanders] { get; }
        IList<object> this[int skip, int take, Expression<Func<TEntity, object>> selector] { get; }
        IQueryable<TEntity> this[int skip, int take, IQueryable<TEntity> query] { get; }
        ISeries<TEntity> this[int skip, int take, SortExpression<TEntity> sortTerms] { get; }
        IQueryable<TEntity> this[IQueryable<TEntity> query, Expression<Func<TEntity, object>> selector, IEnumerable<object> values] { get; }
        TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
        object this[bool reverse, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
        ISeries<TEntity> this[int skip, int take, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] { get; }
        ISeries<TEntity> this[int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms] { get; }
        IList<object> this[int skip, int take, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, object>>[] expanders] { get; }
        ISeries<TEntity> this[int skip, int take, SortExpression<TEntity> sortTerms, Expression<Func<TEntity, object>>[] expanders] { get; }
        object this[bool reverse, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
        ISeries<TEntity> this[int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IList<object> this[int skip, int take, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] { get; }
        IList<object> this[int skip, int take, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] { get; }
    }
}