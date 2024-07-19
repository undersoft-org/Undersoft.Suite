using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Scheduler;

namespace Undersoft.SDK.Service.Hosting
{
    public class SchedulerHostBuilder : IServiceHostBuilder
    {
        IServiceHost _serviceHost;
        IHostBuilder _hostBuilder;
        IServiceManager manager = default!;

        public SchedulerHostBuilder(IServiceHost serviceHost)
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

        public IHost Build<TStartup>(Type[] serviceClients = null)
            where TStartup : class, IHostedService
        {
            _hostBuilder.ConfigureServices(async
                (hostContext, services) =>
                {
                    var setup = services
                        .AddServiceSetup(hostContext.Configuration)
                        .ConfigureServices(serviceClients)
                        .AddSchedulerService();
                    setup.Manager.Registry.AddHostedService<TStartup>()
                         .AddSingleton<SchedulerServiceHost>((SchedulerServiceHost)_serviceHost);
                    setup.Manager.Registry.MergeServices(services, true);
                    setup.Manager.Registry.ReplaceServices(services);
                    manager = setup.Manager;
                    await manager.UseServiceClients();
                }
            );

            _serviceHost.Host = _hostBuilder.Build();

            manager.ReplaceProvider(_serviceHost.Services);

            return _serviceHost.Host;
        }

        public IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService
        {
            _hostBuilder.ConfigureServices(services => services.AddHostedService<TStartup>());
            return _hostBuilder;
        }
    }
}
