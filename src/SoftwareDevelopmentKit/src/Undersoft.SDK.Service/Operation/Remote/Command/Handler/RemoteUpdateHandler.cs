using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteUpdateHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteUpdate<TStore, TDto, TModel>, RemoteCommand<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _servicer;

    public RemoteUpdateHandler(IServicer servicer, IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<RemoteCommand<TModel>> Handle(
        RemoteUpdate<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Predicate == null)
            request.Result = await _repository.SetBy(request.Model, request.Keys);
        else if (request.Conditions == null)
            request.Result = await _repository.SetBy(request.Model, request.Predicate);
        else
            request.Result = await _repository.SetBy(
                request.Model,
                request.Predicate,
                request.Conditions
            );

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} for entity " + $"{typeof(TDto).Name} unable update source"
            );

        _ = _servicer
            .Publish(new RemoteUpdated<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
