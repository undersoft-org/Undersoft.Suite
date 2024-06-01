using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Undersoft.SDK.Service.Configuration;

namespace Undersoft.SDK.Service.Application.Hosting
{
    public class ApplicationHostBuilder : IApplicationHostBuilder
    {
        IApplicationHost _applicationHost;
        IHostBuilder _hostBuilder = default!;

        public ApplicationHostBuilder(IApplicationHost applicationHost)
        {
            _applicationHost = applicationHost;
        }

        public IHostBuilder Configure(string[] args = null)

        {
            var config = ServiceConfigurationHelper.BuildConfiguration();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var host = Host.CreateDefaultBuilder();
            _hostBuilder = host.ConfigureHostConfiguration(configHost =>
            {
                configHost.AddEnvironmentVariables();
                configHost.AddConfiguration(config);
            });

            return _hostBuilder;
        }

        public IHost Build<TStartup>(Type[] serviceClients = null!) where TStartup : class, IHostedService
        {
            _hostBuilder.ConfigureServices(
             (hostContext, services) =>
             {
                 services.AddHostedService<TStartup>()
                 .AddSingleton((ApplicationHost)_applicationHost);
                 var setup = services.AddServiceSetup(hostContext.Configuration)
                     .ConfigureServices(serviceClients);
                 setup.Manager.Registry.MergeServices(services, true);
             }
         );

            _applicationHost.Host = _hostBuilder.Build();
            var _manager = _applicationHost.Host.Services.GetService<IServiceManager>();
            _manager!.ReplaceProvider(_applicationHost.Services);
            _ = _applicationHost.Services.UseServiceClients();

            return _applicationHost.Host;
        }

        public IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService
        {
            _hostBuilder.ConfigureServices(services => services.AddHostedService<TStartup>());
            return _hostBuilder;
        }
    }
}
