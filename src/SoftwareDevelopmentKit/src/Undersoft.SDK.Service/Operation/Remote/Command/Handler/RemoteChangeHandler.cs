using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteChangeHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteChange<TStore, TDto, TModel>, RemoteCommand<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _servicer;

    public RemoteChangeHandler(IServicer servicer, IRemoteRepository<TStore, TDto> repository)
    {
        _servicer = servicer;
        _repository = repository;
    }

    public virtual async Task<RemoteCommand<TModel>> Handle(
        RemoteChange<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Keys != null)
            request.Result = await _repository
                .PatchBy(request.Model, request.Keys)
                .ConfigureAwait(false);
        else
            request.Result = await _repository
                .PatchBy(request.Model, request.Predicate)
                .ConfigureAwait(false);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} for entity " + $"{typeof(TModel).Name} unable patch source"
            );

        _ = _servicer
            .Publish(new RemoteChanged<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
