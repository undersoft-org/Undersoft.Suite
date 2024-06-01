using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class DeleteSetHandler<TStore, TEntity, TDto>
    : IRequestHandler<DeleteSet<TStore, TEntity, TDto>, CommandSet<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public DeleteSetHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _servicer = servicer;
        _repository = repository;
    }

    public virtual async Task<CommandSet<TDto>> Handle(
        DeleteSet<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TEntity> entities;
        if (request.Predicate == null)
            entities = _repository
                .DeleteBy(request.ForOnly(d => d.IsValid, d => d.Contract))
                .Commit();
        else
            entities = _repository
                .DeleteBy(request.ForOnly(d => d.IsValid, d => d.Contract), request.Predicate)
                .Commit();

        await entities.ForEachAsync(
            (e) =>
            {
                request[e.Id].Result = e;
            }
        );

        _ = _servicer.Publish(new DeletedSet<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);

        return request;
    }
}
