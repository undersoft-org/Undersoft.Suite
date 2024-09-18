using Undersoft.GCC.Infrastructure.Stores;
using Undersoft.GCC.Service.API.Extensions;
using Undersoft.GCC.Service.Contracts;
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
            .ConfigureServer(true, [typeof(EventStore), typeof(EntryStore), typeof(ReportStore)])
            .AddDataServer<IEntityStore>(
                DataServerTypes.All,
                builder =>
                    builder
                        .AddInvocations<Currency>()
                        .AddInvocations<CurrencyProvider>()
                        .AddInvocations<CurrencyRate>()
                        .AddInvocations<CurrencyRateTable>()
            )
            .AddDataServer<IEventStore>(
                DataServerTypes.All,
                builder => builder.AddInvocations<Event>()
            )
            .AddCurrencyContexts()
            .AddCurrencyWorkflows();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseServerSetup(env)
            .UseServiceServer(["v1"])
            .UseInternalProvider()
            .UseDataMigrations()
            .UseCurrenciesFeed()
            .UseServiceClients();
    }
}
