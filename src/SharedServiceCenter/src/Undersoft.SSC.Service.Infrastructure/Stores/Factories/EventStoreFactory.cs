﻿using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SSC.Service.Infrastructure.Stores.Factories
{
    public class EventStoreFactory : DbStoreContextFactory<EventStore, ServerSourceProviderConfiguration>
    {
    }
}