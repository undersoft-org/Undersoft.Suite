using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Factories
{
    public class EventStoreFactory : DbStoreFactory<EventStore, ServerSourceProviderConfiguration>
    {
    }
}
