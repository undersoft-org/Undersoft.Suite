using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Undersoft.SDK.Service.Access.MultiTenancy;

namespace Undersoft.SDK.Service.Server.Hosting.Middlewares;

public class MultiTenancyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServicer _servicer;

    public MultiTenancyMiddleware(RequestDelegate next, IServicer servicer)
    {
        _next = next;
        _servicer = servicer;
    }

    public async Task Invoke(HttpContext context)
    {
        if (
            context.User.Identity.IsAuthenticated
            && long.TryParse(
                context.User.Claims.FirstOrDefault(c => c.ValueType == "tenant_id")?.Value,
                out var tenantId
            )
        )
        {
            var tenant = _servicer.GetKeyedObject<ITenant>(tenantId);
            if (tenant == null)
            {

            }
            if (tenant != null)
            {
                KeyValuePair<Type, object>? requestServiceProvider = context.Features.FirstOrDefault(kvp =>
                    kvp.Key == typeof(IServiceProvidersFeature)
                );
                if (requestServiceProvider != null)
                {
                    var services = (
                        (IServiceProvidersFeature)requestServiceProvider.Value.Value
                    ).RequestServices;
                }
            }
        }
        await _next(context);
    }
}
