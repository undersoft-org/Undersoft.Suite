using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Notification.Handler;
using Series;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command.Notification;

public class ChangedSetHandler<TStore, TEntity, TDto>
    : INotificationHandler<ChangedSet<TStore, TEntity, TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IStoreRepository<Event> _eventStore;

    public ChangedSetHandler() { }

    public ChangedSetHandler(
        IStoreRepository<IReportStore, TEntity> repository,
        IStoreRepository<IEventStore, Event> eventStore
    )
    {
        _repository = repository;
        _eventStore = eventStore;
    }

    public virtual async Task Handle(
        ChangedSet<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        await Task.Run(
            () =>
            {
                request.ForOnly(
                    d => !d.Command.IsValid,
                    d =>
                    {
                        request.Remove(d);
                    }
                );

                _eventStore.AddAsync(request).ConfigureAwait(true);

                if (request.PublishMode == EventPublishMode.PropagateCommand)
                {
                    ISeries<TEntity> entities;
                    if (request.Predicate == null)
                        entities = _repository
                            .PatchBy(request.Select(d => d.Command.Contract).ToArray())
                            .ToCatalog();
                    else
                        entities = _repository
                            .PatchBy(
                                request.Select(d => d.Command.Contract).ToArray(),
                                request.Predicate
                            )
                            .ToCatalog();

                    request.ForEach(
                        (r) =>
                        {
                            _ = entities.ContainsKey(r.EntityId)
                                ? r.PublishStatus = EventPublishStatus.Complete
                                : r.PublishStatus = EventPublishStatus.Uncomplete;
                        }
                    );
                }
            },
            cancellationToken
        );
    }
}
