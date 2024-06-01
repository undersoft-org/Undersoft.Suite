using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Undersoft.SDK.Service.Server;

using Microsoft.IdentityModel.Tokens;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Operation.Invocation;
using Undersoft.SDK.Service.Operation.Remote.Invocation;
using Undersoft.SDK.Service.Operation.Remote.Invocation.Handler;
using Undersoft.SDK.Service.Operation.Remote.Invocation.Notification;
using Undersoft.SDK.Service.Operation.Remote.Invocation.Notification.Handler;

public partial class ServerSetup
{
    public IServerSetup AddServerSetupRemoteInvocationImplementations()
    {
        IServiceRegistry service = registry;

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

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
                                                .IsAssignableTo(typeof(ServiceRemoteAttribute))
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
            Type storeType = null,
                modelType = null,
                serviceType = null;

            var genericTypes = controllerType.BaseType.GenericTypeArguments;

            if (genericTypes.Length < 3)
                continue;

            Type[] list = GetStoreModelServiceTypes(genericTypes);

            storeType = list[0];
            serviceType = list[2];
            modelType = list[1];

            var concatNames = storeType.FullName + modelType.FullName + serviceType.FullName;
            if (!concatNames.IsNullOrEmpty() && duplicateCheck.Add(concatNames))
            {
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
                            typeof(RemoteAccess<,,>).MakeGenericType(
                                storeType,
                                serviceType,
                                modelType
                            ),
                            typeof(Invocation<>).MakeGenericType(modelType)
                       }
                   ),
                   typeof(RemoteAcccessHandler<,,>).MakeGenericType(
                       storeType,
                       serviceType,
                       modelType
                   )
               );
                service.AddTransient(
                    typeof(IRequestHandler<,>).MakeGenericType(
                        new[]
                        {
                            typeof(RemoteAction<,,>).MakeGenericType(
                                storeType,
                                serviceType,
                                modelType
                            ),
                            typeof(Invocation<>).MakeGenericType(modelType)
                        }
                    ),
                    typeof(RemoteActionHandler<,,>).MakeGenericType(
                        storeType,
                        serviceType,
                        modelType
                    )
                );
                service.AddTransient(
                    typeof(IRequestHandler<,>).MakeGenericType(
                        new[]
                        {
                            typeof(RemoteSetup<,,>).MakeGenericType(
                                storeType,
                                serviceType,
                                modelType
                            ),
                            typeof(Invocation<>).MakeGenericType(modelType)
                        }
                    ),
                    typeof(RemoteSetupHandler<,,>).MakeGenericType(
                        storeType,
                        serviceType,
                        modelType
                    )
                );
                service.AddTransient(
                  typeof(INotificationHandler<>).MakeGenericType(
                      typeof(RemoteAccessInvoked<,,>).MakeGenericType(
                          storeType,
                          serviceType,
                          modelType
                      )
                  ),
                  typeof(RemoteAccessInvokedHandler<,,>).MakeGenericType(
                      storeType,
                      serviceType,
                      modelType
                  )
              );
                service.AddTransient(
                    typeof(INotificationHandler<>).MakeGenericType(
                        typeof(RemoteActionInvoked<,,>).MakeGenericType(
                            storeType,
                            serviceType,
                            modelType
                        )
                    ),
                    typeof(RemoteActionInvokedHandler<,,>).MakeGenericType(
                        storeType,
                        serviceType,
                        modelType
                    )
                );
                service.AddTransient(
                    typeof(INotificationHandler<>).MakeGenericType(
                        typeof(RemoteSetupInvoked<,,>).MakeGenericType(
                            storeType,
                            serviceType,
                            modelType
                        )
                    ),
                    typeof(RemoteSetupInvokedHandler<,,>).MakeGenericType(
                        storeType,
                        serviceType,
                        modelType
                    )
                );
            }
        }
        return this;
    }
}
