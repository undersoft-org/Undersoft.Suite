using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class CreateHandler<TStore, TEntity, TDto>
    : IRequestHandler<Create<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public CreateHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
        _servicer = servicer;
    }

    public async Task<Command<TDto>> Handle(
        Create<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        request.Result = await _repository.AddByAsync(request.Contract, request.Predicate);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} "
                    + $"for entity {typeof(TEntity).Name} "
                    + $"unable create source"
            );

        _ = _servicer.Publish(new Created<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);
        ;

        return request;
    }
}
