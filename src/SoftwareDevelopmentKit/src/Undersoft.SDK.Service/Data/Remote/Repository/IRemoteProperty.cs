using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Undersoft.SDK.Service.Data.Remote.Repository;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Remote;
using Undersoft.SDK.Service.Data.Repository;
using Undersoft.SDK.Service.Data.Store;

public interface IRemoteProperty<TStore, TOrigin> : IRemoteProperty<TOrigin> where TOrigin : class, IOrigin, IInnerProxy where TStore : IDataServiceStore
{
}

public interface IRemoteProperty<TOrigin> : IRemoteProperty where TOrigin : class, IOrigin, IInnerProxy
{
}

public interface IRemoteProperty : IRepository, IRemoteRelation
{
    bool IsLinked { get; set; }

    IRepository Host { get; set; }

    void Load(object origin);

    Task LoadAsync(object origin);

    void LoadRemotesEvent(object sender, EntityEntryEventArgs e);

    IRemoteSynchronizer Synchronizer { get; }
}
