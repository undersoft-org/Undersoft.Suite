using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public interface IServiceHostBuilder
    {
        IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService;
        IHost Build<TStartup>(Type[] serviceClients = null) where TStartup : class, IHostedService;
        IHostBuilder Configure(string[] args = null);
    }
}