using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Reflection;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   application: Undersoft.SVC.Service.Application.Hybrid
// ********************************************************

using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Application.GUI;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Hybrid;

using Undersoft.SVC.Service.Application.GUI.Compound.Access;
using Undersoft.SVC.Service.Application.GUI.Compound.Presenting.NavMenu.Validators;
using Undersoft.SVC.Service.Clients;
using Undersoft.SVC.Service.Contracts;
using Undersoft.SVC.Service.Contracts.Catalogs;
using Undersoft.SVC.Service.Contracts.Inventory;
using Undersoft.SVC.Service.Contracts.Vaccination;
using EventInfo = SDK.Service.Data.Event.EventInfo;

/// <summary>
/// The maui program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// There is not any async MAUI entry point type to run application. That's
    /// why client started in the same time with servers will throw exception
    /// ones or twice to logs http client cannot connect and get metadata before
    /// servers starts and provide them Creates maui app.
    /// </summary>
    /// <returns>A <see cref="MauiApp"/></returns>
    public static MauiApp CreateMauiApp()
    {
        var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(
                $"Undersoft.SVC.Service.Application.Hybrid."
                    + (
                        (DeviceInfo.Platform == DevicePlatform.Android)
                            ? "appsettings.android.json"
                            : "appsettings.json"
                    )
            );

        var configBuilder = new ConfigurationBuilder();
        configBuilder.AddEnvironmentVariables();
        if (stream != null)
            configBuilder.AddJsonStream(stream);
        var config = configBuilder.Build();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Configuration.AddConfiguration(config);

        var development = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if(development != null && development.Equals("Development"))
            builder.Services.AddBlazorWebViewDeveloperTools();

        builder.Logging.AddDebug();

        builder.Services.AddFluentUIComponents();

        var manager = builder
            .Services.AddServiceSetup(config)
            .ConfigureServices(
                new[] { typeof(ApplicationClient), typeof(AccessClient), typeof(EventClient) }
            )
            .Manager;

        _ = manager.UseServiceClients();

        builder.ConfigureContainer(
            manager.GetProviderFactory(),
            (services) =>
            {
                var reg = manager.GetRegistry();
                reg.AddMauiBlazorWebView();
                reg.AddAuthorizationCore()
                    .AddFluentUIComponents((o) => o.UseTooltipServiceProvider = true)
                    .AddViewServices()
                    .AddSingleton<AppearanceState>()
                    .AddScoped<
                        IRemoteRepository<IAccountStore, Account>,
                        RemoteRepository<IAccountStore, Account>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Office>,
                        RemoteRepository<IDataStore, Office>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Manufacturer>,
                        RemoteRepository<IDataStore, Manufacturer>
                    >()
                    .AddScoped<
                        IRemoteRepository<IEventStore, EventInfo>,
                        RemoteRepository<IEventStore, EventInfo>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Appointment>,
                        RemoteRepository<IDataStore, Appointment>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Campaign>,
                        RemoteRepository<IDataStore, Campaign>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Certificate>,
                        RemoteRepository<IDataStore, Certificate>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Procedure>,
                        RemoteRepository<IDataStore, Procedure>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Stock>,
                        RemoteRepository<IDataStore, Stock>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Request>,
                        RemoteRepository<IDataStore, Request>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Traffic>,
                        RemoteRepository<IDataStore, Traffic>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Vaccine>,
                        RemoteRepository<IDataStore, Vaccine>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, PostSymptom>,
                        RemoteRepository<IDataStore, PostSymptom>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, PostSymptom>,
                        RemoteRepository<IDataStore, PostSymptom>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Supplier>,
                        RemoteRepository<IDataStore, Supplier>
                    >()
                    .AddScoped<AccessProvider<Account>>()
                    .AddScoped<AuthenticationStateProvider, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccessProvider<Account>, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                      .AddScoped<IAccessContext, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccess, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccessService<Account>, AccessProvider<Account>>(sp =>
                        sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IValidator<IViewData<Credentials>>, AccessValidator>()
                    .AddScoped<IValidator<IViewData<Account>>, AccountValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Event>>, EventValidator>()
                    .AddScoped<
                        IValidator<IViewData<ViewModels.Vaccination.Appointment>>,
                        AppointmentValidator
                    >()
                    .AddScoped<IValidator<IViewData<ViewModels.Catalogs.Office>>, OfficeValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Catalogs.Vaccine>>, VaccineValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Inventory.Stock>>, StockValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Vaccination.Procedure>>, ProcedureValidator>()
                    .AddScoped<
                        IValidator<IViewData<ViewModels.Vaccination.Certificate>>,
                        CertificateValidator
                    >()
                    .AddScoped<
                        IValidator<IViewData<ViewModels.Vaccination.PostSymptom>>,
                        PostSymptomValidator
                    >()
                    .AddScoped<IValidator<IViewData<ViewModels.Inventory.Request>>, RequestValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Inventory.Traffic>>, TrafficValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Catalogs.Campaign>>, CampaignValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Catalogs.Supplier>>, SupplierValidator>()
                    .AddScoped<
                        IValidator<IViewData<ViewModels.Catalogs.Manufacturer>>,
                        ManufacturerValidator
                    >();
                reg.MergeServices(services, true);
            }
        );

        var host = builder.Build();
        return host;
    }
}
