using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Application.GUI.View.Abstraction;

namespace Undersoft.SDK.Service.Application.GUI;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add common services required by the Fluent UI Web Components for Blazor library
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Library configuration</param>
    public static IServiceCollection AddViewServices(this IServiceCollection services)
    {
        services.AddScoped<IViewDialogAnimations, ViewDialogAnimations>();
        return services;
    }
}
