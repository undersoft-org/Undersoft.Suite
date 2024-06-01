using Microsoft.Extensions.Hosting;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Application.Hosting
{
    public interface IApplicationHost
    {
        string? Address { get; set; }
        SslCertificate? Certificate { get; set; }
        IHost Host { get; set; }
        string? HostName { get; set; }
        string? Name { get; set; }
        int Port { get; set; }
        string? Route { get; set; }
        IServiceProvider Services { get; }
        long TenantId { get; set; }
        string? TenantName { get; set; }
        string TypeName { get; set; }

        ApplicationHost CreateHost(string[] args = null);

        IHostBuilder Configure(string[] args = null);

        IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService;

        Task RunAsync<TStartup>(Type[] clientServiceType) where TStartup : class, IHostedService;

        void Dispose();

        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);
    }
}