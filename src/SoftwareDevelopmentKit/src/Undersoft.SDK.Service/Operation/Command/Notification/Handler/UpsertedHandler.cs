using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Notification.Handler;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command.Notification;

public class UpsertedHandler<TStore, TEntity, TDto>
    : INotificationHandler<Upserted<TStore, TEntity, TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IStoreRepository<Event> _eventStore;

    public UpsertedHandler() { }

    public UpsertedHandler(
        IStoreRepository<IReportStore, TEntity> repository,
        IStoreRepository<IEventStore, Event> eventStore
    )
    {
        _repository = repository;
        _eventStore = eventStore;
    }

    public virtual Task Handle(
        Upserted<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        return Task.Run(
            async () =>
            {
                if (_eventStore.Add(request) == null)
                    throw new Exception(
                        $"{GetType().Name} "
                            + $"for entity {typeof(TEntity).Name} unable add event"
                    );

                if (request.Command.PublishMode == EventPublishMode.PropagateCommand)
                {
                    TEntity result = null;
                    if (request.Conditions != null)
                        result = await _repository.PutBy(
                            request.Command.Contract,
                            request.Predicate,
                            request.Conditions
                        );
                    else
                        result = await _repository.PutBy(
                            request.Command.Contract,
                            request.Predicate
                        );

                    if (result == null)
                        throw new Exception(
                            $"{GetType().Name} "
                                + $"for entity {typeof(TEntity).Name} unable renew report"
                        );

                    request.PublishStatus = EventPublishStatus.Complete;
                }
            },
            cancellationToken
        );
    }
}
