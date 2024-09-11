using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Access.MultiTenancy;

namespace Undersoft.SDK.Service.Server.Hosting.Middlewares;

public class MultiTenancyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAuthorization _authorization;
    private IServicer _servicer;

    public MultiTenancyMiddleware(
        RequestDelegate next,
        IServicer servicer,
        IAuthorization authorization
    )
    {
        _next = next;
        _servicer = servicer;
        _authorization = authorization;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (
            context.User.Identity.IsAuthenticated
            && long.TryParse(
                context.User.Claims.FirstOrDefault(c => c.Type == "tenant_id")?.Value,
                out var tenantId
            )
        )
        {
            var tenant = _servicer.GetKeyedObject<ITenant>(tenantId);
            if (tenant == null)
            {
                tenant = new Tenant() { Id = tenantId };
                var setup = new ServerSetup(tenant);
                setup.ConfigureTenant(_servicer);
                _servicer = _servicer.GetManager().GetService<IServicer>();
                DataMigrations(_servicer.GetKeyedObject<IServiceManager>(tenantId));
            }
        }
        await _next(context);
    }

    private void ReplaceRequestProvider(HttpContext context, long tenantId)
    {
        KeyValuePair<Type, object>? requestServiceProvider = context.Features.FirstOrDefault(kvp =>
            kvp.Key == typeof(IServiceProvidersFeature)
        );
        if (requestServiceProvider != null)
        {
            ((IServiceProvidersFeature)requestServiceProvider.Value.Value).RequestServices =
                _servicer.GetKeyedObject<IServiceManager>(tenantId).CreateScope().ServiceProvider;
        }
    }

    private void DataMigrations(IServiceManager manager)
    {
        using (IServiceScope scope = manager.CreateScope())
        {
            try
            {
                scope
                    .ServiceProvider.GetRequiredService<IServicer>()
                    .GetSources()
                    .ForEach(e => e.Context.Database.Migrate());
            }
            catch (Exception ex)
            {
                this.Error<Applog>(
                    "DataServer migration initial create - unable to connect the database engine",
                    null,
                    ex
                );
            }
        }
    }
}
