using MediatR;

namespace Undersoft.SDK.Service.Operation.Invocation.Notification.Handler;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Invocation.Notification;

public class SetupInvokedHandler<TStore, TType, TDto>
    : INotificationHandler<SetupInvoked<TStore, TType, TDto>>
    where TType : class
    where TDto : class, IOrigin
    where TStore : IDataStore
{
    protected readonly IStoreRepository<Event> _eventStore;

    public SetupInvokedHandler() { }

    public SetupInvokedHandler(IStoreRepository<IEventStore, Event> eventStore)
    {
        _eventStore = eventStore;
    }

    public virtual Task Handle(
        SetupInvoked<TStore, TType, TDto> request,
        CancellationToken cancellationToken
    )
    {
        return Task.Run(
            () =>
            {
                if (_eventStore.Add(request) == null)
                    throw new Exception(
                        $"{$"{GetType().Name} "}{$"for invoke {typeof(TType).Name} unable add event"}"
                    );
            },
            cancellationToken
        );
    }
}
