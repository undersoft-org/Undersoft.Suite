using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;
using Undersoft.SDK.Service.Operation.Remote.Command.Notification;

public class RemoteUpsertHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteUpsert<TStore, TDto, TModel>, RemoteCommand<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _umaker;

    public RemoteUpsertHandler(IServicer umaker, IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
        _umaker = umaker;
    }

    public async Task<RemoteCommand<TModel>> Handle(
        RemoteUpsert<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Conditions != null)
            request.Result = await _repository.PutBy(
                request.Model,
                request.Predicate,
                request.Conditions
            );
        else
            request.Result = await _repository.PutBy(request.Model, request.Predicate);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} " + $"for entity {typeof(TDto).Name} unable renew source"
            );

        _ = _umaker
            .Publish(new RemoteUpserted<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);
        ;
        return request;
    }
}
