namespace Undersoft.SDK.Service
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Undersoft.SDK.Logging;

    public partial class ServiceRegistry
    {
        public IServiceProvider BuildServiceProviderFromFactory()
        {
            foreach (var service in Services)
            {
                var factoryInterface = service.ImplementationInstance
                    ?.GetType()
                    .GetTypeInfo()
                    .GetInterfaces()
                    .FirstOrDefault(
                        i =>
                            i.GetTypeInfo().IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>)
                    );

                if (factoryInterface == null)
                {
                    continue;
                }

                var containerBuilderType = factoryInterface.GenericTypeArguments[0];
                return (IServiceProvider)
                    typeof(ServiceRegistry)
                        .GetTypeInfo()
                        .GetMethods()
                        .Single(
                            m =>
                                m.Name == nameof(BuildServiceProviderFromFactory)
                                && m.IsGenericMethod
                        )
                        .MakeGenericMethod(containerBuilderType)
                        .Invoke(null, new object[] { this, null });
            }

            return this.BuildServiceProvider();
        }

        public IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>(
            [NotNull] Action<TContainerBuilder> builderAction = null
        )
        {
            var serviceProviderFactory = GetSingleton<IServiceProviderFactory<TContainerBuilder>>();
            if (serviceProviderFactory == null)
            {
                Log.Failure<Datalog, Exception>(
                    null,
                    $"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {this}."
                );
            }

            var builder = serviceProviderFactory.CreateBuilder(this);
            builderAction?.Invoke(builder);
            return serviceProviderFactory.CreateServiceProvider(builder);
        }

        public T GetRequiredService<T>() where T : class
        {
            return GetSingleton<IServiceManager>().Provider.GetRequiredService<T>();
        }

        public object GetRequiredService(Type type)
        {
            return GetSingleton<IServiceManager>().Provider.GetRequiredService(type);
        }

        public Lazy<T> GetRequiredServiceLazy<T>() where T : class
        {
            return new Lazy<T>(GetRequiredService<T>, true);
        }

        public Lazy<object> GetRequiredServiceLazy(Type type)
        {
            return new Lazy<object>(() => GetRequiredService(type), true);
        }

        public Lazy<T> GetServiceLazy<T>() where T : class
        {
            return new Lazy<T>(GetService<T>, true);
        }

        public Lazy<object> GetServiceLazy(Type type)
        {
            return new Lazy<object>(() => GetService(type), true);
        }

        public IServiceProvider GetProvider()
        {
            var a = (ServiceObject<IServiceProvider>)(Get<ServiceObject<IServiceProvider>>()?.ImplementationInstance);
            return a.Value;
        }

        public T GetRequiredSingleton<T>() where T : class
        {
            var service = GetSingleton<T>();
            if (service == null)
            {
                throw new InvalidOperationException(
                    "Could not find singleton service: " + typeof(T).AssemblyQualifiedName
                );
            }

            return service;
        }

        public T GetSingleton<T>() where T : class
        {
            return (T)Get<T>()?.ImplementationInstance;
        }

        public T GetKeyedSingleton<T>(object key) where T : class
        {
            return (T)Get(key.UniqueKey64(typeof(T).UniqueKey64()))?.KeyedImplementationInstance;
        }

        public object GetSingleton(Type type)
        {
            return Get(type)?.ImplementationInstance;
        }

        public bool IsAdded(object key)
        {
            return ContainsKey(key);
        }

        public bool IsAdded<T>() where T : class
        {
            return ContainsKey<T>();
        }

        public bool IsAdded(Type type)
        {
            return ContainsKey(type);
        }

        public T GetService<T>() where T : class
        {
            return GetSingleton<IServiceManager>().Provider.GetService<T>();
        }

        public T GetKeyedService<T>(object key) where T : class
        {
            return GetSingleton<IServiceManager>().Provider.GetKeyedService<T>(key);
        }

        public object GetService(Type type)
        {
            return GetSingleton<IServiceManager>().Provider.GetService(type);
        }
    }
}
