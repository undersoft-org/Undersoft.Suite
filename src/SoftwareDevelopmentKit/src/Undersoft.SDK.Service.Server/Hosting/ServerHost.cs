using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Server.Hosting;

public class ServerHost : ServiceHost, IHost, IServerHost
{
    protected new HostBuilder _hostBuilder;

    public ServerHost(Action<IWebHostBuilder> builder) : this()
    {
        Configure(builder);
    }

    public ServerHost(string[] args = null) : this(ServiceConfigurationHelper.BuildConfiguration(args))
    {
    }

    public ServerHost(IConfiguration configuration)
    {
        _hostBuilder = new HostBuilder();
        ServiceHosts = new Registry<ServiceHost>();
        configuration.Bind("General", this);
        _hostBuilder.ConfigureWebHost(
            builder =>
                builder
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(configuration)
                    .UseKestrel((c, o) => o.Configure(c.Configuration.GetSection("Kestrel")))
        );
    }

    public ServerHost Configure(Action<IWebHostBuilder> builder)
    {
        _hostBuilder.ConfigureWebHost(builder);

        return this;
    }

    public void Run<TStartup>() where TStartup : class
    {
        Host = _hostBuilder.ConfigureWebHost(builder => builder.UseStartup<TStartup>()).Build();

        using (Host)
        {
            Host.Run();
        }
    }

    public Registry<ServiceHost> ServiceHosts { get; set; }

    private Registry<IServiceProvider> hostedServices;
    public Registry<IServiceProvider> HostedServices =>
        hostedServices ??= ServiceHosts.Select(s => s.Services).ToRegistry();
}
