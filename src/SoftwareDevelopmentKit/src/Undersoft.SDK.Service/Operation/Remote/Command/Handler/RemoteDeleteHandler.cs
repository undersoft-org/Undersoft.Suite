using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteDeleteHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteDelete<TStore, TDto, TModel>, RemoteCommand<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _umaker;

    public RemoteDeleteHandler(IServicer umaker, IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
        _umaker = umaker;
    }

    public async Task<RemoteCommand<TModel>> Handle(
        RemoteDelete<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Keys != null)
            request.Result = await _repository.Delete(request.Keys);
        else if (request.Model == null && request.Predicate != null)
            request.Result = await _repository.Delete(request.Predicate);
        else
            request.Result = await _repository.DeleteBy(request.Model, request.Predicate);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} for entity" + $" {typeof(TDto).Name} unable delete source"
            );

        _ = _umaker.Publish(new RemoteDeleted<TStore, TDto, TModel>(request), cancellationToken).ConfigureAwait(false);
        ;

        return request;
    }
}
