using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Undersoft.SDK.Service.Server;

using Operation.Remote.Command;
using Operation.Remote.Command.Handler;
using Operation.Remote.Command.Validator;
using Operation.Remote.Query;
using Operation.Remote.Query.Handler;
using Undersoft.SDK.Service.Data.Client.Attributes;
using Undersoft.SDK.Service.Operation.Remote.Command.Notification;
using Undersoft.SDK.Service.Operation.Remote.Command.Notification.Handler;
using Undersoft.SDK.Service.Operation.Remote.Query.Validator;

public partial class ServerSetup
{
    public IServerSetup AddServerSetupRemoteCqrsImplementations()
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
                                                .IsAssignableTo(typeof(DataRemoteAttribute))
                                    )
                        )
                        .ToArray()
            )
            .Where(
                b =>
                    !b.IsAbstract
                    && b.BaseType.IsGenericType
                    && b.BaseType.GenericTypeArguments.Length > 3
            )
            .ToArray();

        foreach (var controllerType in controllerTypes)
        {
            var genericTypes = controllerType.BaseType.GenericTypeArguments;
            var store_t = genericTypes[1];
            var model_t = genericTypes[3];
            var dto_t = genericTypes[2];

            service.AddTransient(
                typeof(IRequest<>).MakeGenericType(
                    typeof(RemoteCommand<>).MakeGenericType(model_t)
                ),
                typeof(RemoteCommand<>).MakeGenericType(model_t)
            );

            service.AddTransient(
              typeof(IRequest<>).MakeGenericType(
                  typeof(RemoteQuery<,>).MakeGenericType(dto_t, model_t)
              ),
              typeof(RemoteQuery<,>).MakeGenericType(dto_t, model_t)
          );

            service.AddTransient(
               typeof(RemoteQueryValidatorBase<>).MakeGenericType(
                   typeof(RemoteQuery<,>).MakeGenericType(dto_t, model_t)
               ),
               typeof(RemoteQueryValidator<,>).MakeGenericType(dto_t, model_t)
           );

            service.AddTransient(
                typeof(RemoteCommandValidatorBase<>).MakeGenericType(
                    typeof(RemoteCommand<>).MakeGenericType(model_t)
                ),
                typeof(RemoteCommandValidator<>).MakeGenericType(model_t)
            );

            service.AddTransient(
               typeof(RemoteCommandSetValidatorBase<>).MakeGenericType(
                   typeof(RemoteCommandSet<>).MakeGenericType(model_t)
               ),
               typeof(RemoteCommandSetValidator<>).MakeGenericType(model_t)
           );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteFilter<,,>).MakeGenericType(store_t, dto_t, model_t),
                       typeof(RemoteQuery<,>).MakeGenericType(dto_t, model_t)
                    }
                ),
                typeof(RemoteFilterHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    typeof(RemoteFind<,,>).MakeGenericType(store_t, dto_t, model_t),
                     typeof(RemoteQuery<,>).MakeGenericType(dto_t, model_t)
                ),
                typeof(RemoteFindHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    typeof(RemoteGet<,,>).MakeGenericType(store_t, dto_t, model_t),
                    typeof(RemoteQuery<,>).MakeGenericType(dto_t, model_t)
                ),
                typeof(RemoteGetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteCreate<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommand<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteCreateHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteUpdate<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommand<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteUpdateHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteChange<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommand<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteChangeHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteDelete<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommand<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteDeleteHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteChangeSet<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommandSet<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteChangeSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteUpdateSet<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommandSet<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteUpdateSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteCreateSet<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommandSet<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteCreateSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(IRequestHandler<,>).MakeGenericType(
                    new[]
                    {
                        typeof(RemoteDeleteSet<,,>).MakeGenericType(store_t, dto_t, model_t),
                        typeof(RemoteCommandSet<>).MakeGenericType(model_t)
                    }
                ),
                typeof(RemoteDeleteSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteDeletedSet<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteDeletedSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteUpdatedSet<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteUpdatedSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteCreatedSet<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteCreatedSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddScoped(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteChangedSet<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteChangedSetHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteChanged<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteChangedHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteCreated<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteCreatedHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteDeleted<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteDeletedHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );

            service.AddTransient(
                typeof(INotificationHandler<>).MakeGenericType(
                    typeof(RemoteUpdated<,,>).MakeGenericType(store_t, dto_t, model_t)
                ),
                typeof(RemoteUpdatedHandler<,,>).MakeGenericType(store_t, dto_t, model_t)
            );
        }
        return this;
    }
}
