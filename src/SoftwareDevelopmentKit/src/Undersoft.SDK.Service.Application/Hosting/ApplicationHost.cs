using Microsoft.Extensions.Hosting;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Application.Hosting;

public class ApplicationHost : Identifiable, IHost, IApplicationHost
{
    protected IApplicationHostBuilder _hostBuilder;

    public ApplicationHost()
    {
        _hostBuilder = new ApplicationHostBuilder(this);
    }

    public virtual ApplicationHost CreateHost(string[] args = null!)
    {
        var sh = new ApplicationHost();
        sh.Configure(args);
        return sh;
    }

    public virtual IHostBuilder AddWorker<TStartup>()
        where TStartup : BackgroundService
    {
        return _hostBuilder.AddWorker<TStartup>();
    }

    public virtual IHostBuilder Configure(string[] args = null!)
    {
        return _hostBuilder.Configure(args);
    }

    public virtual Task RunAsync<TStartup>(Type[] clientServiceTypes = null!) where TStartup : class, IHostedService
    {
        Host = _hostBuilder.Build<TStartup>(clientServiceTypes);

        return Host.RunAsync();
    }

    public IHost Host { get; set; } = default!;

    public string? Name { get; set; }

    public string? HostName { get; set; }

    public string? Address { get; set; }

    public int Port { get; set; }

    public SslCertificate? Certificate { get; set; }

    public string? Route { get; set; }

    public long TenantId { get; set; }

    public string? TenantName { get; set; }

    public IServiceProvider Services => Host.Services;

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Host.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Host.StopAsync(cancellationToken);
    }

    public void Dispose()
    {
        Host?.Dispose();
    }

}