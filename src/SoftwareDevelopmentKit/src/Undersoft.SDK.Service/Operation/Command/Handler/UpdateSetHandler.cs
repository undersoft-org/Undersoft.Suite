using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class UpdateSetHandler<TStore, TEntity, TDto>
    : IRequestHandler<UpdateSet<TStore, TEntity, TDto>, CommandSet<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public UpdateSetHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<CommandSet<TDto>> Handle(
        UpdateSet<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TEntity> entities = null;
        if (request.Predicate == null)
            entities = _repository.SetBy(request.ForOnly(d => d.IsValid, d => d.Contract).Commit());
        else if (request.Conditions == null)
            entities = _repository.SetBy(
                request.ForOnly(d => d.IsValid, d => d.Contract).Commit(),
                request.Predicate
            );
        else
            entities = _repository.SetBy(
                request.ForOnly(d => d.IsValid, d => d.Contract).Commit(),
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

        _ = _servicer.Publish(new UpdatedSet<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);

        return request;
    }
}
