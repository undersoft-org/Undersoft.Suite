using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Undersoft.SDK.Service
{
    using Configuration;
    using Undersoft.SDK.Service.Data.Repository;
    using Undersoft.SDK.Service.Hosting;
    using Undersoft.SDK.Utilities;

    public class ServiceManager : RepositoryManager, IServiceManager, IAsyncDisposable
    {
        private new bool disposedValue;

        private static IServiceRegistry rootRegistry;
        private static IServiceConfiguration rootConfiguration;

        private IServiceRegistry registry;
        private IServiceConfiguration configuration;

        protected IServiceScope session;
        protected IServiceProvider provider;

        public IServiceProvider RootProvider => GetRootProvider();

        public IServiceProvider Provider => provider ??= GetProvider();
        public IServiceScope Session => session ??= GetSession();

        static ServiceManager()
        {
            var sm = new ServiceManager(new ServiceCollection());
            rootRegistry = sm.Registry;
            rootConfiguration = sm.Configuration;
        }

        public IServiceConfiguration Configuration
        {
            get => configuration;
            set => configuration = value;
        }
        public IServiceRegistry Registry => registry;

        public ServiceManager() : base()
        {
            Manager = this;
        }

        public ServiceManager(IServiceManager serviceManager) : base()
        {
            Manager = serviceManager;
            registry = serviceManager.Registry;
            provider = serviceManager.Provider;
            configuration = serviceManager.Configuration;
        }

        internal ServiceManager(IServiceCollection services) : this()
        {
            if (registry == null)
            {
                registry = new ServiceRegistry(services, this);
                registry.MergeServices(true);
                AddObject<IServiceManager>(this);

                BuildServiceProviderFactory(registry);
            }
            else
                registry.MergeServices(services, true);

            if (configuration == null)
            {
                configuration = new ServiceConfiguration(registry);
                AddObject<IServiceConfiguration>(configuration);
            }
        }

        public virtual IServiceProviderFactory<IServiceCollection> BuildServiceProviderFactory(IServiceRegistry registry)
        {
            var factory = new ServiceManagerFactory(this);

            AddObject<IServiceRegistry>(registry);
            AddObject<IServiceCollection>(registry);
            AddObject<IServiceProviderFactory<IServiceCollection>>(factory);

            registry.Services.Replace(ServiceDescriptor.Singleton<IServiceProviderFactory<IServiceCollection>>(factory));
            registry.Services.Replace(ServiceDescriptor.Singleton<IServiceCollection>(registry));
            registry.MergeServices(true);

            return factory;
        }

        public virtual T GetRootService<T>() where T : class
        {
            var result = RootProvider.GetService<T>();
            return result;
        }

        public virtual IEnumerable<T> GetRootServices<T>() where T : class
        {
            return RootProvider.GetServices<T>();
        }

        public virtual T GetRequiredRootService<T>() where T : class
        {
            return RootProvider.GetRequiredService<T>();
        }

        public virtual object GetRootService(Type type)
        {
            return RootProvider.GetService(type);
        }

        public virtual T GetService<T>() where T : class
        {
            var result = Provider.GetService<T>();
            return result;
        }

        public virtual IEnumerable<T> GetServices<T>() where T : class
        {
            return Provider.GetServices<T>();
        }

        public virtual T GetRequiredService<T>() where T : class
        {
            return Provider.GetRequiredService<T>();
        }

        public virtual object GetService(Type type)
        {
            return Provider.GetService(type);
        }

        public virtual IEnumerable<object> GetServices(Type type)
        {
            return Provider.GetServices(type);
        }

        public Lazy<T> GetRequiredServiceLazy<T>() where T : class
        {
            return new Lazy<T>(GetRequiredService<T>, true);
        }

        public Lazy<T> GetServiceLazy<T>() where T : class
        {
            return new Lazy<T>(GetService<T>, true);
        }

        public Lazy<IEnumerable<T>> GetServicesLazy<T>() where T : class
        {
            return new Lazy<IEnumerable<T>>(GetServices<T>, true);
        }

        public virtual T GetSingleton<T>() where T : class
        {
            return GetObject<T>();
        }

        public virtual object GetSingleton(Type type)
        {
            return registry.GetObject(type);
        }

        public virtual object GetRequiredService(Type type)
        {
            return Provider.GetRequiredService(type);
        }

        public virtual T InitializeRootService<T>(params object[] parameters) where T : class
        {
            return ActivatorUtilities.CreateInstance<T>(RootProvider, parameters);
        }

        public virtual T EnsureGetRootService<T>() where T : class
        {
            return ActivatorUtilities.GetServiceOrCreateInstance<T>(RootProvider);
        }

        public static void SetProvider(IServiceProvider serviceProvider)
        {
            var _provider = serviceProvider;
            _provider.GetRequiredService<ServiceObject<IServiceProvider>>().Value = _provider;
        }

        public IServiceManager ReplaceProvider(IServiceProvider serviceProvider)
        {
            Provider.GetRequiredService<ServiceObject<IServiceProvider>>().Value = serviceProvider;
            provider = registry.GetProvider();
            return this;
        }

        public IServiceProvider BuildInternalProvider(bool withPropertyInjection = false)
        {
            SetProvider(GetRegistry().BuildServiceProviderFromFactory<IServiceCollection>());
            return provider = registry.GetProvider();
        }

        public static IServiceProvider BuildInternalRootProvider(bool withPropertyInjection = false)
        {
            SetProvider(GetRootRegistry().BuildServiceProviderFromFactory<IServiceCollection>());
            return GetRootProvider();
        }

        public static IServiceProvider GetRootProvider()
        {
            var _provider = rootRegistry.GetProvider();
            if (_provider == null)
                return BuildInternalRootProvider();
            return _provider;
        }

        public IServiceProvider AddPropertyInjection()
        {
            var _provider = GetProvider() ?? GetRegistry().BuildServiceProviderFromFactory<IServiceCollection>();

            SetProvider(_provider.AddPropertyInjection());

            return _provider;
        }

        public IServiceProvider GetProvider()
        {
            return provider ??= registry.GetProvider() ?? BuildInternalProvider();
        }

        public IServiceProviderFactory<IServiceCollection> GetProviderFactory()
        {
            return GetObject<IServiceProviderFactory<IServiceCollection>>();
        }

        public IServiceProvider CreateProviderFromFacotry()
        {
            return Registry.BuildServiceProviderFromFactory<IServiceCollection>();
        }

        public static IServiceProviderFactory<IServiceCollection> GetRootProviderFactory()
        {
            return GetRootObject<IServiceProviderFactory<IServiceCollection>>();
        }

        public ObjectFactory CreateFactory<T>(Type[] constrTypes)
        {
            return ActivatorUtilities.CreateFactory(typeof(T), constrTypes);
        }

        public ObjectFactory CreateFactory(Type instanceType, Type[] constrTypes)
        {
            return ActivatorUtilities.CreateFactory(instanceType, constrTypes);
        }

        public T Initialize<T>(params object[] besidesInjectedArguments)
        {
            return ActivatorUtilities.CreateInstance<T>(Provider, besidesInjectedArguments);
        }

        public object Initialize(Type type, params object[] besidesInjectedArguments)
        {
            return ActivatorUtilities.CreateInstance(Provider, type, besidesInjectedArguments);
        }

        public T EnsureGetService<T>()
        {
            return ActivatorUtilities.GetServiceOrCreateInstance<T>(Provider);
        }

        public object EnsureGetService<T>(Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(Provider, type);
        }

        public T GetObject<T>() where T : class
        {
            return registry.GetObject<T>();
        }

        public T AddObject<T>(T obj) where T : class
        {
            return registry.AddObject(obj).Value;
        }
        public T AddObject<T>() where T : class
        {
            return registry.AddObject(typeof(T).New<T>()).Value;
        }

        public T AddKeyedObject<T>(object key, T obj) where T : class
        {
            return registry.AddKeyedObject(key, obj).Value;
        }
        public T AddKeyedObject<T>(object key) where T : class
        {
            return registry.AddKeyedObject(key, typeof(T).New<T>()).Value;
        }

        public static T GetRootObject<T>() where T : class
        {
            return rootRegistry.GetObject<T>();
        }

        public static T AddRootObject<T>(T obj) where T : class
        {
            return rootRegistry.AddObject(obj).Value;
        }
        public static T AddRootObject<T>() where T : class
        {
            return rootRegistry.AddObject(typeof(T).New<T>()).Value;
        }

        public IServiceScope GetSession()
        {
            return CreateSession();
        }

        public IServiceScope CreateSession()
        {
            return session ??= CreateScope();
        }

        public static IServiceScope CreateRootSession()
        {
            return GetRootProvider().CreateScope();
        }

        public IServiceScope CreateScope()
        {
            return GetProvider().CreateScope();
        }

        public static IServiceManager GetRootManager()
        {
            return GetRootObject<IServiceManager>();
        }

        public IServiceManager GetManager()
        {
            return GetObject<IServiceManager>();
        }

        public static IServiceRegistry GetRootRegistry()
        {
            return rootRegistry;
        }

        public IServiceRegistry GetRegistry()
        {
            return registry;
        }

        public IServiceRegistry GetRegistry(IServiceCollection services)
        {
            return registry ??= new ServiceManager(services).Registry;
        }

        public static IServiceConfiguration GetRootConfiguration()
        {
            return rootConfiguration;
        }

        public IServiceConfiguration GetConfiguration()
        {
            return configuration;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (session != null)
                        session.Dispose();
                }
                disposedValue = true;
            }
        }

        public override async ValueTask DisposeAsyncCore()
        {
            await new ValueTask(Task.Run(() =>
            {
                if (session != null)
                    session.Dispose();

            }));
        }

        public async Task LoadDataServiceModels()
        {
            await Task.WhenAll(GetClients().ForEach(client => client.BuildMetadata()).Commit());

            Registry.AddOpenDataRemoteImplementations();
        }

        public async Task<ServiceManager> UseServiceClients(bool buildProvider = false)
        {
            await LoadDataServiceModels();

            if (buildProvider)
                BuildInternalProvider();

            return this;
        }

        public T GetKeyedObject<T>(object key) where T : class
        {
            return registry.GetKeyedObject<T>(key);
        }

        public T GetKeyedService<T>(object key) where T : class
        {
            return registry.GetKeyedService<T>(key);
        }

        public T GetKeyedSingleton<T>(object key) where T : class
        {
            return registry.GetKeyedSingleton<ServiceObject<T>>(key)?.Value;
        }
    }
}
