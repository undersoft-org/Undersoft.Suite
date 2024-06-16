using MediatR;

namespace Undersoft.SDK.Service.Operation.Query.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Query;

public class FilterAsyncHandler<TStore, TParams, TEntity, TDto>
    : IStreamRequestHandler<FilterAsync<TStore, TEntity, TDto>, TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
    where TParams : class
{
    protected readonly IStoreRepository<TEntity> _repository;

    public FilterAsyncHandler(IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual IAsyncEnumerable<TDto> Handle(
        FilterAsync<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        return _repository.FilterStreamAsync<TDto>(
            request.Offset,
            request.Limit,
            request.Parameters.Filter,
            request.Parameters.Sort,
            request.Parameters.Expanders
        );
    }
}
