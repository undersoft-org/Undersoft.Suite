using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service;


public partial class ServiceRegistry
{
    public ServiceObject<T> EnsureGetObject<T>() where T : class
    {
        if (ContainsKey(typeof(ServiceObject<T>)))
        {
            return (ServiceObject<T>)Get<ServiceObject<T>>()?.ImplementationInstance;
        }

        return AddObject<T>();
    }

    public ServiceObject EnsureGetObject(Type type)
    {
        Type accessorType = typeof(ServiceObject<>).MakeGenericType(type);
        if (ContainsKey(accessorType))
        {
            return (ServiceObject)Get(accessorType)?.ImplementationInstance;
        }

        return AddObject(type);
    }

    public ServiceObject<T> AddKeyedObject<T>(object key) where T : class
    {
        return AddKeyedObject(key, new ServiceObject<T>());
    }

    public ServiceObject<T> AddObject<T>() where T : class
    {
        return AddObject(new ServiceObject<T>());
    }

    public ServiceObject AddKeyedObject(object key, Type type)
    {
        return this.AddObject(type, null);
    }

    public ServiceObject AddObject(Type type)
    {
        return this.AddObject(type, null);
    }

    public ServiceObject AddObject(Type type, object obj)
    {
        Type oaType = typeof(ServiceObject<>).MakeGenericType(type);
        Type iaType = typeof(IServiceObject<>).MakeGenericType(type);

        ServiceObject accessor = (ServiceObject)oaType.New(obj);

        if (ContainsKey(oaType))
        {
            return accessor;
        }

        this.Put(ServiceDescriptor.Singleton(oaType, accessor));
        this.Put(ServiceDescriptor.Singleton(iaType, accessor));

        if (obj != null)
            this.AddSingleton(type, obj);

        return accessor;
    }

    public ServiceObject AddKeyedObject(object key, Type type, object obj)
    {
        Type oaType = typeof(ServiceObject<>).MakeGenericType(type);
        Type iaType = typeof(IServiceObject<>).MakeGenericType(type);

        ServiceObject accessor = (ServiceObject)oaType.New(obj);

        if (!ContainsKey(key))
        {
            this.Put(key.UniqueKey64(type.UniqueKey64()), ServiceDescriptor.KeyedSingleton(oaType, key, accessor));
            this.Put(key.UniqueKey64(type.UniqueKey64()), ServiceDescriptor.KeyedSingleton(iaType, key, accessor));
        }

        if (ContainsKey(oaType))
        {
            return accessor;
        }

        this.Put(ServiceDescriptor.Singleton(oaType, accessor));
        this.Put(ServiceDescriptor.Singleton(iaType, accessor));

        if (obj != null)
            this.AddSingleton(type, obj);

        return accessor;
    }

    public ServiceObject<T> AddObject<T>(T obj) where T : class
    {
        return AddObject(new ServiceObject<T>(obj));
    }

    public ServiceObject<T> AddKeyedObject<T>(object key, T obj) where T : class
    {
        return AddKeyedObject<T>(key, new ServiceObject<T>(obj));
    }

    public ServiceObject<T> AddObject<T>(ServiceObject<T> accessor) where T : class
    {
        if (ContainsKey(typeof(ServiceObject<T>)))
        {
            return accessor;
        }

        this.Put(ServiceDescriptor.Singleton(typeof(ServiceObject<T>), accessor));
        this.Put(ServiceDescriptor.Singleton(typeof(IServiceObject<T>), accessor));

        if (accessor.Value != null)
            this.AddSingleton<T>(accessor.Value);

        return accessor;
    }

    public ServiceObject<T> AddKeyedObject<T>(object key, ServiceObject<T> accessor) where T : class
    {
        if (!ContainsKey(key))
        {
            this.Put(ServiceDescriptor.KeyedSingleton<ServiceObject<T>>(key, accessor));
            this.Put(ServiceDescriptor.KeyedSingleton<IServiceObject<T>>(key, accessor));
        }
        if (accessor.Value != null)
            this.AddKeyedSingleton<T>(key, accessor.Value);

        return accessor;
    }

    public object GetObject(Type type)
    {
        Type accessorType = typeof(ServiceObject<>).MakeGenericType(type);
        return ((ServiceObject)GetSingleton(accessorType))?.Value;
    }

    public T GetObject<T>()
        where T : class
    {
        return GetSingleton<ServiceObject<T>>()?.Value;
    }

    public T GetKeyedObject<T>(object key)
    where T : class
    {
        return GetKeyedSingleton<ServiceObject<T>>(key)?.Value;
    }

    public T GetRequiredObject<T>()
        where T : class
    {
        return GetObject<T>() ?? throw new Exception($"Could not find an object of {typeof(T).AssemblyQualifiedName} in  Be sure that you have used AddObjectAccessor before!");
    }
}