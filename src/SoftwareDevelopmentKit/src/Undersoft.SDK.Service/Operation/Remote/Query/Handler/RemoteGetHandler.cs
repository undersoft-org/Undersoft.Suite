using MediatR;
using Undersoft.SDK.Service.Data.Remote.Repository;

namespace Undersoft.SDK.Service.Operation.Remote.Query.Handler;

public class RemoteGetHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteGet<TStore, TDto, TModel>, RemoteQuery<TDto, TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
    where TModel : class, IOrigin, IInnerProxy
{
    protected readonly IRemoteRepository<TDto> _repository;

    public RemoteGetHandler(IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
    }

    public virtual async Task<RemoteQuery<TDto, TModel>> Handle(
        RemoteGet<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        request.Result = await _repository
            .GetNoTrackedQueryAsync<TModel>(
                request.Offset,
                request.Limit,
                request.Parameters.Sort,
                request.Parameters.Expanders
            )
            .ConfigureAwait(false);

        return request;
    }
}
