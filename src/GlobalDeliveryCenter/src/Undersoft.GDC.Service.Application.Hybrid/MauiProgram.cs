using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Reflection;
using Undersoft.GDC.Service.Application.GUI.Compound.Access;
using Undersoft.GDC.Service.Clients;
using Undersoft.GDC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GDC.Service.Application.Hybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var stream = Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(
                $"Undersoft.GDC.Service.Application.Hybrid."
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
            .ConfigureServices(new[] {
                typeof(ApplicationClient),
                typeof(AccessClient)
            },
            s => s.AddValidators())
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
                    .AddScoped<IRemoteRepository<IAccountStore, Account>, RemoteRepository<IAccountStore, Account>>()
                    .AddScoped<IRemoteRepository<IDataStore, Contracts.Service>, RemoteRepository<IDataStore, Contracts.Service>>()
                    .AddScoped<IRemoteRepository<IDataStore, Contracts.Member>, RemoteRepository<IDataStore, Contracts.Member>>()
                    .AddScoped<AccessProvider<Account>>()
                    .AddScoped<AuthenticationStateProvider, AccessProvider<Account>>(sp => sp.GetRequiredService<AccessProvider<Account>>())
                    .AddScoped<IAccess, AccessProvider<Account>>(sp => sp.GetRequiredService<AccessProvider<Account>>())
                    .AddScoped<IAccessService<Account>, AccessProvider<Account>>(sp => sp.GetRequiredService<AccessProvider<Account>>())
                    .AddScoped<IValidator<IViewData<Credentials>>, AccessValidator>()
                    .AddScoped<IValidator<IViewData<Account>>, AccountValidator>()
                    .AddScoped<AccountValidator>()
                    .AddScoped<AccessValidator>();
                reg.MergeServices(services, true);
                reg.ReplaceServices(services);
            }
        );

        var host = builder.Build();

        manager.ReplaceProvider(host.Services);

        return host;
    }
}
