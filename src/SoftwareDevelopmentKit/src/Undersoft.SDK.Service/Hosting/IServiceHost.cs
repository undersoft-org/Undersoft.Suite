using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public interface IServiceHost : IHost, IHostedService
    {
        ServiceHost CreateHost(string[] args = null);

        IHostBuilder Configure(string[] args = null);

        IHostBuilder AddWorker<TStartup>() where TStartup : BackgroundService;

        Task RunAsync<TStartup>(Type[] clientServiceType) where TStartup : class, IHostedService;

        void Dispose();

        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);

        IHost Host { get; set; }
        string HostName { get; set; }
        string? Address { get; set; }
        int Port { get; set; }
        string Route { get; set; }
        string Name { get; set; }
        long TenantId { get; set; }
        string TenantName { get; set; }
        string TypeName { get; set; }
        IServiceProvider Services { get; }
        SslCertificate? Certificate { get; set; }
    }
}