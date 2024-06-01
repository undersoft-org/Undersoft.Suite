using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Application.Hosting
{
    public interface IApplicationHostBuilder
    {
        IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService;
        IHost Build<TStartup>(Type[] serviceClients = null) where TStartup : class, IHostedService;
        IHostBuilder Configure(string[] args = null);
    }
}