using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Infrastructure.DataStores.Factories
{
    public class EventStoreFactory : DbStoreContextFactory<EventStore, ServiceSourceProviderConfiguration>
    {
    }
}
