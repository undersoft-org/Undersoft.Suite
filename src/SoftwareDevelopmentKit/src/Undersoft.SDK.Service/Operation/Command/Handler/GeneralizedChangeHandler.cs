﻿using MediatR;

namespace Undersoft.SDK.Service.Operation.Command.Handler;

using Notification;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;
using Undersoft.SDK.Service.Operation.Command;

public class GeneralizedChangeHandler<TStore, TEntity, TDto>
    : IRequestHandler<GeneralizedChange<TStore, TEntity, TDto>, Command<TDto>>
    where TDto : class, IOrigin, IInnerProxy
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
{
    protected readonly IStoreRepository<TEntity> _repository;
    protected readonly IServicer _servicer;

    public GeneralizedChangeHandler(IServicer servicer, IStoreRepository<TStore, TEntity> repository)
    {
        _servicer = servicer;
        _repository = repository;
    }

    public virtual async Task<Command<TDto>> Handle(
        GeneralizedChange<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidationResult.IsValid)
            return request;

        if (request.Keys != null)
            request.Result = await _repository.GeneralizedPatchBy(request.Contract, request.Keys);
        else
            request.Result = await _repository.GeneralizedPatchBy(request.Contract, request.Predicate);

        if (request.Result == null)
            throw new Exception(
                $"{GetType().Name} for entity " + $"{typeof(TEntity).Name} unable patch source"
            );

        _ = _servicer
            .Publish(new GeneralizedChanged<TStore, TEntity, TDto>(request), cancellationToken)
            .ConfigureAwait(false);

        return request;
    }
}
