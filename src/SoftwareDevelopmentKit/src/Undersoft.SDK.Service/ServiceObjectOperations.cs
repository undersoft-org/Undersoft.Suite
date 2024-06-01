using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service;


public partial class ServiceRegistry
{
    public ServiceObject<T> TryAddObject<T>() where T : class
    {
        if (ContainsKey(typeof(ServiceObject<T>)))
        {
            return (ServiceObject<T>)Get<ServiceObject<T>>()?.ImplementationInstance;
        }

        return AddObject<T>();
    }

    public ServiceObject TryAddObject(Type type)
    {
        Type accessorType = typeof(ServiceObject<>).MakeGenericType(type);
        if (ContainsKey(accessorType))
        {
            return (ServiceObject)Get(accessorType)?.ImplementationInstance;
        }

        return AddObject(type);
    }

    public ServiceObject<T> AddObject<T>() where T : class
    {
        return AddObject(new ServiceObject<T>());
    }

    public ServiceObject AddObject(Type type)
    {
        Type oaType = typeof(ServiceObject<>).MakeGenericType(type);
        Type iaType = typeof(IServiceObject<>).MakeGenericType(type);

        ServiceObject accessor = (ServiceObject)oaType.New();

        if (ContainsKey(oaType))
        {
            return accessor;
        }

        Put(ServiceDescriptor.Singleton(oaType, accessor));
        Put(ServiceDescriptor.Singleton(iaType, accessor));

        return accessor;
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

    public ServiceObject<T> AddObject<T>(T obj) where T : class
    {
        return AddObject(new ServiceObject<T>(obj));
    }

    public ServiceObject<T> AddObject<T>(ServiceObject<T> accessor) where T : class
    {
        if (ContainsKey(typeof(ServiceObject<T>)))
        {
            return accessor;
        }

        Put(ServiceDescriptor.Singleton(typeof(ServiceObject<T>), accessor));
        Put(ServiceDescriptor.Singleton(typeof(IServiceObject<T>), accessor));

        if (accessor.Value != null)
            this.AddSingleton<T>(accessor.Value);

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

    public T GetRequiredObject<T>()
        where T : class
    {
        return GetObject<T>() ?? throw new Exception($"Could not find an object of {typeof(T).AssemblyQualifiedName} in  Be sure that you have used AddObjectAccessor before!");
    }
}