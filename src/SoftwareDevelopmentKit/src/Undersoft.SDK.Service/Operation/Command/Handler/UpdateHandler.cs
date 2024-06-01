using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class UpdateHandler<TStore, TEntity, TDto>
    : IRequestHandler<Update<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public UpdateHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<Command<TDto>> Handle(
        Update<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Keys != null)
            request.Result = await _repository.SetBy(request.Contract, request.Keys);
        else if (request.Conditions == null)
            request.Result = await _repository.SetBy(request.Contract, request.Predicate);
        else
            request.Result = await _repository.SetBy(
                request.Contract,
                request.Predicate,
                request.Conditions
            );

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} for entity " + $"{typeof(TEntity).Name} unable update source"
            );

        _ = _servicer.Publish(new Updated<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);

        return request;
    }
}
