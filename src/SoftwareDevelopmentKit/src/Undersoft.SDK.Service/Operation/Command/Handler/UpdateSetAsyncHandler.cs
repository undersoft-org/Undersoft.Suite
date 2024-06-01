using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class UpdateSetAsyncHandler<TStore, TEntity, TDto>
    : IStreamRequestHandler<UpdateSetAsync<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public UpdateSetAsyncHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public IAsyncEnumerable<Command<TDto>> Handle(
        UpdateSetAsync<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        IAsyncEnumerable<TEntity> entities;
        if (request.Predicate == null)
            entities = _repository.SetByAsync(
                request.ForOnly(d => d.IsValid, d => d.Contract).Commit()
            );
        else if (request.Conditions == null)
            entities = _repository.SetByAsync(
                request.ForOnly(d => d.IsValid, d => d.Contract).Commit(),
                request.Predicate
            );
        else
            entities = _repository.SetByAsync(
                request.ForOnly(d => d.IsValid, d => d.Contract).Commit(),
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

        _ = _servicer.Publish(new UpdatedSet<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);

        return response;
    }
}
