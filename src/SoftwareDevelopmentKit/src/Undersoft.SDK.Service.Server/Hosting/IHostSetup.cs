using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Server.Hosting
{
    public interface IHostSetup
    {
        void ConfigureServices(IServiceCollection srvc);

        void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}