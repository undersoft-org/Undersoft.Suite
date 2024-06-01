using MediatR;

namespace Undersoft.SDK.Service.Operation.Query.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Query;

public class FindHandler<TStore, TEntity, TDto>
    : IRequestHandler<Find<TStore, TEntity, TDto>, Query<TEntity, TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;

    public FindHandler(IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<Query<TEntity, TDto>> Handle(
        Find<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (request.Keys != null)
            request.Result = await _repository
                .FindQueryAsync<TDto>(request.Keys, request.Parameters.Expanders)
                .ConfigureAwait(false);
        else
            request.Result = await _repository
                .FindQueryAsync<TDto>(request.Parameters.Filter, request.Parameters.Expanders)
                .ConfigureAwait(false);

        return request;
    }
}
