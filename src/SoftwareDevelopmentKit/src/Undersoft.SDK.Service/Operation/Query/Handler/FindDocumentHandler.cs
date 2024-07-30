using MediatR;

namespace Undersoft.SDK.Service.Operation.Query.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Query;

public class FindDocumentHandler<TStore, TEntity, TDto>
    : IRequestHandler<FindDocument<TStore, TEntity, TDto>, Query<TEntity, TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;

    public FindDocumentHandler(IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<Query<TEntity, TDto>> Handle(
        FindDocument<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (request.Keys != null)
            request.Result = await _repository
                .DetalizedFindQueryAsync<TDto>(request.Keys, request.Parameters.Expanders)
                .ConfigureAwait(false);
        else
            request.Result = await _repository
                .DetalizedFindQueryAsync<TDto>(request.Parameters.Filter, request.Parameters.Expanders)
                .ConfigureAwait(false);

        return request;
    }
}
