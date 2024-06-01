using Microsoft.AspNetCore.Hosting;
using Undersoft.SDK.Series;
using Undersoft.SDK.Service.Application.Hosting;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Application.Server.Hosting
{
    public interface IApplicationServerHost : IServiceHost
    {
        Registry<ApplicationHost>? ApplicationHosts { get; set; }

        Registry<IServiceProvider> HostedApplications { get; }

        ApplicationServerHost Configure(Action<IWebHostBuilder> builder);

        void Run<TStartup>() where TStartup : class;
    }
}