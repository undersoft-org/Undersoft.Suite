using MediatR;

namespace Undersoft.SDK.Service.Operation.Query.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Query;

public class GetHandler<TStore, TEntity, TDto>
    : IRequestHandler<Get<TStore, TEntity, TDto>, Query<TEntity, TDto>>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{
    protected readonly IStoreRepository<TEntity> _repository;

    public GetHandler(IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<Query<TEntity, TDto>> Handle(
        Get<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        request.Result = await _repository
            .GetNoTrackedQueryAsync<TDto>(
                request.Offset,
                request.Limit,
                request.Parameters.Sort,
                request.Parameters.Expanders
            )
            .ConfigureAwait(false);

        return request;
    }
}
