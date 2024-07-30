namespace Undersoft.SDK.Service.Operation.Query;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;

public class FilterDocument<TStore, TEntity, TDto> : Query<TEntity, TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{

    public FilterDocument() : base(OperationType.Get | OperationType.Query) { }

    public FilterDocument(
        int offset,
        int limit
    ) : base(OperationType.Get | OperationType.Query)
    {
        Offset = offset;
        Limit = limit;
    }

    public FilterDocument(
        int offset,
        int limit,
        IQueryParameters<TEntity> parameters
    ) : base(OperationType.Get | OperationType.Query, parameters)
    {
        Offset = offset;
        Limit = limit;
    }
}
