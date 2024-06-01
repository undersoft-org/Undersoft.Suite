using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Undersoft.SDK.Service.Configuration;

namespace Undersoft.SDK.Service.Hosting
{
    public class ServiceHostBuilder : IServiceHostBuilder
    {
        IServiceHost _serviceHost = default!;
        IHostBuilder _hostBuilder = default!;
        protected IServiceManager manager = default!;

        public ServiceHostBuilder(IServiceHost serviceHost)
        {
            _serviceHost = serviceHost;
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

        public IHost Build<TStartup>(Type[] serviceClients = null) where TStartup : class, IHostedService
        {
            _hostBuilder.ConfigureServices(
             (hostContext, services) =>
             {
                 services.AddHostedService<TStartup>()
                 .AddSingleton((ServiceHost)_serviceHost);
                 var setup = services.AddServiceSetup(hostContext.Configuration)
                     .ConfigureServices(serviceClients);
                 setup.Manager.Registry.MergeServices(services, true);
                 setup.Manager.Registry.ReplaceServices(services);
                 manager = setup.Manager;
             }
         );

            _serviceHost.Host = _hostBuilder.Build();
            manager.Registry.MergeServices();
            manager.BuildInternalProvider();
            _ = manager.UseServiceClients();

            return _serviceHost.Host;
        }

        public IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService
        {
            _hostBuilder.ConfigureServices(services => services.AddHostedService<TStartup>());
            return _hostBuilder;
        }

    }
}
