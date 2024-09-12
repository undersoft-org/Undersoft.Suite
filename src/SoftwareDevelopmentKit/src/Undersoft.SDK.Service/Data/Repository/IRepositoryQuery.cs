using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryQuery<TEntity> : IOrderedQueryable<TEntity>, IEnumerable<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        Task<bool> Exist(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Exist(TEntity entity, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        Task<bool> Exist(Type exceptionType, Expression<Func<TEntity, bool>> predicate, string message);
        Task<bool> Exist(Type exceptionType, object instance, string message);
        Task<bool> Exist<TException>(Expression<Func<TEntity, bool>> predicate, string message) where TException : Exception;
        Task<bool> Exist<TException>(object instance, string message) where TException : Exception;

        Task<IQueryable<TDto>> FilterQueryAsync<TDto>(int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        Task<IQueryable<TDto>> FilterNoTrackedQueryAsync<TDto>(int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        IQueryable<TDto> FilterQuery<TDto>(int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        IAsyncEnumerable<TDto> FilterStreamAsync<TDto>(int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        IAsyncEnumerable<TDto> FilterStreamAsync<TDto>(int skip, int take, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;

        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool reverse = false);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool reverse = false, params Expression<Func<TEntity, object>>[] expanders);
        Task<TEntity> Find(object[] keys, params Expression<Func<TEntity, object>>[] expanders);
        Task<TEntity> Find(params object[] keys);
        TDto Find<TDto, TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate) where TResult : class;
        TDto Find<TDto, TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders) where TResult : class;
        TDto Find<TDto, TResult>(Expression<Func<TEntity, TResult>> selector, object[] keys, params Expression<Func<TEntity, object>>[] expanders) where TResult : class;
        TDto Find<TDto>(Expression<Func<TEntity, bool>> predicate, bool reverse);
        TDto Find<TDto>(Expression<Func<TEntity, bool>> predicate, bool reverse, params Expression<Func<TEntity, object>>[] expanders);
        TDto Find<TDto>(object[] keys, params Expression<Func<TEntity, object>>[] expanders);
        TDto Find<TDto>(params object[] keys);
        Task<TResult> Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
        Task<TResult> Find<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, object>>[] expanders);
        Task<TResult> Find<TResult>(object[] keys, Expression<Func<TEntity, TResult>> selector, params Expression<Func<TEntity, object>>[] expanders) where TResult : class;
        IQueryable<TDto> FindQuery<TDto>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders) where TDto : class, IOrigin;
        IQueryable<TDto> FindQuery<TDto>(object[] keys, params Expression<Func<TEntity, object>>[] expanders) where TDto : class, IOrigin;
        Task<IQueryable<TDto>> FindQueryAsync<TDto>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders) where TDto : class, IOrigin;
        Task<IQueryable<TDto>> FindQueryAsync<TDto>(object[] keys, params Expression<Func<TEntity, object>>[] expanders) where TDto : class, IOrigin;
        IAsyncEnumerable<TDto> GetStreamAsync<TDto>(int skip, int take, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        IAsyncEnumerable<TDto> GetStreamAsync<TDto>(int skip, int take, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;

        IQueryable<TDto> GetQuery<TDto>(int skip, int take, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        IQueryable<TDto> GetQuery<TDto>(int skip, int take, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        Task<IQueryable<TDto>> GetQueryAsync<TDto>(int skip, int take, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        Task<IQueryable<TDto>> GetQueryAsync<TDto>(int skip, int take, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;
        Task<IQueryable<TDto>> GetNoTrackedQueryAsync<TDto>(int skip, int take, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;

        Task<bool> NotExist(Expression<Func<TEntity, bool>> predicate);
        Task<bool> NotExist(TEntity entity, Func<TEntity, Expression<Func<TEntity, bool>>> predicate);
        Task<bool> NotExist(Type exceptionType, Expression<Func<TEntity, bool>> predicate, string message);
        Task<bool> NotExist(Type exceptionType, object instance, string message);
        Task<bool> NotExist<TException>(Expression<Func<TEntity, bool>> predicate, string message) where TException : Exception;
        Task<bool> NotExist<TException>(object instance, string message) where TException : Exception;

        IQueryable<TEntity> Sort(IQueryable<TEntity> query, SortExpression<TEntity> sortTerms);

        Task<IQueryable<TDto>> DetalizedGetQueryAsync<TDto>(int skip, int take, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;

        Task<IQueryable<TDto>> DetalizedFilterQueryAsync<TDto>(int skip, int take, Expression<Func<TEntity, bool>> predicate, SortExpression<TEntity> sortTerms, params Expression<Func<TEntity, object>>[] expanders) where TDto : class;

        Task<IQueryable<TDto>> DetalizedFindQueryAsync<TDto>(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] expanders) where TDto : class, IOrigin;
        Task<IQueryable<TDto>> DetalizedFindQueryAsync<TDto>(object[] keys, params Expression<Func<TEntity, object>>[] expanders) where TDto : class, IOrigin;
    }
}