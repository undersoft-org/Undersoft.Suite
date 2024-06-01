using Microsoft.AspNetCore.Hosting;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Server.Hosting
{
    public interface IServerHost : IServiceHost
    {
        ServerHost Configure(Action<IWebHostBuilder> builder);

        Registry<IServiceProvider> HostedServices { get; }

        Registry<ServiceHost> ServiceHosts { get; set; }

        void Run<TStartup>() where TStartup : class;
    }
}