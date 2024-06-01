using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Server;

using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Data.Store.Repository;

public partial class ServerSetup
{
    public IServiceSetup AddDataStoreImplementations()
    {
        IServiceRegistry service = registry;

        HashSet<Type> duplicateCheck = new HashSet<Type>();
        Type[] stores = DataStoreRegistry.Stores.Where(s => s.IsAssignableTo(typeof(IDataServerStore))).ToArray();

        service.AddScoped<IRemoteSynchronizer, RemoteSynchronizer>();

        foreach (ISeries<IEntityType> contextEntityTypes in DataStoreRegistry.EntityTypes)
        {
            foreach (IEntityType _entityType in contextEntityTypes)
            {
                Type entityType = _entityType.ClrType;
                if (duplicateCheck.Add(entityType))
                {
                    foreach (Type store in stores)
                    {
                        service.AddScoped(
                            typeof(IStoreRepository<,>).MakeGenericType(store, entityType),
                            typeof(StoreRepository<,>).MakeGenericType(store, entityType));

                        service.AddSingleton(
                            typeof(IEntityCache<,>).MakeGenericType(store, entityType),
                            typeof(EntityCache<,>).MakeGenericType(store, entityType));
                    }
                }
            }
        }
        return this;
    }
}