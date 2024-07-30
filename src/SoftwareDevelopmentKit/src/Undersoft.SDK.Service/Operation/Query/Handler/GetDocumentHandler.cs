using MediatR;

namespace Undersoft.SDK.Service.Operation.Query.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Query;

public class GetDocumentHandler<TStore, TEntity, TDto>
    : IRequestHandler<GetDocument<TStore, TEntity, TDto>, Query<TEntity, TDto>>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{
    protected readonly IStoreRepository<TEntity> _repository;

    public GetDocumentHandler(IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<Query<TEntity, TDto>> Handle(
        GetDocument<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        request.Result = await _repository
            .DetalizedGetQueryAsync<TDto>(
                request.Offset,
                request.Limit,
                request.Parameters.Sort,
                request.Parameters.Expanders
            )
            .ConfigureAwait(false);

        return request;
    }
}
