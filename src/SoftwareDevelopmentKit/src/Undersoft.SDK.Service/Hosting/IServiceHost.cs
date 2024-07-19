using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public interface IServiceHost : IHost, IHostedService
    {
        IHostBuilder Configure(string[] args = null);

        IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService;

        Task RunAsync<TStartup>(Type[] clientServiceType) where TStartup : class, IHostedService;

        IHost Host { get; set; }
        string HostName { get; set; }
        string Address { get; set; }
        int Port { get; set; }
        string Route { get; set; }
        string Name { get; set; }
        long TenantId { get; set; }
        string TenantName { get; set; }
        string TypeName { get; set; }
        SslCertificate Certificate { get; set; }
    }
}