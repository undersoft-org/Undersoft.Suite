using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Configuration;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service
{
    public interface IServiceManager : IRepositoryManager, IServiceProvider
    {
        IServiceConfiguration Configuration { get; set; }
        IServiceProvider Provider { get; }
        IServiceRegistry Registry { get; }
        IServiceProvider RootProvider { get; }
        IServiceScope Session { get; }

        T AddObject<T>() where T : class;
        T AddKeyedObject<T>(object key) where T : class;
        T AddKeyedObject<T>(object key, T item) where T : class;
        T AddObject<T>(T obj) where T : class;
        IServiceProvider AddPropertyInjection();
        IServiceProvider BuildInternalProvider(bool withPropertyInjection = false);
        IServiceProviderFactory<IServiceCollection> BuildServiceProviderFactory(IServiceRegistry registry);
        T EnsureGetRootService<T>() where T : class;
        IServiceConfiguration GetConfiguration();
        T GetObject<T>() where T : class;
        T GetKeyedObject<T>(object key) where T : class;
        T GetKeyedService<T>(object key) where T : class;
        T GetKeyedSingleton<T>(object key) where T : class;
        IServiceManager ReplaceProvider(IServiceProvider serviceProvider);
        IServiceProvider GetProvider();
        IServiceProviderFactory<IServiceCollection> GetProviderFactory();
        IServiceRegistry GetRegistry();
        IServiceRegistry GetRegistry(IServiceCollection services);
        T GetRequiredRootService<T>() where T : class;
        object GetRequiredService(Type type);
        T GetRequiredService<T>() where T : class;
        Lazy<T> GetRequiredServiceLazy<T>() where T : class;
        object GetRootService(Type type);
        T GetRootService<T>() where T : class;
        IEnumerable<T> GetRootServices<T>() where T : class;
        T GetService<T>() where T : class;
        Lazy<T> GetServiceLazy<T>() where T : class;
        IEnumerable<object> GetServices(Type type);
        IEnumerable<T> GetServices<T>() where T : class;
        Lazy<IEnumerable<T>> GetServicesLazy<T>() where T : class;
        IServiceScope GetSession();
        IServiceScope CreateScope();
        IServiceManager GetManager();
        object GetSingleton(Type type);
        T GetSingleton<T>() where T : class;
        ObjectFactory CreateFactory(Type instanceType, Type[] constrTypes);
        ObjectFactory CreateFactory<T>(Type[] constrTypes);
        T InitializeRootService<T>(params object[] parameters) where T : class;
        IServiceScope CreateSession();
        T Initialize<T>(params object[] besidesInjectedArguments);
        object Initialize(Type type, params object[] besidesInjectedArguments);
        T EnsureGetService<T>();
        object EnsureGetService<T>(Type type);
        Task LoadDataServiceModels();
        Task<ServiceManager> UseServiceClients(bool buildProvider = false);

    }
}