using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class CreateSetHandler<TStore, TEntity, TDto>
    : IRequestHandler<CreateSet<TStore, TEntity, TDto>, CommandSet<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public CreateSetHandler(IServicer uservice, IStoreRepository<TStore, TEntity> repository)
    {
        _servicer = uservice;
        _repository = repository;
    }

    public virtual async Task<CommandSet<TDto>> Handle(
        CreateSet<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TEntity> entities;
        if (request.Predicate == null)
            entities = _repository.AddBy(request.ForOnly(d => d.IsValid, d => d.Contract));
        else
            entities = _repository.AddBy(
                request.ForOnly(d => d.IsValid, d => d.Contract),
                request.Predicate
            );

        await entities.ForEachAsync(
            (e, x) =>
            {
                request[x].Result = e;
            }
        );

        _ = _servicer.Publish(new CreatedSet<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);

        return request;
    }
}
