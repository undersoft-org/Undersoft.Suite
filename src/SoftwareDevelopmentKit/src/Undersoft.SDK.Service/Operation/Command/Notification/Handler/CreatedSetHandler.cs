using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Notification.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command.Notification;

public class CreatedSetHandler<TStore, TEntity, TDto>
    : INotificationHandler<CreatedSet<TStore, TEntity, TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IStoreRepository<Event> _eventStore;

    public CreatedSetHandler() { }

    public CreatedSetHandler(
        IStoreRepository<IReportStore, TEntity> repository,
        IStoreRepository<IEventStore, Event> eventStore
    )
    {
        _repository = repository;
        _eventStore = eventStore;
    }

    public virtual async Task Handle(
        CreatedSet<TStore, TEntity, TDto> request,
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

                _eventStore.Add(request.ForEach(r => r.GetEvent())).Commit();

                if (request.PublishMode == EventPublishMode.PropagateCommand)
                {
                    var entities = _repository
                        .Add(
                            request.Select(d => d.Command.Result).Cast<TEntity>(),
                            request.Predicate
                        )
                        .ToListing();

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
