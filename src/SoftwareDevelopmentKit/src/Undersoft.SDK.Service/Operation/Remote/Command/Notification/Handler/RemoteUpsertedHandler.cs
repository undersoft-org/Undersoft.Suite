using FluentValidation.Results;
using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Notification.Handler;
using Logging;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Remote.Command.Notification;

public class RemoteUpsertedHandler<TStore, TDto, TModel>
    : INotificationHandler<RemoteUpserted<TStore, TDto, TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IStoreRepository<TDto> _repository;
    protected readonly IStoreRepository<Event> _eventStore;

    public RemoteUpsertedHandler() { }

    public RemoteUpsertedHandler(
        IStoreRepository<IReportStore, TDto> repository,
        IStoreRepository<IEventStore, Event> eventStore
    )
    {
        _repository = repository;
        _eventStore = eventStore;
    }

    public virtual Task Handle(
        RemoteUpserted<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        return Task.Run(
            () =>
            {
                try
                {
                    if (_eventStore.Add(request) == null)
                        throw new Exception(
                            $"{GetType().Name} "
                                + $"for entity {typeof(TDto).Name} unable add event"
                        );

                }
                catch (Exception ex)
                {
                    request.Command.ValidationResult.Errors.Add(
                        new ValidationFailure(string.Empty, ex.Message)
                    );
                    this.Failure<Domainlog>(ex.Message, request.Command.ErrorMessages, ex);
                    request.PublishStatus = EventPublishStatus.Error;
                }
            },
            cancellationToken
        );
    }
}
