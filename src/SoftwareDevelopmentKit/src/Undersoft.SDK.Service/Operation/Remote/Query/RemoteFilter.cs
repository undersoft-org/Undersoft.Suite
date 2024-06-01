using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Operation.Remote.Query;

public class RemoteFilter<TStore, TDto, TModel> : RemoteQuery<TDto, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
    where TDto : class, IOrigin, IInnerProxy
{
    public RemoteFilter(int offset, int limit, IQueryParameters<TDto> parameters)
        : base(OperationType.Filter | OperationType.Query | OperationType.Remote, parameters)
    {
        Offset = offset;
        Limit = limit;
    }

    public RemoteFilter(IQueryParameters<TDto> parameters)
        : base(OperationType.Filter | OperationType.Query | OperationType.Remote, parameters) { }

    public RemoteFilter() : base(OperationType.Filter | OperationType.Query | OperationType.Remote)
    { }

    public RemoteFilter(int offset, int limit)
        : base(OperationType.Filter | OperationType.Query | OperationType.Remote)
    {
        Offset = offset;
        Limit = limit;
    }
}
