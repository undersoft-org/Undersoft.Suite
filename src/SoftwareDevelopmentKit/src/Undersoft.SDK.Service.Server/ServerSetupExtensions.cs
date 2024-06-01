using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Server
{
    public static class ServerSetupExtensions
    {
        public static IServerSetup AddServerSetup(this IServiceCollection services, IMvcBuilder mvcBuilder = null)
        {
            return new ServerSetup(services, mvcBuilder);
        }

        public static async Task LoadOpenDataEdms(this ServerSetup app)
        {
            await Task.Run(() =>
            {
                app.Manager.GetClients().ForEach((client) =>
                {
                    client.BuildMetadata();
                });

                app.Manager.Registry.AddOpenDataRemoteImplementations();
            });
        }
    }
}
