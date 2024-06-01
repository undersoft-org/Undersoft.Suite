using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Data.Repository.Pagination
{
    public interface IPage<TEntity> : IPagedSet<TEntity>
    {
        IPage<TEntity> AsPage(int pageIndex, int pageSize, int indexFrom = 0);

        Task<IPagedSet<TEntity>> PagedGet(params Expression<Func<TEntity, object>>[] expanders);
        Task<IPagedSet<TModel>> PagedGet<TModel>(params Expression<Func<TEntity, object>>[] expanders);

        Task<IPagedSet<TEntity>> PagedFilter(SortExpression<TEntity> sortTerms);
        Task<IPagedSet<TEntity>> PagedFilter(Expression<Func<TEntity, bool>> predicate);
        Task<IPagedSet<TEntity>> PagedFilter(Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms);
        Task<IPagedSet<TEntity>> PagedFilter(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders);
        Task<IPagedSet<TEntity>> PagedFilter(Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders);
        Task<IPagedSet<TEntity>> PagedFilter(SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders);

        Task<IPagedSet<TModel>> PagedFilter<TModel>(SortExpression<TEntity> sortTerms);
        Task<IPagedSet<TModel>> PagedFilter<TModel>(Expression<Func<TEntity, bool>> predicate);
        Task<IPagedSet<TModel>> PagedFilter<TModel>(Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms);
        Task<IPagedSet<TModel>> PagedFilter<TModel>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders);
        Task<IPagedSet<TModel>> PagedFilter<TModel>(Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders);
        Task<IPagedSet<TModel>> PagedFilter<TModel>(SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders);

    }

}
