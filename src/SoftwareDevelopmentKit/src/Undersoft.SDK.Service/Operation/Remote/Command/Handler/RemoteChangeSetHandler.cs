using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteChangeSetHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteChangeSet<TStore, TDto, TModel>, RemoteCommandSet<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _servicer;

    public RemoteChangeSetHandler(IServicer servicer, IRemoteRepository<TStore, TDto> repository)
    {
        _servicer = servicer;
        _repository = repository;
    }

    public virtual async Task<RemoteCommandSet<TModel>> Handle(
        RemoteChangeSet<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {

        IEnumerable<TDto> dtos;
        if (request.Predicate == null)
            dtos = _repository.Patch(request.ForOnly(d => d.IsValid, d => d.Model).Commit());
        else
            dtos = _repository.Patch(
                request.ForOnly(d => d.IsValid, d => d.Model).Commit(),
                request.Predicate
            );

        await dtos
            .ForEachAsync(
                (e) =>
                {
                    request[e.Id].Result = e;
                }
            )
            .ConfigureAwait(false);

        _ = _servicer
            .Publish(new RemoteChangedSet<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
