using Microsoft.AspNetCore.Http;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Server.Hosting;

public class ServerHostJwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServicer _servicer;

    public ServerHostJwtMiddleware(RequestDelegate next, IServicer servicer)
    {
        _next = next;
        _servicer = servicer;
    }

    public async Task Invoke(HttpContext context)
    {
        var auth = _servicer.GetService<IAuthorization>();
        auth.Credentials.SessionToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
        await _next(context);
    }
}