using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Undersoft.SDK.Service.Server;

using Data.Identifier;
using Operation.Command;
using Operation.Command.Handler;
using Operation.Command.Validator;
using Operation.Query;
using Operation.Query.Handler;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command.Notification;
using Undersoft.SDK.Service.Operation.Command.Notification.Handler;

public partial class ServerSetup
{
    public IServerSetup AddServerSetupCqrsImplementations()
    {
        IServiceRegistry service = registry;

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        Type[] dtoTypes = assemblies
            .SelectMany(a =>
                a.DefinedTypes.Where(t =>
                    t.UnderlyingSystemType.IsAssignableTo(typeof(IContract))
                    || (
                        t.UnderlyingSystemType.IsGenericType
                        && t.UnderlyingSystemType.GetGenericArguments()[0]
                            .IsAssignableTo(typeof(IContract))
                    )
                )
            )
            .Select(t => t.UnderlyingSystemType)
            .ToArray();

        Catalog<Type> duplicateCheck = new Catalog<Type>();
        Type[] storeTypes = DataStoreRegistry
            .Stores.Where(s => s.IsAssignableTo(typeof(IDataServiceStore)))
            .ToArray();

        foreach (ISeries<IEntityType> contextEntityTypes in DataStoreRegistry.EntityTypes)
        {
            foreach (IEntityType entityType in contextEntityTypes)
            {
                Type entity_t = entityType.ClrType;
                if (duplicateCheck.TryAdd(entity_t))
                {
                    foreach (Type dtoType in dtoTypes)
                    {
                        Type dto_t = dtoType;
                        if (!dto_t.Name.Contains($"{entity_t.Name}"))
                        {
                            if (
                                entity_t.IsGenericType
                                && entity_t.IsAssignableTo(typeof(Identifier))
                                && dto_t.Name.Contains(
                                    entity_t.GetGenericArguments().FirstOrDefault().Name
                                )
                            )
                            {
                                dto_t = typeof(Identifier<>).MakeGenericType(dtoType);
                            }
                            else
                                continue;
                        }
                        service.AddTransient(
                            typeof(IRequest<>).MakeGenericType(
                                typeof(Command<>).MakeGenericType(dto_t)
                            ),
                            typeof(Command<>).MakeGenericType(dto_t)
                        );

                        service.AddTransient(
                            typeof(IRequest<>).MakeGenericType(
                                typeof(Query<,>).MakeGenericType(entity_t, dto_t)
                            ),
                            typeof(Query<,>).MakeGenericType(entity_t, dto_t)
                        );

                        service.AddTransient(
                            typeof(CommandValidatorBase<>).MakeGenericType(
                                typeof(Command<>).MakeGenericType(dto_t)
                            ),
                            typeof(CommandValidator<>).MakeGenericType(dto_t)
                        );

                        service.AddTransient(
                            typeof(CommandSetValidatorBase<>).MakeGenericType(
                                typeof(CommandSet<>).MakeGenericType(dto_t)
                            ),
                            typeof(CommandSetValidator<>).MakeGenericType(dto_t)
                        );

                        service.AddTransient(
                            typeof(QueryValidatorBase<>).MakeGenericType(
                                typeof(Query<,>).MakeGenericType(entity_t, dto_t)
                            ),
                            typeof(QueryValidator<,>).MakeGenericType(entity_t, dto_t)
                        );

                        foreach (Type store_t in storeTypes)
                        {
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(Filter<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Query<,>).MakeGenericType(entity_t, dto_t)
                                    }
                                ),
                                typeof(FilterHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IStreamRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(GetAsync<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        dto_t
                                    }
                                ),
                                typeof(GetAsyncHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    typeof(Find<,,>).MakeGenericType(store_t, entity_t, dto_t),
                                    typeof(Query<,>).MakeGenericType(entity_t, dto_t)
                                ),
                                typeof(FindHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    typeof(Get<,,>).MakeGenericType(store_t, entity_t, dto_t),
                                    typeof(Query<,>).MakeGenericType(entity_t, dto_t)
                                ),
                                typeof(GetHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(Create<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(CreateHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(Upsert<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(UpsertHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(Update<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(UpdateHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(Change<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(ChangeHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(Delete<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(DeleteHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddScoped(
                                typeof(IStreamRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(ChangeSetAsync<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(ChangeSetAsyncHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(ChangeSet<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(CommandSet<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(ChangeSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(UpdateSet<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(CommandSet<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(UpdateSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IStreamRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(UpdateSetAsync<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(UpdateSetAsyncHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(CreateSet<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(CommandSet<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(CreateSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IStreamRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(CreateSetAsync<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(CreateSetAsyncHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(UpsertSet<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(CommandSet<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(UpsertSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IStreamRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(UpsertSetAsync<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(UpsertSetAsyncHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(DeleteSet<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(CommandSet<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(DeleteSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(IStreamRequestHandler<,>).MakeGenericType(
                                    new[]
                                    {
                                        typeof(DeleteSetAsync<,,>).MakeGenericType(
                                            store_t,
                                            entity_t,
                                            dto_t
                                        ),
                                        typeof(Command<>).MakeGenericType(dto_t)
                                    }
                                ),
                                typeof(DeleteSetAsyncHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(DeletedSet<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(DeletedSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(UpsertedSet<,,>).MakeGenericType(
                                        store_t,
                                        entity_t,
                                        dto_t
                                    )
                                ),
                                typeof(UpsertedSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(UpdatedSet<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(UpdatedSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(CreatedSet<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(CreatedSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddScoped(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(ChangedSet<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(ChangedSetHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddTransient(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(Changed<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(ChangedHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(Created<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(CreatedHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(Deleted<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(DeletedHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                            service.AddTransient(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(Upserted<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(UpsertedHandler<,,>).MakeGenericType(
                                    store_t,
                                    entity_t,
                                    dto_t
                                )
                            );
                            service.AddTransient(
                                typeof(INotificationHandler<>).MakeGenericType(
                                    typeof(Updated<,,>).MakeGenericType(store_t, entity_t, dto_t)
                                ),
                                typeof(UpdatedHandler<,,>).MakeGenericType(store_t, entity_t, dto_t)
                            );
                        }
                    }
                }
            }
        }
        return this;
    }
}
