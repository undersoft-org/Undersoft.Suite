using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public class ServiceHost : Identifiable, IHost, IServiceHost
    {
        protected IServiceHostBuilder _hostBuilder;

        public ServiceHost()
        {
            _hostBuilder = new ServiceHostBuilder(this);
        }

        public virtual IHostBuilder AddWorker<TStartup>()
            where TStartup : BackgroundService
        {
            return _hostBuilder.AddWorker<TStartup>();
        }

        public virtual IHostBuilder Configure(string[] args = null)
        {
            return _hostBuilder.Configure(args);
        }

        public virtual Task RunAsync<TStartup>(Type[] clientServiceTypes = null) where TStartup : class, IHostedService
        {
            Host = _hostBuilder.Build<TStartup>(clientServiceTypes);

            return Host.RunAsync();
        }

        public string Name { get; set; }

        public IHost Host { get; set; }

        public string HostName { get; set; }

        public string Address { get; set; }

        public int Port { get; set; }

        public SslCertificate Certificate { get; set; }

        public string Route { get; set; }

        public long TenantId { get; set; }

        public string TenantName { get; set; }

        public IServiceProvider Services => Host.Services;

        public IServicer GetServicer() => Services.GetRequiredService<IServicer>();

        public void Dispose()
        {
            Host.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            return Host.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return Host.StopAsync(cancellationToken);
        }
    }
}