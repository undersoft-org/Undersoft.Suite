using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class UpsertSetAsyncHandler<TStore, TEntity, TDto>
    : IStreamRequestHandler<UpsertSetAsync<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public UpsertSetAsyncHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _servicer = servicer;
        _repository = repository;
    }

    public virtual IAsyncEnumerable<Command<TDto>> Handle(
        UpsertSetAsync<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IAsyncEnumerable<TEntity> entities;
        if (request.Conditions == null)
            entities = _repository.PutByAsync(
                request.ForOnly(d => d.IsValid, d => d.Contract),
                request.Predicate
            );
        else
            entities = _repository.PutByAsync(
                request.ForOnly(d => d.IsValid, d => d.Contract),
                request.Predicate,
                request.Conditions
            );

        var response = entities.ForEachAsync(
            (e) =>
            {
                var r = request[e.Id];
                r.Result = e;
                return r;
            }
        );

        _ = _servicer
            .Publish(new UpsertedSet<TStore, TEntity, TDto>(request), cancellationToken)
            .ConfigureAwait(false);

        return response;
    }
}
