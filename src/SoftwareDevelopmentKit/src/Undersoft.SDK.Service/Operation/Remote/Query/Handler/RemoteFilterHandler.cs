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
        request.Result = await _repository
            .FilterNoTrackedQueryAsync<TModel>(
                request.Offset,
                request.Limit,
                request.Parameters.Filter,
                request.Parameters.Sort,
                request.Parameters.Expanders
            )
            .ConfigureAwait(false);

        return request;
    }
}
