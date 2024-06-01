using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using System.Reflection;
using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service
{
    public static class ServiceProviderExtensions
    {
        public static IServiceProvider AddPropertyInjection(this IServiceProvider provider)
        {
            var field = typeof(ServiceProvider).GetField(
                "_createServiceAccessor",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            var accessor = (Delegate)field.GetValue(provider);
            var newAccessor = (Type type) =>
            {
                Func<object, object> newFunc = (scope) =>
                {
                    var resolver = (Delegate)accessor.DynamicInvoke(new[] { type });
                    var resolved = resolver.DynamicInvoke(new[] { scope });
                    InjectProperties(provider, type, resolved);
                    return resolved;
                };
                return newFunc;
            };
            field.SetValue(provider, newAccessor);
            return provider;
        }

        private static void InjectProperties(IServiceProvider provider, Type type, object service)
        {
            if (service is null)
                return;
            service
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<InjectPropertyAttribute>() != null)
                .ForEach(prop => prop.SetValue(service, provider.GetService(prop.PropertyType)));
        }

        public static async Task LoadDataServiceModels(this IServiceProvider provider)
        {
            var sm = provider.GetService<IServiceManager>();

            await Task.Run((Action)(() =>
            {
                Task.WaitAll(sm.GetClients().ForEach((Func<IRepositoryClient, Task<IEdmModel>>)((client) =>
                {
                    return (Task<IEdmModel>)client.BuildMetadata();
                })).Commit());

                sm.Registry.AddOpenDataRemoteImplementations();
            }));
        }

        public static async Task<IServiceProvider> UseServiceClients(this IServiceProvider provider, int delayInSeconds = 0)
        {
            if (delayInSeconds > 0)
                await Task.Delay(delayInSeconds * 1000);
            await provider.LoadDataServiceModels();
            var sm = provider.GetService<IServiceManager>();
            sm.Registry.MergeServices(true);
            sm.BuildInternalProvider();
            return provider;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectPropertyAttribute : Attribute { }
}
