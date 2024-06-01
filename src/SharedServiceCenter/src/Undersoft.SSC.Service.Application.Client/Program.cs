using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SSC.Service.Application.GUI.Compound.Access;
using Undersoft.SSC.Service.Clients;
using Undersoft.SSC.Service.Contracts;

namespace Undersoft.SSC.Service.Application.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var manager = builder.Services
                .AddServiceSetup(builder.Configuration)
                .ConfigureServices(
                new[]
                {
                    typeof(ApplicationClient),
                    typeof(AccessClient)
                })
                .Manager;

            await manager.UseServiceClients();

            builder.ConfigureContainer(
                manager.GetProviderFactory(),
                (services) =>
                {
                    var reg = manager.GetRegistry();
                    reg.AddAuthorizationCore()
                        .AddFluentUIComponents((o) => { o.UseTooltipServiceProvider = true; })
                        .AddScoped<IRemoteRepository<IAccountStore, Account>, RemoteRepository<IAccountStore, Account>>()
                        .AddSingleton<AppearanceState>()
                        .AddScoped<AccessProvider<Account>>()
                        .AddScoped<AuthenticationStateProvider, AccessProvider<Account>>(sp => sp.GetRequiredService<AccessProvider<Account>>())
                        .AddScoped<IAccountAccess, AccessProvider<Account>>(sp => sp.GetRequiredService<AccessProvider<Account>>())
                        .AddScoped<IAccountService<Account>, AccessProvider<Account>>(sp => sp.GetRequiredService<AccessProvider<Account>>())
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

            await host.RunAsync();
        }
    }
}
