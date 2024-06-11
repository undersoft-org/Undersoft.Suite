﻿using MediatR;

namespace Undersoft.SDK.Service.Operation.Query.Handler;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;

public class FilterHandler<TStore, TEntity, TDto>
    : IRequestHandler<Filter<TStore, TEntity, TDto>, Query<TEntity, TDto>>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{
    protected readonly IStoreRepository<TEntity> _repository;

    public FilterHandler(IStoreRepository<TStore, TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<Query<TEntity, TDto>> Handle(
        Filter<TStore, TEntity, TDto> request,
        CancellationToken cancellationToken
    )
    {
        if (request.Parameters.Filter != null)
        {
            if (typeof(TEntity) != typeof(TDto))
                request.Result = await _repository
                    .FilterQueryAsync<TDto>(
                        request.Offset,
                        request.Limit,
                        request.Parameters.Filter,
                        request.Parameters.Sort,
                        request.Parameters.Expanders
                    )
                    .ConfigureAwait(false);
            else
                request.Result =
                    (IQueryable<TDto>)
                        _repository[
                            request.Offset,
                            request.Limit,
                            _repository[
                                request.Parameters.Filter,
                                request.Parameters.Sort,
                                request.Parameters.Expanders
                            ]
                        ];
        }
        else
        {
            if (typeof(TEntity) != typeof(TDto))
                request.Result = await _repository
                    .GetQueryAsync<TDto>(
                        request.Offset,
                        request.Limit,
                        request.Parameters.Sort,
                        request.Parameters.Expanders
                    )
                    .ConfigureAwait(false);
            else
                request.Result =
                    (IQueryable<TDto>)
                        _repository[
                            request.Offset,
                            request.Limit,
                            _repository[request.Parameters.Sort, request.Parameters.Expanders]
                        ];
        }
        return request;
    }
}
