using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Undersoft.SCC.Service.Application.GUI.Compound.Access;
using Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Validatore;
using Undersoft.SCC.Service.Clients;
using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.Access;
using Undersoft.SDK.Service.Application.GUI.Models;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Application.Client
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
                        typeof(AccessClient),
                        typeof(EventClient)
                    }
                )
                .Manager;

            await manager.UseServiceClients();

            builder.ConfigureContainer(
                manager.GetProviderFactory(),
                (services) =>
                {
                    var reg = manager.GetRegistry();
                    reg.AddAuthorizationCore()
                        .AddFluentUIComponents(
                            (o) => o.UseTooltipServiceProvider = true)
                        .AddScoped<
                            IRemoteRepository<IAccountStore, Account>,
                            RemoteRepository<IAccountStore, Account>
                        >()
                        .AddSingleton<AppearanceState>()
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
                         .AddScoped<IValidator<IViewData<Contact>>, ContactValidator>()
                        .AddScoped<IValidator<IViewData<Group>>, GroupValidator>()
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
            await host.RunAsync();
        }
    }
}
