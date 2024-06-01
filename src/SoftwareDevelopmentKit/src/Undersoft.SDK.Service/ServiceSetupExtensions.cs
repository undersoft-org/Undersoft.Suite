using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service
{
    public static class ServiceSetupExtensions
    {
        public static IServiceSetup AddServiceSetup(this IServiceCollection services)
        {
            return new ServiceSetup(services);
        }

        public static IServiceSetup AddServiceSetup(this IServiceCollection services, IConfiguration configuration)
        {
            return new ServiceSetup(services, configuration);
        }
    }
}
