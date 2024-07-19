using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository;

public abstract partial class Repository<TEntity> : IRepositoryIndexer<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    public abstract TEntity this[params object[] keys] { get; set; }

    public abstract TEntity this[object[] keys, Expression<Func<TEntity, object>>[] expanders] { get; set; }

    public abstract object this[Expression<Func<TEntity, object>> selector, object[] keys, params Expression<Func<TEntity, object>>[] expanders] { get; set; }

    public virtual TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate] =>
        (reverse)
            ? this[predicate].LastOrDefault()
            : this[predicate].FirstOrDefault();

    public virtual TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms] =>
        (reverse)
            ? this[predicate, sortTerms].LastOrDefault()
            : this[predicate, sortTerms].FirstOrDefault();

    public virtual TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] =>
        (reverse)
            ? this[predicate, expanders].LastOrDefault()
            : this[predicate, expanders].FirstOrDefault();

    public virtual TEntity this[bool reverse, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] =>
        (reverse)
            ? this[predicate, sortTerms, expanders].LastOrDefault()
            : this[predicate, sortTerms, expanders].FirstOrDefault();

    public virtual IQueryable<TEntity> this[int skip, int take, IQueryable<TEntity> query] => (take > 0) ? query.Skip(skip).Take(take) : query;

    public virtual IQueryable<TEntity> this[int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] => Sort(this[skip, take, this[predicate, expanders]], sortTerms);

    public virtual IQueryable<TEntity> this[IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] expanders]
    {
        get
        {
            IQueryable<TEntity> _query = query;
            if (expanders != null)
            {
                foreach (var expander in expanders)
                {
                    _query.Include(expander);
                }
            }

            return _query;
        }
    }

    public virtual IQueryable<TEntity> this[IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate] => query.Where(predicate);

    public virtual IQueryable<object> this[IQueryable<TEntity> query, Expression<Func<TEntity, object>> selector] => query.Select(selector);

    public virtual IQueryable<object> this[IQueryable<TEntity> query, Expression<Func<TEntity, int, object>> selector] => query.Select(selector);

    public virtual IQueryable<TEntity> this[IQueryable<TEntity> query, Expression<Func<TEntity, object>> selector, IEnumerable<object> values] => query.WhereIn(selector, values);

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate] =>
        Query.Where(predicate);

    public virtual IQueryable<TEntity> this[SortExpression<TEntity> sortTerms] =>
        Sort(Query, sortTerms);

    public virtual IQueryable<object> this[Expression<Func<TEntity, object>> selector] =>
        Query.Select(selector);

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, object>>[] expanders]
    {
        get
        {
            IQueryable<TEntity> query = Query;
            if (expanders != null)
            {
                foreach (var expander in expanders)
                {
                    query.Include(expander);
                }
            }

            return query;
        }
    }

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, object>> selector, IEnumerable<object> values] => Query.WhereIn(selector, values);

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, object>> selector, IEnumerable<object> values, params Expression<Func<TEntity, object>>[] expanders] =>
        this[this[expanders], selector, values];

    public virtual IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, object>>[] expanders] => this[this[expanders], selector];

    public virtual IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] =>
        this[this[predicate, expanders], selector];

    public virtual IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] => this[Sort(this[predicate, expanders], sortTerms), selector];

    public virtual IQueryable<object> this[Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate] => this[this[predicate], selector];

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms] => Sort(this[predicate], sortTerms);

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders] =>
        this[this[predicate], expanders];

    public virtual IQueryable<TEntity> this[SortExpression<TEntity> sortTerms, Expression<Func<TEntity, object>>[] expanders] => Sort(this[expanders], sortTerms);

    public virtual IQueryable<TEntity> this[Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders] => Sort(this[predicate, expanders], sortTerms);

    public virtual IQueryable<IGrouping<dynamic, TEntity>> this[Func<IQueryable<TEntity>, IQueryable<IGrouping<dynamic, TEntity>>> groupByObject, Expression<Func<TEntity, bool>> predicate] =>
        groupByObject(Query.Where(predicate));
}
