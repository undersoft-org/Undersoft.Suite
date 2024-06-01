using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Application.Server;

public static class ServerSetupExtensions
{
    public static IApplicationServerSetup AddApplicationServerSetup(
        this IServiceCollection services,
        IMvcBuilder? mvcBuilder = null
    )
    {
        return new ApplicationServerSetup(services, mvcBuilder);
    }
}
