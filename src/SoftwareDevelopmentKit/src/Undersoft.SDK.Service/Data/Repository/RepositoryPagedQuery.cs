using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Repository;

public partial class Repository<TEntity>
{
    public virtual Task<ISeries<TDto>> FilterAsync<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, sortTerms]);
    }

    public virtual Task<ISeries<TDto>> FilterAsync<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, predicate]);
    }

    public virtual IList<TDto> Filter<TDto, TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector
    ) where TResult : class
    {
        return MapTo<TDto>(
            (take > 0)
                ? Query.Select(selector).Skip(skip).Take(take).ToArray()
                : Query.Select(selector).ToArray()
        );
    }

    public virtual Task<ISeries<TDto>> FilterAsync<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, predicate, expanders]);
    }

    public virtual Task<ISeries<TDto>> FilterAsync<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, predicate, sortTerms]);
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IAsyncEnumerable<TDto> FilterStreamAsync<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapToAsync<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IList<TDto> Filter<TDto, TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate
    ) where TResult : class
    {
        return MapTo<TDto>(this[skip, take, this[predicate]].Select(selector));
    }

    public virtual IList<TDto> Filter<TDto, TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return MapTo<TDto>(this[skip, take, this[expanders]].Select(selector));
    }

    public virtual Task<ISeries<TDto>> FilterAsync<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        if (predicate == null)
            return GetAsync<TDto>(skip, take, sortTerms, expanders);

        if (typeof(TEntity) != typeof(TDto))
            return KeyedMapAsync<TDto>(this[skip, take, predicate, sortTerms, expanders]);
        else
            return Task.FromResult((ISeries<TDto>)((IQueryable<TDto>)this[skip, take, this[predicate, sortTerms, expanders]]).ToListing());
    }

    public virtual Task<IQueryable<TDto>> FilterQueryAsync<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        if (predicate == null)
            return GetQueryAsync<TDto>(skip, take, sortTerms, expanders);

        if (typeof(TEntity) != typeof(TDto))
            return MapQueryAsync<TDto>(this[skip, take, this[predicate, sortTerms, expanders]]);
        else
            return Task.FromResult((IQueryable<TDto>)this[skip, take, this[predicate, sortTerms, expanders]]);
    }

    public virtual IQueryable<TDto> FilterQuery<TDto>(
       int skip,
       int take,
       Expression<Func<TEntity, bool>> predicate,
       SortExpression<TEntity> sortTerms,
       params Expression<Func<TEntity, object>>[] expanders
   ) where TDto : class
    {

        if (typeof(TEntity) != typeof(TDto))
            return MapQuery<TDto>(this[skip, take, this[predicate, sortTerms, expanders]]);
        else
            return (IQueryable<TDto>)this[skip, take, this[predicate, sortTerms, expanders]];
    }

    public virtual IAsyncEnumerable<TDto> FilterStreamAsync<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapToAsync<TDto>(this[skip, take, predicate, sortTerms, expanders]);
    }

    public virtual IList<TDto> Filter<TDto, TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        SortExpression<TEntity> sortTerms,
        Expression<Func<TEntity, bool>> predicate
    ) where TResult : class
    {
        return MapTo<TDto>(this[skip, take, this[predicate, sortTerms]].Select(selector));
    }

    public virtual IList<TDto> Filter<TDto, TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return MapTo<TDto>(
            this[skip, take, this[predicate, sortTerms, expanders]].Select(selector)
        );
    }

    public virtual TDto Find<TDto>(params object[] keys)
    {
        return MapTo<TDto>(this[keys]);
    }

    public virtual TDto Find<TDto>(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return MapTo<TDto>(this[keys, expanders]);
    }

    public virtual TDto Find<TDto>(Expression<Func<TEntity, bool>> predicate, bool reverse)
    {
        return MapTo<TDto>(this[reverse, predicate]);
    }

    public virtual TDto Find<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate
    ) where TResult : class
    {
        return MapTo<TDto>(this.Find(predicate, selector));
    }

    public virtual TDto Find<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return MapTo<TDto>(this.Find(keys, selector, expanders));
    }

    public virtual TDto Find<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        bool reverse,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return MapTo<TDto>(this[reverse, predicate, expanders]);
    }

    public virtual TDto Find<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return MapTo<TDto>(this.Find(predicate, selector, expanders));
    }

    public virtual Task<IQueryable<TDto>> FindQueryAsync<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return MapQueryAsync<TDto>(this[predicate, expanders]);
    }

    public virtual Task<IQueryable<TDto>> FindQueryAsync<TDto>(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return MapQueryAsync<TDto>(new[] { this[keys, expanders] }.AsQueryable());
    }

    public virtual Task<IQueryable<TDto>> DetalizedFindQueryAsync<TDto>(
       Expression<Func<TEntity, bool>> predicate,
       params Expression<Func<TEntity, object>>[] expanders
   ) where TDto : class, IOrigin
    {
        return DetalizeQueryAsync<TDto>(this[predicate, expanders]);
    }

    public virtual Task<IQueryable<TDto>> DetalizedFindQueryAsync<TDto>(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return DetalizeQueryAsync<TDto>(new[] { this[keys, expanders] }.AsQueryable());
    }

    public virtual IQueryable<TDto> FindQuery<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return MapQuery<TDto>(this[predicate, expanders]);
    }

    public virtual IQueryable<TDto> FindQuery<TDto>(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return MapQuery<TDto>(new[] { this[keys, expanders] }.AsQueryable());
    }

    public virtual IList<TDto> Get<TDto, TResult>(Expression<Func<TEntity, TResult>> selector)
        where TResult : class
    {
        return Query.Select(selector).ForEach(s => s.PutTo<TDto>()).ToArray();
    }

    public virtual IList<TDto> Get<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return MapTo<TDto>(this[0, 0, this[expanders]].Select(selector));
    }

    public virtual Task<ISeries<TDto>> Get<TDto>(
        int skip,
        int take,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, expanders]);
    }

    public virtual Task<ISeries<TDto>> GetAsync<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return KeyedMapAsync<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IEnumerable<TDto> GetYield<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return MapTo<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IQueryable<TDto> GetQuery<TDto>(int skip, int take,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapQuery<TDto>(this[skip, take, this[expanders]]);
    }

    public virtual IQueryable<TDto> GetQuery<TDto>(int skip, int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapQuery<TDto>(this[skip, take, this[sortTerms, expanders]]);
    }

    public virtual Task<IQueryable<TDto>> GetQueryAsync<TDto>(int skip, int take,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        if (typeof(TEntity) != typeof(TDto))
            return MapQueryAsync<TDto>(this[skip, take, this[expanders]]);
        else
            return Task.FromResult((IQueryable<TDto>)this[skip, take, this[expanders]]);
    }

    public virtual Task<IQueryable<TDto>> GetQueryAsync<TDto>(int skip, int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        if (typeof(TEntity) != typeof(TDto))
            return MapQueryAsync<TDto>(this[skip, take, this[sortTerms, expanders]]);
        else
            return Task.FromResult((IQueryable<TDto>)this[skip, take, this[sortTerms, expanders]]);
    }

    public virtual Task<IQueryable<TDto>> DetalizedGetQueryAsync<TDto>(int skip, int take,
       SortExpression<TEntity> sortTerms,
       params Expression<Func<TEntity, object>>[] expanders
   ) where TDto : class
    {
        if (typeof(TEntity) != typeof(TDto))
            return DetalizeQueryAsync<TDto>(this[skip, take, this[sortTerms, expanders]]);
        else
            return Task.FromResult((IQueryable<TDto>)this[skip, take, this[sortTerms, expanders]]);
    }

    public virtual IAsyncEnumerable<TDto> GetStreamAsync<TDto>(
        int skip,
        int take,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapToAsync<TDto>(this[skip, take, expanders]);
    }

    public virtual IAsyncEnumerable<TDto> GetStreamAsync<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapToAsync<TDto>(this[skip, take, sortTerms, expanders]);
    }
}
