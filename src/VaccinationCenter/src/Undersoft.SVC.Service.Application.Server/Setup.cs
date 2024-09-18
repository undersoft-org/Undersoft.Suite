// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Application.Server
// ********************************************************

using Undersoft.SDK.Service.Application.Server;
using Undersoft.SDK.Service.Application.Server.Hosting;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SVC.Service.Application.Server;

using Undersoft.SVC.Service.Clients;
using Undersoft.SVC.Service.Clients.Abstractions;
using Undersoft.SVC.Service.Contracts;
using Undersoft.SVC.Service.Contracts.Accounts;
using Undersoft.SVC.Service.Contracts.Catalogs;
using Undersoft.SVC.Service.Contracts.Inventory;
using Undersoft.SVC.Service.Contracts.Vaccination;
using Undersoft.SVC.Service.Infrastructure.Stores;

/// <summary>
/// The setup.
/// </summary>
public class Setup
{
    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="srvc">The srvc.</param>
    public void ConfigureServices(IServiceCollection srvc)
    {
        srvc.AddApplicationServerSetup()
            .ConfigureApplicationServer(
                true,
                new[] { typeof(EventStore), typeof(ReportStore), typeof(EntryStore) },
                new[]
                {
                    typeof(AccessClient),
                    typeof(CatalogsClient),
                    typeof(InventoryClient),
                    typeof(VaccinationClient)
                }
            )
            .AddDataServer<ICenterStore>(
                DataServerTypes.All,
                builder =>
                    builder
                        .AddInvocations<Appointment>()
                        .AddInvocations<Campaign>()
                        .AddInvocations<Certificate>()
                        .AddInvocations<Manufacturer>()
                        .AddInvocations<Office>()
                        .AddInvocations<PostSymptom>()
                        .AddInvocations<Procedure>()
                        .AddInvocations<Request>()
                        .AddInvocations<Stock>()
                        .AddInvocations<Traffic>()
                        .AddInvocations<Vaccine>()
                        .AddInvocations<Supplier>()
                        .AddInvocations<Price>()
            )
            .AddDataServer<IEventStore>(
                DataServerTypes.All,
                builder => builder.AddInvocations<EventInfo>()
            )
            .AddDataServer<IAccountStore>(
                DataServerTypes.All,
                builder => builder.AddInvocations<Account>()
                                  .AddInvocations<AccountAddress>()
                                  .AddInvocations<AccountPersonal>()
                                  .AddInvocations<AccountProfessional>()
                                  .AddInvocations<AccountOrganization>()
                                  .AddInvocations<AccountSubscription>()
                                  .AddInvocations<AccountConsent>()
                                  .AddInvocations<AccountTenant>()
                                  .AddInvocations<AccountPayment>()
            );
    }

    /// <summary>
    /// Configures the specified application.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="env">The env.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseApplicationServerSetup(env)
            .UseServiceApplication()
            .UseInternalProvider()
            .UseDataMigrations()
            .UseServiceClients();
    }
}
