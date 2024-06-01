namespace Undersoft.SDK.Service.Operation.Query;

using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Store;

public class Find<TStore, TEntity, TDto> : Query<TEntity, TDto>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServerStore
    where TDto : class, IOrigin, IInnerProxy
{
    public Find(params object[] keys) : base(OperationType.Find | OperationType.Query, keys) { }

    public Find(IQueryParameters<TEntity> parameters) : base(OperationType.Find | OperationType.Query, parameters) { }

    public Find() : base(OperationType.Find | OperationType.Query) { }
}
