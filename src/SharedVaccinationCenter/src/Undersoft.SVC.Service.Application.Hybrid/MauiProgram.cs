using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Reflection;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   application: Undersoft.SVC.Service.Application.Hybrid
// ********************************************************

using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Application.Hybrid;

using Undersoft.SDK.Service.Data.Event;
using Undersoft.SVC.Service.Application.GUI.Compound.Access;
using Undersoft.SVC.Service.Application.GUI.Compound.Presenting.Validators;
using Undersoft.SVC.Service.Clients;
using Undersoft.SVC.Service.Contracts;

/// <summary>
/// The maui program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// There is not any async MAUI entry point type to run application. That's why client started
    /// in the same time with servers will throw exception ones or twice to logs 
    /// http client cannot connect and get metadata before servers starts and provide them
    /// Creates maui app.
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
        var config =
            (stream != null) ? new ConfigurationBuilder().AddJsonStream(stream).Build() : null;

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        builder.Services.AddFluentUIComponents();

        if (config != null)
            builder.Configuration.AddConfiguration(config);

        var manager = builder.Services
            .AddServiceSetup(config)
            .ConfigureServices(
                new[] { typeof(ApplicationClient), typeof(AccessClient), typeof(EventClient) },
                s => s.AddValidators()
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
                        IRemoteRepository<IEventStore, Event>,
                        RemoteRepository<IEventStore, Event>
                    >()
                    .AddScoped<AccessProvider<Account>>()
                    .AddScoped<AuthenticationStateProvider, AccessProvider<Account>>(
                        sp => sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccountAccess, AccessProvider<Account>>(
                        sp => sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IAccountService<Account>, AccessProvider<Account>>(
                        sp => sp.GetRequiredService<AccessProvider<Account>>()
                    )
                    .AddScoped<IValidator<IViewData<Credentials>>, AccessValidator>()
                    .AddScoped<IValidator<IViewData<Account>>, AccountValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Appointment>>, AppointmentValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Office>>, OfficeValidator>();
                reg.MergeServices(services, true);
            }
        );

        var host = builder.Build();
        return host;
    }
}
