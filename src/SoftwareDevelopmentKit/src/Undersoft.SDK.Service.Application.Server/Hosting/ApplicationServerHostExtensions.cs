using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Undersoft.SDK.Service.Application.Server.Hosting;

using System.Net;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

public static class ApplicationServerHostExtensions
{
    public static IApplicationServerHostSetup UseApplicationServerSetup(this IApplicationBuilder app)
    {
        return new ApplicationServerHostSetup(app);
    }
    
    public static IApplicationServerHostSetup UseApplicationServerSetup(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        return new ApplicationServerHostSetup(app, env);
    }

    public static IApplicationBuilder UseApplicationTracking(this IApplicationBuilder app)
    {
        app.UseWhen(context => context.Request.Path.StartsWithSegments("/ip.axd"), app => app.Run(async context =>
        {
            var ip = "";
            var headers = context.Request.Headers;
            if (headers.ContainsKey("X-Forwarded-For"))
            {
                var ips = new List<string>();
                foreach (var xf in headers["X-Forwarded-For"])
                {
                    if (!string.IsNullOrEmpty(xf))
                    {
                        ips.Add(xf);
                    }
                }
                ip = string.Join(";", ips);
            }
            else
            {
                ip = context.Connection?.RemoteIpAddress?.ToIPv4String();
            }

            context.Response.Headers.Add("Content-TypeName", new Microsoft.Extensions.Primitives.StringValues("application/json; charset=utf-8"));
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Id = context.TraceIdentifier, Ip = ip }));
        }));
        return app;
    }
}