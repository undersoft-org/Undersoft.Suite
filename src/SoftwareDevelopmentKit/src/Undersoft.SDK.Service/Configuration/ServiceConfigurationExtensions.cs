using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Undersoft.SDK.Service.Configuration;

public static class ServiceConfigurationExtensions
{
    public static IServiceConfiguration BuildConfiguration(this IConfiguration config)
    {
        return new ServiceConfiguration(config);
    }

    public static IServiceConfiguration BuildConfiguration(
        this IConfiguration config,
        IServiceCollection services
    )
    {
        var _config = new ServiceConfiguration(config);
        _config.Services = services;
        return _config;
    }

    public static IServiceCollection ReplaceConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.Replace(ServiceDescriptor.Singleton(configuration));
    }

    public static IConfiguration GetConfiguration(this IServiceRegistry services)
    {
        var hostBuilderContext = services.GetSingleton<HostBuilderContext>();
        if (hostBuilderContext?.Configuration != null)
        {
            return hostBuilderContext.Configuration as IConfigurationRoot;
        }

        return services.GetSingleton<IConfiguration>();
    }

    public static void OnRegistred(this IServiceRegistry services, Action<IOnServiceRegistredContext> registrationAction)
    {
        GetOrCreateRegistrationActionList(services).Add(registrationAction);
    }

    public static ServiceRegistrationActionList GetRegistrationActionList(this IServiceRegistry services)
    {
        return GetOrCreateRegistrationActionList(services);
    }

    private static ServiceRegistrationActionList GetOrCreateRegistrationActionList(IServiceRegistry services)
    {
        var actionList = services.GetSingleton<IServiceObject<ServiceRegistrationActionList>>()?.Value;
        if (actionList == null)
        {
            actionList = new ServiceRegistrationActionList();
            services.AddObject(actionList);
        }

        return actionList;
    }
}

public class ServiceRegistrationActionList : List<Action<IOnServiceRegistredContext>>
{
    public bool IsClassInterceptorsDisabled { get; set; }
}

public interface IOnServiceRegistredContext
{
    Type ImplementationType { get; }
}