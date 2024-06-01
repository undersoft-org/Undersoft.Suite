using Undersoft.SDK.Service.Data.Query;

namespace Undersoft.SDK.Service.Operation.Remote.Query;

public class RemoteFind<TStore, TDto, TModel> : RemoteQuery<TDto, TModel>
    where TModel : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
    where TDto : class, IOrigin, IInnerProxy
{
    public RemoteFind(params object[] keys)
        : base(OperationType.Find | OperationType.Query | OperationType.Remote, keys) { }

    public RemoteFind(IQueryParameters<TDto> parameters)
        : base(OperationType.Find | OperationType.Query | OperationType.Remote, parameters) { }

    public RemoteFind() : base(OperationType.Find | OperationType.Query | OperationType.Remote) { }
}
