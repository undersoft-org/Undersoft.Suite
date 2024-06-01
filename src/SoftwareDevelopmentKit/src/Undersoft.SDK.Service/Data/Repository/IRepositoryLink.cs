namespace Undersoft.SDK.Service.Data.Repository;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

public interface IRepositoryLink<TStore, TOrigin, TTarget> : IRemoteRepository<TStore, TTarget>,
                 IRemoteRelation<TOrigin, TTarget>, IRemoteProperty<TStore, TOrigin>
                 where TOrigin : class, IOrigin, IInnerProxy where TTarget : class, IOrigin, IInnerProxy where TStore : IDataServiceStore
{
}
