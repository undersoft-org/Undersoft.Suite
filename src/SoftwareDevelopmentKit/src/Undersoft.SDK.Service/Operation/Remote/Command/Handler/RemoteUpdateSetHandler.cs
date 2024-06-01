using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteUpdateSetHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteUpdateSet<TStore, TDto, TModel>, RemoteCommandSet<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _servicer;

    public RemoteUpdateSetHandler(IServicer servicer, IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<RemoteCommandSet<TModel>> Handle(
        RemoteUpdateSet<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TDto> entities = null;
        if (request.Predicate == null)
            entities = _repository.SetBy(request.ForOnly(d => d.IsValid, d => d.Model).Commit());
        else if (request.Conditions == null)
            entities = _repository.SetBy(
                request.ForOnly(d => d.IsValid, d => d.Model).Commit(),
                request.Predicate
            );
        else
            entities = _repository.SetBy(
                request.ForOnly(d => d.IsValid, d => d.Model).Commit(),
                request.Predicate,
                request.Conditions
            );

        await entities
            .ForEachAsync(
                (e) =>
                {
                    request[e.Id].Result = e;
                }
            )
            .ConfigureAwait(false);

        _ = _servicer
            .Publish(new RemoteUpdatedSet<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
