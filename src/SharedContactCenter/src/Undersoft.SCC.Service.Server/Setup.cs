using Undersoft.SCC.Service.Clients;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Infrastructure.Stores;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SCC.Service.Server;

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
                new[] { typeof(ApplicationClient) }
            )
            .AddAccessServer<AccountStore, Account>()
            .AddDataServer<IEntityStore>(
                DataServerTypes.Rest | DataServerTypes.OData,
                builder =>
                    builder
                        .AddInvocations<Contact>()
                        .AddInvocations<Country>()
                        .AddInvocations<Group>()
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
            .UseServiceClients();
    }
}
