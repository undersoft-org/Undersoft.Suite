using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Server;

namespace Undersoft.SDK.Service.Application.Server;

public partial class ApplicationServerSetup : ServerSetup, IApplicationServerSetup
{
    public ApplicationServerSetup(IServiceCollection services, IMvcBuilder? mvcBuilder = null)
        : base(services, mvcBuilder) { }

    public ApplicationServerSetup(IServiceCollection services, IConfiguration configuration)
        : base(services, configuration) { }

    public IServerSetup ConfigureApplicationServer(
        bool includeSwagger = true,
        Type[]? sourceTypes = null,
        Type[]? clientTypes = null
    )
    {
        Services.AddControllersWithViews();
        Services.AddRazorPages();

        base.ConfigureServer(includeSwagger, sourceTypes, clientTypes);

        return this;
    }
}
