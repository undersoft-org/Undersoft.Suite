using Microsoft.Extensions.Hosting;
using Undersoft.SDK.Service.Hosting;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service.Hosting
{
    public static class ServiceHostSetupExtensions
    {
        public static IServiceHostSetup UseAppSetup(this IHostBuilder app, IServiceManager sm, IHostEnvironment env)
        {
            return new ServiceHostSetup(app, sm, env);
        }

        public static IHostBuilder UseInternalProvider(this IHostBuilder app, IServiceManager sm)
        {
            new ServiceHostSetup(app,sm).UseInternalProvider();
            return app;
        }

        public static IHostBuilder RebuildProviders(this IHostBuilder app, IServiceManager sm)
        {
            new ServiceHostSetup(app, sm).RebuildProviders();
            return app;
        }

        public static async Task LoadOpenDataEdms(this IServiceHostSetup app)
        {
            await Task.Run(() =>
            {
                app.Manager.GetClients().ForEach((client) =>
                {
                    client.BuildMetadata();
                });

                app.Manager.Registry.AddOpenDataRemoteImplementations();
                app.RebuildProviders();
            });
        }

    }
}