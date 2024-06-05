using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Reflection;
using Undersoft.SCC.Service.Application.GUI.Compound.Access;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Validatore;
using Undersoft.SCC.Service.Clients;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Application.Hybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(
                $"Undersoft.SCC.Service.Application.Hybrid."
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
                        IRemoteRepository<IDataStore, ViewModels.Contact>,
                        RemoteRepository<IDataStore, ViewModels.Contact>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, ViewModels.Group>,
                        RemoteRepository<IDataStore, ViewModels.Group>
                    >()
                    .AddScoped<
                        IRemoteRepository<IDataStore, Contracts.Country>,
                        RemoteRepository<IDataStore, Contracts.Country>
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
                    .AddScoped<IValidator<IViewData<ViewModels.Contact>>, ContactValidator>()
                    .AddScoped<IValidator<IViewData<ViewModels.Group>>, GroupValidator>()
                    .AddScoped<IValidator<IViewData<Contracts.Country>>, CountryValidator>()
                    .AddScoped<AccountValidator>()
                    .AddScoped<AccessValidator>()
                    .AddScoped<ContactValidator>()
                    .AddScoped<GroupValidator>()
                    .AddScoped<CountryValidator>();
                reg.MergeServices(services, true);
            }
        );

        var host = builder.Build();
        return host;
    }
}
