using Microsoft.AspNetCore.Http;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Server.Accounts.Tokens;

namespace Undersoft.SDK.Service.Server.Hosting.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAuthorization _authorization;

    public JwtMiddleware(RequestDelegate next, IAuthorization authorization)
    {
        _next = next;
        _authorization = authorization;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _authorization.Credentials.SessionToken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
        await _next(context);
    }
}