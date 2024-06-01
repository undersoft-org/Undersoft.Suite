using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.Hosting;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Application.Server.Hosting;

public class ApplicationServerHost : ServiceHost, IHost, IApplicationServerHost
{
    private readonly HostBuilder _hostBuilder;

    public ApplicationServerHost(Action<IWebHostBuilder> builder) : this()
    {
        Configure(builder);
    }

    public ApplicationServerHost(string[]? args = null)
        : this(ServiceConfigurationHelper.BuildConfiguration(args)) { }

    public ApplicationServerHost(IConfiguration configuration)
    {
        _hostBuilder = new HostBuilder();
        ApplicationHosts = new Registry<ApplicationHost>();
        configuration.Bind("General", this);
        _hostBuilder.ConfigureWebHost(
            builder =>
                builder
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(configuration)
                    .UseKestrel((c, o) => o.Configure(c.Configuration.GetSection("Kestrel")))
                    .UseStaticWebAssets()
        );
    }

    public ApplicationServerHost Configure(Action<IWebHostBuilder> builder)
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

    public Registry<ApplicationHost> ApplicationHosts { get; set; } = default!;

    private Registry<IServiceProvider>? hostedApplications;
    public Registry<IServiceProvider> HostedApplications =>
        hostedApplications ??= ApplicationHosts.Select(s => s.Services).ToRegistry();
}
