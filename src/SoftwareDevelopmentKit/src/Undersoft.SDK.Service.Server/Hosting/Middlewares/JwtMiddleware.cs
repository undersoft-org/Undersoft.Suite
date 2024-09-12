using Microsoft.AspNetCore.Http;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Server.Accounts.Tokens;

namespace Undersoft.SDK.Service.Server.Hosting.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServicer _servicer;

    public JwtMiddleware(RequestDelegate next, IServicer servicer)
    {
        _next = next;
        _servicer = servicer;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();
            var servicer = _servicer.GetTenantServicer(context.User);        
            var auth = servicer.GetManager().GetService<IAuthorization>();
            auth.Credentials.SessionToken = token.Split(" ").LastOrDefault();
        }
        await _next(context);
    }
}