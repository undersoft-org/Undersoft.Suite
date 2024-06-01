using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class UpsertHandler<TStore, TEntity, TDto>
    : IRequestHandler<Upsert<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _umaker;

    public UpsertHandler(IServicer umaker, IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
        _umaker = umaker;
    }

    public async Task<Command<TDto>> Handle(
        Upsert<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Conditions != null)
            request.Result = await _repository.PutBy(
                request.Contract,
                request.Predicate,
                request.Conditions
            );
        else
            request.Result = await _repository.PutBy(request.Contract, request.Predicate);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} " + $"for entity {typeof(TEntity).Name} unable renew source"
            );

        _ = _umaker.Publish(new Upserted<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);
        ;

        return request;
    }
}
