using Castle.DynamicProxy;
using Microsoft.OData.Edm;
using Undersoft.SDK.Uniques;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Undersoft.SDK.Service.Data.Object;

using Event;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Client;

public static class DataObjectExtensions
{
    public static Type GetDataType(this Type type)
    {
        while (type.IsAssignableTo(typeof(IProxyTargetAccessor)))
            type = type.UnderlyingSystemType.BaseType;
        if (type == typeof(IEdmEntityType))
            return OpenDataRegistry.MappedTypes[((IEdmEntityType)type).FullTypeName()];
        return type;
    }

    public static Type GetDataType(this object obj)
    {
        return obj.GetType().GetDataType();
    }

    public static string GetDataName(this object obj)
    {
        return obj.GetType().GetDataType().Name;
    }

    public static string GetDataFullName(this object obj)
    {
        return obj.GetType().GetDataType().FullName;
    }

    public static string GetDataName(this Type obj)
    {
        return obj.GetDataType().Name;
    }

    public static string GetDataFullName(this Type obj)
    {
        return obj.GetDataType().FullName;
    }

    public static int GetDataTypeId(this object obj)
    {
        return obj.GetType().GetDataType().FullName.UniqueKey32();
    }

    public static int GetDataTypeId(this Type obj)
    {
        return obj.GetDataType().FullName.UniqueKey32();
    }

    public static bool IsEventType(this object obj)
    {
        return obj.GetType().IsAssignableTo(typeof(Event));
    }

    public static bool IsDataProxy(this Type t)
    {
        if (t.IsAssignableTo(typeof(IProxyTargetAccessor)))
            return true;
        if (t == typeof(IEdmEntityType))
            return true;
        if (t.IsAssignableTo(typeof(IProxy)))
            return true;
        return false;
    }
}