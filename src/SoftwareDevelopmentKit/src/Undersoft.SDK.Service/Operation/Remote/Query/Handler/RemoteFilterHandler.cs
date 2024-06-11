using MediatR;
using Undersoft.SDK.Service.Data.Remote.Repository;

namespace Undersoft.SDK.Service.Operation.Remote.Query.Handler;

public class RemoteFilterHandler<TStore, TDto, TModel>
    : IRequestHandler<RemoteFilter<TStore, TDto, TModel>, RemoteQuery<TDto, TModel>>
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
    where TDto : class, IOrigin, IInnerProxy
{
    protected readonly IRemoteRepository<TDto> _repository;

    public RemoteFilterHandler(IRemoteRepository<TStore, TDto> repository)
    {
        _repository = repository;
    }

    public virtual async Task<RemoteQuery<TDto, TModel>> Handle(
        RemoteFilter<TStore, TDto, TModel> request,
        CancellationToken cancellationToken
    )
    {
        if (request.Parameters.Filter != null)
        {
            if (typeof(TModel) != typeof(TDto))
                request.Result = await _repository
                    .FilterQueryAsync<TModel>(
                        request.Offset,
                        request.Limit,
                        request.Parameters.Filter,
                        request.Parameters.Sort,
                        request.Parameters.Expanders
                    )
                    .ConfigureAwait(false);
            else
                request.Result =
                    (IQueryable<TModel>)
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
            if (typeof(TModel) != typeof(TDto))
                request.Result = await _repository
                    .GetQueryAsync<TModel>(
                        request.Offset,
                        request.Limit,
                        request.Parameters.Sort,
                        request.Parameters.Expanders
                    )
                    .ConfigureAwait(false);
            else
                request.Result =
                    (IQueryable<TModel>)
                        _repository[
                            request.Offset,
                            request.Limit,
                            _repository[request.Parameters.Sort, request.Parameters.Expanders]
                        ];
        }
        return request;
    }
}
