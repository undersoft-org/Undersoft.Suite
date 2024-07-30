using System.Reflection;
using System.Runtime.InteropServices;
using Undersoft.SDK.Utilities;

namespace System
{
    public static class TypeExtensions
    {
        public static object Default(this Type type)
        {
            if (
                type == null
                || !type.IsValueType
                || type == typeof(void)
                || Nullable.GetUnderlyingType(type) != null
            )
                return null;

            if (type.IsPrimitive || type.IsPublic)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{"
                            + MethodBase.GetCurrentMethod()
                            + "} Error:\n\nThe Activator.CreateInstance method could not "
                            + "CreateInstance a default instance of the supplied value type <"
                            + type
                            + "> (Inner Exception message: \""
                            + e.Message
                            + "\")",
                        e
                    );
                }
            }

            throw new ArgumentException(
                "{"
                    + MethodBase.GetCurrentMethod()
                    + "} Error:\n\nThe supplied value type <"
                    + type
                    + "> is not a publicly-visible type, so the default value cannot be retrieved"
            );
        }

        public static object DefaultNotNullable(this Type type)
        {
            var notNullableType = type.GetNotNullableType();

            if (notNullableType.IsAssignableTo(typeof(DateTime)))
                return DateTime.Now.Date;
            else if (notNullableType.IsAssignableTo(typeof(DateOnly)))
                return DateOnly.FromDateTime(DateTime.Now.Date);
            else if (notNullableType.IsAssignableTo(typeof(DateTimeOffset)))
                return new DateTimeOffset(DateTime.Now.Date);
            else
                return notNullableType.Default();
        }

        public static object DefaultNullable(this Type type)
        {
            var nullable = false;
            var notNullableType = type;
            if (type.IsNullable())
            {
                notNullableType = type.GetNotNullableType();
                nullable = true;
            }
            object value;
            if (notNullableType.IsAssignableTo(typeof(DateTime)))
                value = DateTime.Now.Date;
            else if (notNullableType.IsAssignableTo(typeof(DateOnly)))
                value = DateOnly.FromDateTime(DateTime.Now.Date);
            else if (notNullableType.IsAssignableTo(typeof(DateTimeOffset)))
                value = new DateTimeOffset(DateTime.Now.Date);
            else
                value = notNullableType.Default();
            if (nullable)
                return typeof(Nullable<>).MakeGenericType(notNullableType).New(value);
            return value;
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type GetNotNullableType(this Type type)
        {
            if (type.IsGenericType)
            {
                var _type = type.GetGenericTypeDefinition();
                return _type == typeof(Nullable<>) ? type.GetGenericArguments()[0] : type;
            }
            return type;
        }

        public static Type GetNotNullableType<T>(this T obj)
        {
            var type = typeof(T);
            if (type.IsGenericType)
            {
                var _type = type.GetGenericTypeDefinition();
                return _type == typeof(Nullable<>) ? type.GetGenericArguments()[0] : type;
            }
            return type;
        }

        public static unsafe object DefaultHighBits(this Type type)
        {
            if (
                type == null
                || !type.IsValueType
                || type == typeof(void)
                || Nullable.GetUnderlyingType(type) != null
            )
                return null;

            if (type.IsPrimitive || type.IsPublic)
            {
                try
                {
                    if (type.IsPrimitive || (!type.IsEnum && !(type == typeof(DateTime))))
                    {
                        int size = Marshal.SizeOf(type);
                        byte* pbyte = stackalloc byte[size];
                        for (int i = 0; i < size; i++)
                        {
                            *(pbyte + i) = 0xFF;
                        }

                        return Marshal.PtrToStructure(new IntPtr(pbyte), type);
                    }

                    return Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(
                        "{"
                            + MethodBase.GetCurrentMethod()
                            + "} Error:\n\nThe Activator.CreateInstance method could not "
                            + "CreateInstance a default instance of the supplied value type <"
                            + type
                            + "> (Inner Exception message: \""
                            + e.Message
                            + "\")",
                        e
                    );
                }
            }

            throw new ArgumentException(
                "{"
                    + MethodBase.GetCurrentMethod()
                    + "} Error:\n\nThe supplied value type <"
                    + type
                    + "> is not a publicly-visible type, so the default value cannot be retrieved"
            );
        }

        public static Type GetEnumerableElementType(this Type seqType)
        {
            var enumerableType = GetEnumerableGenericType(seqType);
            return enumerableType == null ? seqType : enumerableType.GetGenericArguments()[0];
        }

        public static MethodInfo GetGenericMethod(this Type owner, string methodName)
        {
            var method = owner
                .GetMethods()
                .FirstOrDefault(m => m.Name == methodName && m.IsGenericMethod);
            return method;
        }

        private static Type GetEnumerableGenericType(this Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;

            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (seqType.IsAssignableTo(ienum))
                    {
                        return ienum;
                    }
                }
            }

            var ifaces = seqType.GetInterfaces();
            if (ifaces?.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = GetEnumerableGenericType(iface);
                    if (ienum != null)
                    {
                        return ienum;
                    }
                }
            }

            if (seqType?.BaseType != typeof(object))
            {
                return GetEnumerableGenericType(seqType.BaseType);
            }

            return null;
        }

        public static IList<Type> GetImplementedGenericTypes(this Type seqType)
        {
            return AssemblyUtilities.FindTypes(seqType);
        }

        public static IList<Type> GetAssignableTypes(this Type seqType)
        {
            return AssemblyUtilities.FindTypes(new Type[] { seqType });
        }


#if !NET5_0_OR_GREATER
        public static bool IsAssignableTo(this Type srcType, Type destType)
        {
            return destType.IsAssignableFrom(srcType);
        }
#endif
    }
}
