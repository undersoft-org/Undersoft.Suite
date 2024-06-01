using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class UpsertSetHandler<TStore, TEntity, TDto>
    : IRequestHandler<UpsertSet<TStore, TEntity, TDto>, CommandSet<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _uservice;

    public UpsertSetHandler(IServicer uservice, IStoreRepository<TStore, TEntity> repository)
    {
        _uservice = uservice;
        _repository = repository;
    }

    public virtual async Task<CommandSet<TDto>> Handle(
        UpsertSet<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TEntity> entities;
        if (request.Conditions == null)
            entities = _repository.PutBy(
                request.ForOnly(d => d.IsValid, d => d.Contract),
                request.Predicate
            );
        else
            entities = _repository.PutBy(
                request.ForOnly(d => d.IsValid, d => d.Contract),
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

        _ = _uservice
            .Publish(new UpsertedSet<TStore, TEntity, TDto>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
