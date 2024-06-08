using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Repository.Pagination;

namespace Undersoft.SDK.Service.Data.Repository;

public partial class Repository<TEntity>
{
    public IPage<TEntity> AsPage(int pageIndex, int pageSize, int indexFrom = 0)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        IndexFrom = indexFrom;
        TotalCount = this.Count();
        TotalPages = (int)Math.Ceiling(TotalCount / ((double)PageSize));
        return this as IPage<TEntity>;
    }

    public virtual IList<TDto> Filter<TDto>(IQueryable<TEntity> query)
    {
        return MapTo<TDto>(query);
    }

    public virtual IList<TEntity> Filter<TDto>(IQueryable<TDto> query)
    {
        return MapFrom<TDto>(query);
    }

    public virtual Task<IPagedSet<TResult>> PagedFilter<TResult>(
        Expression<Func<TEntity, TResult>> selector
    ) where TResult : class
    {
        return Task.Run(
            () =>
                (IPagedSet<TResult>)
                    new PagedSet<TResult>(
                        (PageSize > 0)
                            ? Query
                                .Select(selector)
                                .Skip((PageIndex - IndexFrom) * PageSize)
                                .Take(PageSize)
                                .ToArray()
                            : Query.Select(selector).ToArray(),
                        PageIndex,
                        PageSize,
                        IndexFrom
                    ),
            Cancellation
        );
    }

    public async Task<IPagedSet<TEntity>> PagedFilter(SortExpression<TEntity> sortTerms)
    {
        Items = await Filter((PageIndex - IndexFrom) * PageSize, PageSize, sortTerms);
        return this;
    }

    public async Task<IPagedSet<TEntity>> PagedFilter(Expression<Func<TEntity, bool>> predicate)
    {
        Items = await Filter((PageIndex - IndexFrom) * PageSize, PageSize, predicate);
        return this;
    }

    public async Task<IPagedSet<TDto>> PagedFilter<TDto>(SortExpression<TEntity> sortTerms)
    {
        return new PagedSet<TDto>(
            await Filter<TDto>((PageIndex - IndexFrom) * PageSize, PageSize, sortTerms),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public async Task<IPagedSet<TDto>> PagedFilter<TDto>(Expression<Func<TEntity, bool>> predicate)
    {
        return new PagedSet<TDto>(
            await Filter<TDto>((PageIndex - IndexFrom) * PageSize, PageSize, predicate),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual IPagedSet<TDto> PagedFilter<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector
    ) where TResult : class
    {
        return new PagedSet<TDto>(
            Filter<TDto, TResult>((PageIndex - IndexFrom) * PageSize, PageSize, selector),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual Task<IPagedSet<TResult>> PagedFilter<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate
    ) where TResult : class
    {
        return Task.Run(
            () =>
                (IPagedSet<TResult>)
                    new PagedSet<TResult>(
                        this[(PageIndex - IndexFrom) * PageSize, PageSize, this[predicate]].Select(
                            selector
                        ),
                        PageIndex,
                        PageSize,
                        IndexFrom
                    ),
            Cancellation
        );
    }

    public virtual Task<IPagedSet<TResult>> PagedFilter<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return Task.Run(
            () =>
                (IPagedSet<TResult>)
                    new PagedSet<TResult>(
                        this[(PageIndex - IndexFrom) * PageSize, PageSize, this[expanders]].Select(
                            selector
                        ),
                        PageIndex,
                        PageSize,
                        IndexFrom
                    ),
            Cancellation
        );
    }

    public async Task<IPagedSet<TEntity>> PagedFilter(
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms
    )
    {
        Items = await Filter((PageIndex - IndexFrom) * PageSize, PageSize, predicate, sortTerms);
        return this;
    }

    public async Task<IPagedSet<TEntity>> PagedFilter(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        Items = await Filter((PageIndex - IndexFrom) * PageSize, PageSize, predicate, expanders);
        return this;
    }

    public async Task<IPagedSet<TEntity>> PagedFilter(
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        Items = await Filter((PageIndex - IndexFrom) * PageSize, PageSize, sortTerms, expanders);
        return this;
    }

    public async Task<IPagedSet<TDto>> PagedFilter<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms
    )
    {
        return new PagedSet<TDto>(
            await Filter<TDto>((PageIndex - IndexFrom) * PageSize, PageSize, predicate, sortTerms),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public async Task<IPagedSet<TDto>> PagedFilter<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return new PagedSet<TDto>(
            await Filter<TDto>((PageIndex - IndexFrom) * PageSize, PageSize, predicate, expanders),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public async Task<IPagedSet<TDto>> PagedFilter<TDto>(
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return new PagedSet<TDto>(
            await Filter<TDto>((PageIndex - IndexFrom) * PageSize, PageSize, sortTerms, expanders),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual IPagedSet<TDto> PagedFilter<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate
    ) where TResult : class
    {
        return new PagedSet<TDto>(
            Filter<TDto, TResult>(
                (PageIndex - IndexFrom) * PageSize,
                PageSize,
                selector,
                predicate
            ),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual IPagedSet<TDto> PagedFilter<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return new PagedSet<TDto>(
            Filter<TDto, TResult>(
                (PageIndex - IndexFrom) * PageSize,
                PageSize,
                selector,
                expanders
            ),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms
    )
    {
        return HashMapTo<TDto>(this[skip, take, sortTerms]);
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return HashMapTo<TDto>(this[skip, take, predicate]);
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

    public async Task<IPagedSet<TEntity>> PagedFilter(
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        Items = await Filter(
            (PageIndex - IndexFrom) * PageSize,
            PageSize,
            predicate,
            sortTerms,
            expanders
        );
        return this;
    }

    public async Task<IPagedSet<TDto>> PagedFilter<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return new PagedSet<TDto>(
            await Filter<TDto>(
                (PageIndex - IndexFrom) * PageSize,
                PageSize,
                predicate,
                sortTerms,
                expanders
            ),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return HashMapTo<TDto>(this[skip, take, predicate, expanders]);
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms
    )
    {
        return HashMapTo<TDto>(this[skip, take, predicate, sortTerms]);
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return HashMapTo<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IAsyncEnumerable<TDto> FilterAsync<TDto>(
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

    public virtual Task<IPagedSet<TResult>> PagedFilter<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return Task.Run(
            () =>
                (IPagedSet<TResult>)
                    new PagedSet<TResult>(
                        this[
                            (PageIndex - IndexFrom) * PageSize,
                            PageSize,
                            this[predicate, sortTerms, expanders]
                        ].Select(selector),
                        PageIndex,
                        PageSize,
                        IndexFrom
                    ),
            Cancellation
        );
    }

    public virtual IPagedSet<TDto> PagedFilter<TDto, TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return new PagedSet<TDto>(
            Filter<TDto, TResult>(
                (PageIndex - IndexFrom) * PageSize,
                PageSize,
                selector,
                predicate,
                sortTerms,
                expanders
            ),
            PageIndex,
            PageSize,
            IndexFrom
        );
    }

    public virtual Task<ISeries<TDto>> Filter<TDto>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        if (typeof(TEntity) != typeof(TDto))
            return HashMapTo<TDto>(this[skip, take, predicate, sortTerms, expanders]);
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
        if (typeof(TEntity) != typeof(TDto))
            return QueryMapAsyncTo<TDto>(this[skip, take, this[predicate, sortTerms, expanders]]);
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
            return QueryMapTo<TDto>(this[skip, take, this[predicate, sortTerms, expanders]]);
        else
            return (IQueryable<TDto>)this[skip, take, this[predicate, sortTerms, expanders]];
    }

    public virtual IAsyncEnumerable<TDto> FilterAsync<TDto>(
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
        return QueryMapAsyncTo<TDto>(this[predicate, expanders]);
    }

    public virtual Task<IQueryable<TDto>> FindQueryAsync<TDto>(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return QueryMapAsyncTo<TDto>(new[] { this[keys, expanders] }.AsQueryable());
    }

    public virtual IQueryable<TDto> FindQuery<TDto>(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return QueryMapTo<TDto>(this[predicate, expanders]);
    }

    public virtual IQueryable<TDto> FindQuery<TDto>(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class, IOrigin
    {
        return QueryMapTo<TDto>(new[] { this[keys, expanders] }.AsQueryable());
    }

    public virtual IList<TDto> Get<TDto, TResult>(Expression<Func<TEntity, TResult>> selector)
        where TResult : class
    {
        return Query.Select(selector).ForEach(s => s.PutTo<TDto>()).ToArray();
    }

    public async Task<IPagedSet<TEntity>> PagedGet(
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        Items = await this.Get((PageIndex - IndexFrom) * PageSize, PageSize, expanders)
            .ConfigureAwait(false);
        return this;
    }

    public async Task<IPagedSet<TDto>> PagedGet<TDto>(
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return new PagedSet<TDto>(
            await Get<TDto>((PageIndex - IndexFrom) * PageSize, PageSize, expanders)
                .ConfigureAwait(false),
            PageIndex,
            PageSize,
            IndexFrom
        );
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
        return HashMapTo<TDto>(this[skip, take, expanders]);
    }

    public virtual Task<ISeries<TDto>> Get<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return HashMapTo<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IEnumerable<TDto> GetYield<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return YieldMapTo<TDto>(this[skip, take, sortTerms, expanders]);
    }

    public virtual IQueryable<TDto> GetQuery<TDto>(int skip, int take,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return QueryMapTo<TDto>(this[skip, take, this[expanders]]);
    }

    public virtual IQueryable<TDto> GetQuery<TDto>(int skip, int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return QueryMapTo<TDto>(this[skip, take, this[sortTerms, expanders]]);
    }

    public virtual Task<IQueryable<TDto>> GetQueryAsync<TDto>(int skip, int take,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return QueryMapAsyncTo<TDto>(this[skip, take, this[expanders]]);
    }

    public virtual Task<IQueryable<TDto>> GetQueryAsync<TDto>(int skip, int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return QueryMapAsyncTo<TDto>(this[skip, take, this[sortTerms, expanders]]);
    }

    public virtual IAsyncEnumerable<TDto> GetAsync<TDto>(
        int skip,
        int take,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapToAsync<TDto>(this[skip, take, expanders]);
    }

    public virtual IAsyncEnumerable<TDto> GetAsync<TDto>(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TDto : class
    {
        return MapToAsync<TDto>(this[skip, take, sortTerms, expanders]);
    }
}
