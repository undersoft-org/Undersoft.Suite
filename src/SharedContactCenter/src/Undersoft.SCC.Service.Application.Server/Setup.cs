namespace Undersoft.SCC.Service.Application.Server;

using Undersoft.SCC.Service.Clients;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SCC.Service.Infrastructure.Stores;
using Undersoft.SDK.Service.Application.Server;
using Undersoft.SDK.Service.Application.Server.Hosting;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;

public class Setup
{
    public void ConfigureServices(IServiceCollection srvc)
    {
        srvc.AddApplicationServerSetup()
            .ConfigureApplicationServer(
                true,
                new[]
                {
                    typeof(EventStore),
                    typeof(ReportStore),
                    typeof(EntryStore)
                },
                new[]
                {
                    typeof(ServiceClient),
                    typeof(AccessClient)
                }
            )
            .AddDataServer<IEntityStore>(
                DataServerTypes.All,
                builder =>
                    builder
                        .AddInvocations<ViewModels.Contact>()
                        .AddInvocations<Country>()
                        .AddInvocations<ViewModels.Group>()
                        .AddInvocations<ViewModels.Contacts.ContactPersonal>()
                        .AddInvocations<ViewModels.Contacts.ContactProfessional>()
                        .AddInvocations<ViewModels.Contacts.ContactAddress>()
                        .AddInvocations<ViewModels.Contacts.ContactOrganization>()
            )
            .AddDataServer<IEventStore>(
                DataServerTypes.All,
                builder =>
                    builder
                        .AddInvocations<Event>()
            ).AddDataServer<IAccountStore>(
                DataServerTypes.All,
                builder =>
                    builder
                        .AddInvocations<Account>()
            );
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseApplicationServerSetup(env)
            .UseServiceApplication()
            .UseInternalProvider()
            .UseDataMigrations()
            .UseServiceClients();
    }
}
