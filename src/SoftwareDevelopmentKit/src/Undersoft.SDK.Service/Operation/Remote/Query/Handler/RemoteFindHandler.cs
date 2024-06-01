using MediatR;
using Undersoft.SDK.Service.Data.Remote.Repository;

namespace Undersoft.SDK.Service.Operation.Remote.Query.Handler;

public class RemoteFindHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteFind<TStore, TDto, TModel>, RemoteQuery<TDto, TModel>>
    where TModel : class, IOrigin, IInnerProxy
    where TDto : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;

    public RemoteFindHandler(IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
    }

    public virtual async Task<RemoteQuery<TDto, TModel>> Handle(
        RemoteFind<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (request.Keys != null)
            request.Result = await _repository
                .FindQueryAsync<TModel>(request.Keys, request.Parameters.Expanders)
                .ConfigureAwait(false);
        else
            request.Result = await _repository
                .FindQueryAsync<TModel>(request.Parameters.Filter, request.Parameters.Expanders)
                .ConfigureAwait(false);

        return request;
    }
}
