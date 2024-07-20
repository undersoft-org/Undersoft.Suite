using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Server.Hosting;

public class ServerHostMultitenancyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServicer _servicer;

    public ServerHostMultitenancyMiddleware(RequestDelegate next, IServicer servicer)
    {
        _next = next;
        _servicer = servicer;
    }

    public async Task Invoke(HttpContext context)
    {
        KeyValuePair<Type, object>? requestServiceProvider = context.Features.FirstOrDefault(kvp => kvp.Key == typeof(IServiceProvidersFeature));
        if (requestServiceProvider != null)
        {
            var services = ((IServiceProvidersFeature)requestServiceProvider.Value.Value).RequestServices;
        }

        var auth = _servicer.GetService<IAuthorization>();
        auth.Credentials.SessionToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
        await _next(context);
    }
}