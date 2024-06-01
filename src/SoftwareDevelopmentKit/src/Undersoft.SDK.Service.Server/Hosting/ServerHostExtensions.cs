using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Undersoft.SDK.Service.Server.Hosting;

using Microsoft.OData.Edm;
using Service.Hosting;

public static class ServerHostExtensions
{
    public static IServerHostSetup UseServerSetup(this IApplicationBuilder app)
    {
        return new ServerHostSetup(app);
    }
    public static IServerHostSetup UseServerSetup(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        return new ServerHostSetup(app, env);
    }
    public static IApplicationBuilder UseDefaultProvider(this IApplicationBuilder app)
    {
        new ServerHostSetup(app).UseDefaultProvider();
        return app;
    }

    public static IApplicationBuilder UseInternalProvider(this IApplicationBuilder app)
    {
        new ServerHostSetup(app).UseInternalProvider();
        return app;
    }

    public static IApplicationBuilder RebuildProviders(this IApplicationBuilder app)
    {
        new ServerHostSetup(app).RebuildProviders();
        return app;
    }

    public static async Task LoadOpenDataEdms(this ServerHostSetup app, int delayInSeconds = 0)
    {
        await Task.Run((Action)(async () =>
        {
            if (delayInSeconds > 0)
                await Task.Delay(delayInSeconds);

            Task.WaitAll(app.Manager.GetClients().ForEach((Func<IRepositoryClient, Task<IEdmModel>>)((client) =>
            {
                return (Task<IEdmModel>)client.BuildMetadata();
            })).Commit());

            app.Manager.Registry.AddOpenDataRemoteImplementations();
            app.RebuildProviders();
        }));
    }
}