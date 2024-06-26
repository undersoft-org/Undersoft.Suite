﻿using Undersoft.GCC.Infrastructure.DataStores;
using Undersoft.GCC.Service.API.Extensions;
using Undersoft.GCC.Service.Extensions;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.GCC.Service.API;

public class Setup
{
    public void ConfigureServices(IServiceCollection srvc)
    {
        srvc.AddServerSetup()
            .ConfigureServer(
                true,
                new[] { typeof(EventStore), typeof(EntryStore), typeof(ReportStore) }
            )
            .AddDataServer<IEntityStore>(DataServerTypes.Rest | DataServerTypes.OData)
            .AddDataServer<IEventStore>(DataServerTypes.OData, builder => builder.AddInvocations<Event>())
            .AddCurrencyContexts()
            .AddCurrencyWorkflows();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseServerSetup(env)
            .UseServiceServer(new string[] { "v1" })
            .UseInternalProvider()
            .UseDataMigrations()
            .UseCurrenciesFeed()
            .UseServiceClients();
    }
}
