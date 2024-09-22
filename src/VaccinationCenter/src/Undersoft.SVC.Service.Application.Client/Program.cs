using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   application: Undersoft.SVC.Service.Application.Client
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Client;

using Undersoft.SDK.Service.Application.GUI;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SVC.Service.Application.GUI.Compound.Access;
using Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu.Validators;
using Undersoft.SVC.Service.Application.ViewModels.Catalogs;
using Undersoft.SVC.Service.Application.ViewModels.Inventory;
using Undersoft.SVC.Service.Application.ViewModels.Vaccination;
using Undersoft.SVC.Service.Clients;
using Undersoft.SVC.Service.Contracts;

/// <summary>
/// The program.
/// </summary>
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var manager = builder
            .Services.AddServiceSetup(builder.Configuration)
            .ConfigureServices(
                new[] { typeof(ApplicationClient), typeof(AccessClient), typeof(EventClient) }
            )
            .Manager;

        await manager.UseServiceClients();

        builder.ConfigureContainer(
            manager.GetProviderFactory(),
            (services) =>
            {
                var reg = manager.GetRegistry();
                reg.AddAuthorizationCore()
                    .AddFluentUIComponents((o) => o.UseTooltipServiceProvider = true)
                    .AddViewServices()
                    .AddScoped<
                        IRemoteRepository<IAccountStore, Account>,
                        RemoteRepository<IAccountStore, Account>
                    >()
                    .AddScoped<
                        IRemoteRepository<IEventStore, EventInfo>,
                        RemoteRepository<IEventStore, EventInfo>
                    >()
                    .AddSingleton<AppearanceState>()
                    .AddScoped<AccessProvider<Account>>()
                    .AddScoped<AuthenticationStateProvider, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccess, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccessContext, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccessService<Account>, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                      .AddScoped<IAccessProvider<Account>, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IValidator<IViewData<Credentials>>, AccessValidator>()
                    .AddScoped<IValidator<IViewData<Account>>, AccountValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Event>>, EventValidator>()
                    .AddScoped<
                        IValidator<IViewData<Appointment>>,
                        AppointmentValidator
                    >()
                    .AddScoped<IValidator<IViewData<Office>>, OfficeValidator>()
                    .AddScoped<IValidator<IViewData<Vaccine>>, VaccineValidator>()
                    .AddScoped<IValidator<IViewData<Stock>>, StockValidator>()
                    .AddScoped<IValidator<IViewData<Procedure>>, ProcedureValidator>()
                    .AddScoped<
                        IValidator<IViewData<Certificate>>,
                        CertificateValidator
                    >()
                    .AddScoped<
                        IValidator<IViewData<PostSymptom>>,
                        PostSymptomValidator
                    >()
                    .AddScoped<IValidator<IViewData<Request>>, RequestValidator>()
                    .AddScoped<IValidator<IViewData<Traffic>>, TrafficValidator>()
                    .AddScoped<IValidator<IViewData<Campaign>>, CampaignValidator>()
                    .AddScoped<IValidator<IViewData<Supplier>>, SupplierValidator>()
                    .AddScoped<
                        IValidator<IViewData<Manufacturer>>,
                        ManufacturerValidator
                    >();
                reg.MergeServices(services, true);
            }
        );

        var host = builder.Build();
        await host.RunAsync();
    }
}
