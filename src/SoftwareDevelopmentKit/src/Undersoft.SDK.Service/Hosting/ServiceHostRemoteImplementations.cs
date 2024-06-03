using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service.Hosting
{
    public static class ServiceHostRemoteImplementations
    {
        public static void AddOpenDataRemoteImplementations(this IServiceRegistry reg)
        {
            IServiceRegistry service = reg;
            HashSet<Type> duplicateCheck = new HashSet<Type>();

            /**************************************** Data Service Remotes *********************************************/

            foreach (ISeries<IEdmEntityType> contextEntityTypes in OpenDataRegistry.ContextEntities)
            {
                foreach (IEdmEntityType _entityType in contextEntityTypes)
                {
                    Type entityType = OpenDataRegistry.GetMappedType(_entityType.Name);
                    if (entityType != null && duplicateCheck.Add(entityType))
                    {
                        Type callerType = DataStoreRegistry.GetRemoteType(entityType.Name);
                        if (callerType != null)
                        {
                            var relationName = callerType.FullName + "__&__" + entityType.FullName;
                            var reversedName = entityType.FullName + "__&__" + callerType.FullName;

                            if (
                                OpenDataRegistry.Remotes.TryGet(
                                    relationName,
                                    out ISeriesItem<RemoteRelation> relation
                                ) || OpenDataRegistry.Remotes.TryGet(
                                    reversedName,
                                    out relation
                                )
                            )
                            {
                                service.AddObject(
                                    typeof(IRemoteRelation<,>).MakeGenericType(
                                        callerType,
                                        entityType
                                    ),
                                    relation.Value
                                );
                                //if(relation.Value.Towards == Towards.SetToSet)
                                //{
                                //    service.AddObject(
                                //    typeof(IRemoteRelation<,>).MakeGenericType(
                                //        entityType,
                                //        callerType
                                //    ),
                                //    relation.Value);
                                //}
                            }
                            /*****************************************************************************************/
                        }

                        var remoteStores = OpenDataRegistry.GetEntityStoreTypes(entityType);
                        if (remoteStores != null)
                        {
                            /*****************************************************************************************/
                            foreach (Type remoteStore in remoteStores)
                            {
                                /*****************************************************************************************/
                                service.AddScoped(
                                    typeof(IRemoteRepository<,>).MakeGenericType(remoteStore, entityType),
                                    typeof(RemoteRepository<,>).MakeGenericType(remoteStore, entityType)
                                );

                                service.AddSingleton(
                                    typeof(IEntityCache<,>).MakeGenericType(remoteStore, entityType),
                                    typeof(EntityCache<,>).MakeGenericType(remoteStore, entityType)
                                );
                                /*****************************************************************************************/
                                service.AddScoped(
                                    typeof(IRemoteSet<,>).MakeGenericType(remoteStore, entityType),
                                    typeof(RemoteSet<,>).MakeGenericType(remoteStore, entityType)
                                );

                                if (callerType != null)
                                {
                                    service.AddTransient(
                                        typeof(IRepositoryLink<,,>).MakeGenericType(
                                            remoteStore,
                                            callerType,
                                            entityType
                                        ),
                                        typeof(RepositoryLink<,,>).MakeGenericType(
                                            remoteStore,
                                            callerType,
                                            entityType
                                        )
                                    );
                                    service.AddTransient(
                                        typeof(IRemoteProperty<,>).MakeGenericType(remoteStore, callerType),
                                        typeof(RepositoryLink<,,>).MakeGenericType(
                                            remoteStore,
                                            callerType,
                                            entityType
                                        )
                                    );
                                }
                            }
                            /*********************************************************************************************/
                        }
                    }
                }
            }
            //service.Manager;
        }
    }
}
