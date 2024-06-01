using MediatR;

namespace Undersoft.SDK.Service.Operation.Remote.Invocation.Handler;

using Notification;
using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Invocation;
using Undersoft.SDK.Service.Operation.Remote.Invocation;

public class RemoteAcccessHandler<TStore, TService, TModel>
    : IRequestHandler<RemoteAccess<TStore, TService, TModel>, Invocation<TModel>>
    where TService : class
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    protected readonly IRemoteRepository<TModel> _repository;
    protected readonly IServicer _servicer;

    public RemoteAcccessHandler(IServicer servicer, IRemoteRepository<TStore, TModel> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<Invocation<TModel>> Handle(
        RemoteAccess<TStore, TService, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.Result.IsValid)
            return request;

        request.Response =
                 await _repository.Access(request.Arguments.MethodName, request.Arguments)

        ;

        if (request.Response == null)
            throw new Exception(
                $"{GetType().Name} "
                    + $"for entity {typeof(TModel).Name} "
                    + $"unable create source"
            );

        _ = _servicer
            .Publish(new RemoteAccessInvoked<TStore, TService, TModel>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
