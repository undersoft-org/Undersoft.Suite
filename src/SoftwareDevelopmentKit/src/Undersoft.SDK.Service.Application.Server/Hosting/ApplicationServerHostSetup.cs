using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Undersoft.SDK.Service.Server.Hosting;

namespace Undersoft.SDK.Service.Application.Server.Hosting;

public class ApplicationServerHostSetup : ServerHostSetup, IApplicationServerHostSetup
{
    public ApplicationServerHostSetup(IApplicationBuilder application) : base(application) { }

    public ApplicationServerHostSetup(
        IApplicationBuilder application,
        IWebHostEnvironment environment
    ) : base(application, environment) { }

    public IApplicationServerHostSetup UseServiceApplication()
    {
        UseHeaderForwarding();       

        if (_environment.IsDevelopment())
        {
            _builder.UseDeveloperExceptionPage()
                .UseWebAssemblyDebugging();
        }
        else
        {
            _builder.UseExceptionHandler("/Error")
                .UseHsts();
        }

        _builder
            .UseODataBatching()
            .UseODataQueryRequest()
            .UseBlazorFrameworkFiles()
            .UseStaticFiles()
            .UseRouting()
            .UseCors(o => o.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        UseSwaggerSetup(new[] { "v1.0" });

        _builder.UseAuthentication()
            .UseAuthorization();

        UseJwtMiddleware();
        UseMultitenancy();

        _builder.UseApplicationTracking();

        UseEndpoints(true);

        return this;
    }
}
