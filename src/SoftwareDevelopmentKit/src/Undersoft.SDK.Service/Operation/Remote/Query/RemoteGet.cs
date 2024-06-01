using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Operation.Remote.Query;

public class RemoteGet<TStore, TDto, TModel> : RemoteQuery<TDto, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
    where TDto : class, IOrigin, IInnerProxy
{
    public RemoteGet() : base(OperationType.Get | OperationType.Query | OperationType.Remote) { }

    public RemoteGet(int offset, int limit)
        : base(OperationType.Get | OperationType.Query | OperationType.Remote)
    {
        Offset = offset;
        Limit = limit;
    }

    public RemoteGet(int offset, int limit, IQueryParameters<TDto> parameters)
        : base(OperationType.Get | OperationType.Query | OperationType.Remote, parameters)
    {
        Offset = offset;
        Limit = limit;
    }
}
