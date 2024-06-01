using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Undersoft.SDK.Service.Data.Event.Bus;

using Configuration;
using Undersoft.SDK.Service.Data.Event.Handler;

public class EventBusModule
{
    private ServiceConfigurationContext context { get; set; }

    public void PreConfigureServices(ServiceConfigurationContext context)
    {
        this.context = context;
        AddEventHandlers(context.Services);
    }

    private static void AddEventHandlers(IServiceRegistry services)
    {
        var localHandlers = new List<Type>();

        services.OnRegistred(context =>
        {
            if (context.ImplementationType.IsAssignableTo(typeof(IEventHandler<>)))
            {
                localHandlers.Add(context.ImplementationType);
            }
        });

        services.Configure<EventBusOptions>(options =>
        {
            localHandlers.Select(h => !options.Handlers.Any(o => o.GetType().Equals(h)));
        });
    }
}