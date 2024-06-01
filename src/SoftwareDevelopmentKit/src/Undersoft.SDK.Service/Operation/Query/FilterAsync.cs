using MediatR;

namespace Undersoft.SDK.Service.Operation.Query;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;

public class FilterAsync<TStore, TEntity, TDto>
    : Filter<TStore, TEntity, TDto>,
        IStreamRequest<TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{
    public FilterAsync(int offset, int limit, IQueryParameters<TEntity> parameters)
      : base(parameters)
    {
        Offset = offset;
        Limit = limit;
    }

    public FilterAsync() : base() { }

    public FilterAsync(
        int offset,
        int limit) : base()
    {
        Offset = offset;
        Limit = limit;
    }
}
