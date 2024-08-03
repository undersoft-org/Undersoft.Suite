namespace Undersoft.GDC.Service.Application.Server;

using Undersoft.GDC.Service.Clients;
using Undersoft.GDC.Service.Contracts;
using Undersoft.GDC.Service.Infrastructure.Stores;
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
                        .AddInvocations<Member>()
                        .AddInvocations<Service>()
                        .AddInvocations<Schedule>()
                        .AddInvocations<Activity>()
                        .AddInvocations<Resource>()
                        .AddInvocations<ServiceMember>()
                        .AddInvocations<ServiceActivity>()
                        .AddInvocations<ServiceResource>()
                        .AddInvocations<ServiceSchedule>()
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
            .UseServiceClients(30);
    }
}
