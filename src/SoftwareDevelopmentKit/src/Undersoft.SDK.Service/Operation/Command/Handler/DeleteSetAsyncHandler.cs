using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class DeleteSetAsyncHandler<TStore, TEntity, TDto>
    : IStreamRequestHandler<DeleteSetAsync<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public DeleteSetAsyncHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _servicer = servicer;
        _repository = repository;
    }

    public virtual IAsyncEnumerable<Command<TDto>> Handle(
        DeleteSetAsync<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IAsyncEnumerable<TEntity> entities;
        if (request.Predicate == null)
            entities = _repository.DeleteByAsync(request.ForOnly(d => d.IsValid, d => d.Contract));
        else
            entities = _repository.DeleteByAsync(
                request.ForOnly(d => d.IsValid, d => d.Contract),
                request.Predicate
            );

        var response = entities.ForEachAsync(
            (e) =>
            {
                var r = request[e.Id];
                r.Result = e;
                return r;
            }
        );

        _ = _servicer.Publish(new DeletedSet<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);

        return response;
    }
}
