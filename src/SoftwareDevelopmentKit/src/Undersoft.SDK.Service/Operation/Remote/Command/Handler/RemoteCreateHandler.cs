using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteCreateHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteCreate<TStore, TDto, TModel>, RemoteCommand<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _servicer;

    public RemoteCreateHandler(IServicer servicer, IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<RemoteCommand<TModel>> Handle(
        RemoteCreate<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        request.Result = await _repository.AddByAsync(request.Model, request.Predicate);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} " + $"for entity {typeof(TDto).Name} " + $"unable create source"
            );

        _ = _servicer
            .Publish(new RemoteCreated<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
