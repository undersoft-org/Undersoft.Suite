using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Undersoft.SDK.Service
{
    public interface IServiceRegistry : IServiceCollection
    {
        ServiceDescriptor this[string name] { get; set; }
        ServiceDescriptor this[Type serviceType] { get; set; }

        IServiceManager Manager { get; }
        IServiceCollection Services { get; set; }

        ServiceObject AddObject(Type type);
        ServiceObject AddObject(Type type, object obj);
        ServiceObject<T> AddObject<T>() where T : class;
        ServiceObject<T> AddObject<T>(ServiceObject<T> accessor) where T : class;
        ServiceObject<T> AddObject<T>(T obj) where T : class;

        ServiceObject<T> AddKeyedObject<T>(object key) where T : class;
        ServiceObject AddKeyedObject(object key, Type type);
        ServiceObject<T> AddKeyedObject<T>(object key, T obj) where T : class;

        IServiceProvider BuildServiceProviderFromFactory();
        IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>([NotNull] Action<TContainerBuilder> builderAction = null);
        bool ContainsKey(Type type);
        bool ContainsKey<TService>();
        ServiceDescriptor Get(Type contextType);
        ServiceDescriptor Get<TService>() where TService : class;
        long GetKey(ServiceDescriptor item);
        long GetKey(object item);
        long GetKey(Type item);
        long GetKey<T>();
        object GetObject(Type type);
        T GetObject<T>() where T : class;
        T GetKeyedObject<T>(object key) where T : class;
        IServiceProvider GetProvider();
        T GetRequiredObject<T>() where T : class;
        object GetRequiredService(Type type);
        T GetRequiredService<T>() where T : class;
        Lazy<object> GetRequiredServiceLazy(Type type);
        Lazy<T> GetRequiredServiceLazy<T>() where T : class;
        T GetRequiredSingleton<T>() where T : class;
        Lazy<object> GetServiceLazy(Type type);
        Lazy<T> GetServiceLazy<T>() where T : class;
        object GetSingleton(Type type);
        T GetSingleton<T>() where T : class;
        T GetKeyedSingleton<T>(object key) where T : class;
        T GetKeyedService<T>(object key) where T : class;
        bool IsAdded(Type type);
        bool IsAdded<T>() where T : class;
        IServiceRegistry ReplaceServices(IServiceCollection services);
        void MergeServices(bool actualizeExternalServices = true);
        void MergeServices(IServiceCollection services, bool actualizeExternalServices = true);
        bool Remove<TContext>() where TContext : class;
        ISeriesItem<ServiceDescriptor> Set(ServiceDescriptor descriptor);
        bool TryAdd(ServiceDescriptor profile);
        ServiceObject EnsureGetObject(Type type);
        ServiceObject<T> EnsureGetObject<T>() where T : class;
        bool TryGet<TService>(out ServiceDescriptor profile) where TService : class;
    }
}