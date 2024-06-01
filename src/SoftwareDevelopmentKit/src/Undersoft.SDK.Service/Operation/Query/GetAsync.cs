using MediatR;

namespace Undersoft.SDK.Service.Operation.Query;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;

public class GetAsync<TStore, TEntity, TDto>
    : Get<TStore, TEntity, TDto>,
        IStreamRequest<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{
    public GetAsync() : base() { }

    public GetAsync(
        int offset,
        int limit,
        IQueryParameters<TEntity> parameters
    ) : base(offset, limit, parameters) { }

    public GetAsync(
        int offset,
        int limit
    ) : base(offset, limit) { }
}
