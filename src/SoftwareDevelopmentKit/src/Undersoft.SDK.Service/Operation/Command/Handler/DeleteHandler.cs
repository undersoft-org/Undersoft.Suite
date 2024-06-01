using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;
using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class DeleteHandler<TStore, TEntity, TDto>
    : IRequestHandler<Delete<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _umaker;

    public DeleteHandler(IServicer umaker, IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
        _umaker = umaker;
    }

    public async Task<Command<TDto>> Handle(
        Delete<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Keys != null)
            request.Result = await _repository.Delete(request.Keys);
        else if (request.Contract != null)
        {
            if (request.Predicate == null)
                request.Result = await _repository.DeleteBy(request.Contract);
            else
                request.Result = await _repository.DeleteBy(request.Contract, request.Predicate);
        }
        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} for entity" + $" {typeof(TEntity).Name} unable delete source"
            );

        _ = _umaker.Publish(new Deleted<TStore, TEntity, TDto>(request), cancellationToken).ConfigureAwait(false);
        ;

        return request;
    }
}
