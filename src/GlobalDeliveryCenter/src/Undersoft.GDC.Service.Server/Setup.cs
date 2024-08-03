using Undersoft.GDC.Service.Clients;
using Undersoft.GDC.Service.Contracts;
using Undersoft.GDC.Service.Infrastructure.Stores;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.GDC.Service.Server;

public class Setup
{
    public void ConfigureServices(IServiceCollection srvc)
    {
        srvc.AddServerSetup()
            .ConfigureServer(
                true,
                new[]
                {
                    typeof(AccountStore),
                    typeof(EventStore),
                    typeof(EntryStore),
                    typeof(ReportStore)
                },
                new[] { typeof(ApplicationClient),
                        typeof(EventClient) }
            )
            .AddAccessServer<AccountStore, Account>()
            .AddDataServer<IEntityStore>(
                DataServerTypes.All,
                builder =>
                    builder
                        .AddInvocations<ServiceActivity>()
                        .AddInvocations<ServiceResource>()
                        .AddInvocations<ServiceSchedule>()
                        .AddInvocations<Contracts.Service>()
                        .AddInvocations<ServiceMember>()
            )
            .AddDataServer<IEventStore>(
                DataServerTypes.All,
                builder => builder.AddInvocations<Event>()
            )
            .AddDataServer<IAccountStore>(
                DataServerTypes.All,
                builder => builder.AddInvocations<Account>()
            );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseServerSetup(env)
            .UseServiceServer(new string[] { "v1" })
            .UseInternalProvider()
            .UseDataMigrations()
            .UseServiceClients(30);
    }
}
