using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Undersoft.SDK.Service.Server;

using Data.Model;
using Series;
using System.Collections.Generic;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Operation.Invocation;
using Undersoft.SDK.Service.Operation.Invocation.Handler;
using Undersoft.SDK.Service.Operation.Invocation.Notification;
using Undersoft.SDK.Service.Operation.Invocation.Notification.Handler;

public partial class ServerSetup
{
    public IServerSetup AddServerSetupInvocationImplementations()
    {
        IServiceRegistry service = registry;

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        IServiceCollection deck = service
            .AddTransient<ISeries<IViewModel>, Registry<IViewModel>>()
            .AddScoped<ITypedSeries<IViewModel>, TypedRegistry<IViewModel>>();

        var controllerTypes = assemblies
            .SelectMany(
                a =>
                    a.GetTypes()
                        .Where(
                            type =>
                                type.GetCustomAttributes()
                                    .Any(
                                        a =>
                                            a.GetType()
                                                .IsAssignableTo(typeof(ServiceClientAttribute))
                                    )
                        )
                        .ToArray()
            )
            .Where(
                b =>
                    !b.IsAbstract
                    && b.BaseType.IsGenericType
                    && b.BaseType.GenericTypeArguments.Length > 2
            )
            .ToArray();

        HashSet<string> duplicateCheck = new HashSet<string>();

        foreach (var controllerType in controllerTypes)
        {
            var genericTypes = controllerType.BaseType.GenericTypeArguments;

            if (genericTypes.Length < 3)
                continue;

            Type[] list = GetStoreModelServiceTypes(genericTypes);

            Type storeType = list[0];
            Type serviceType = list[2];
            Type modelType = list[1];

            if (duplicateCheck.Add(storeType.Name + serviceType.Name + modelType.Name))
            {
                service.AddScoped(typeof(IInvoker<>).MakeGenericType(serviceType),
                                  typeof(Invoker<>).MakeGenericType(serviceType));

                service.AddTransient(
                    typeof(IRequest<>).MakeGenericType(
                        typeof(Invocation<>).MakeGenericType(modelType)
                    ),
                    typeof(Invocation<>).MakeGenericType(modelType)
                );
                service.AddTransient(
                 typeof(IRequestHandler<,>).MakeGenericType(
                     new[]
                     {
                            typeof(Access<,,>).MakeGenericType(storeType, serviceType, modelType),
                            typeof(Invocation<>).MakeGenericType(modelType)
                     }
                 ),
                 typeof(AccessHandler<,,>).MakeGenericType(storeType, serviceType, modelType)
                );
                service.AddTransient(
                    typeof(IRequestHandler<,>).MakeGenericType(
                        new[]
                        {
                            typeof(Action<,,>).MakeGenericType(storeType, serviceType, modelType),
                            typeof(Invocation<>).MakeGenericType(modelType)
                        }
                    ),
                    typeof(ActionHandler<,,>).MakeGenericType(storeType, serviceType, modelType)
                );
                service.AddTransient(
                    typeof(IRequestHandler<,>).MakeGenericType(
                        new[]
                        {
                            typeof(Setup<,,>).MakeGenericType(storeType, serviceType, modelType),
                            typeof(Invocation<>).MakeGenericType(modelType)
                        }
                    ),
                    typeof(SetupHandler<,,>).MakeGenericType(storeType, serviceType, modelType)
                );
                service.AddTransient(
                  typeof(INotificationHandler<>).MakeGenericType(
                      typeof(AccessInvoked<,,>).MakeGenericType(storeType, serviceType, modelType)
                  ),
                  typeof(AccessInvokedHandler<,,>).MakeGenericType(
                      storeType,
                      serviceType,
                      modelType
                  )
              );
                service.AddTransient(
                    typeof(INotificationHandler<>).MakeGenericType(
                        typeof(ActionInvoked<,,>).MakeGenericType(storeType, serviceType, modelType)
                    ),
                    typeof(ActionInvokedHandler<,,>).MakeGenericType(
                        storeType,
                        serviceType,
                        modelType
                    )
                );
                service.AddTransient(
                    typeof(INotificationHandler<>).MakeGenericType(
                        typeof(SetupInvoked<,,>).MakeGenericType(storeType, serviceType, modelType)
                    ),
                    typeof(SetupInvokedHandler<,,>).MakeGenericType(
                        storeType,
                        serviceType,
                        modelType
                    )
                );
            }
        }
        return this;
    }

    private Type[] GetStoreModelServiceTypes(Type[] genericTypes)
    {
        Type[] list = new Type[3];
        int store = 0,
            model = 1,
            service = 2;

        if (genericTypes.Length > 5)
        {
            list[store] = genericTypes[1];
            list[service] = genericTypes[5];
            list[model] = genericTypes[4];
        }
        else if (genericTypes.Length > 4)
        {
            list[store] = genericTypes[1];
            list[model] = genericTypes[3];
            list[service] = genericTypes[4];
        }
        else if (genericTypes.Length > 3)
        {
            list[store] = genericTypes[1];
            list[model] = genericTypes[2];
            list[service] = genericTypes[3];
        }
        else
        {
            list[store] = genericTypes[0];
            list[model] = genericTypes[2];
            list[service] = genericTypes[1];
        }
        return list;
    }
}
