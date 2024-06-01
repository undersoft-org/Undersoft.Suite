using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Remote.Command;
using Undersoft.SDK.Service.Operation.Remote.Command.Notification;

public class RemoteUpsertSetHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteUpsertSet<TStore, TDto, TModel>, RemoteCommandSet<TModel>>
    where TDto : class, IOrigin, IInnerProxy
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TDto> _repository;
    protected readonly IServicer _uservice;

    public RemoteUpsertSetHandler(IServicer uservice, IRemoteRepository<TStore, TDto> repository)
    {
        _uservice = uservice;
        _repository = repository;
    }

    public virtual async Task<RemoteCommandSet<TModel>> Handle(
        RemoteUpsertSet<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        IEnumerable<TDto> entities;
        if (request.Conditions == null)
            entities = _repository.PutBy(
                request.ForOnly(d => d.IsValid, d => d.Model),
                request.Predicate
            );
        else
            entities = _repository.PutBy(
                request.ForOnly(d => d.IsValid, d => d.Model),
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
            .Publish(new RemoteUpsertedSet<TStore, TDto, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
