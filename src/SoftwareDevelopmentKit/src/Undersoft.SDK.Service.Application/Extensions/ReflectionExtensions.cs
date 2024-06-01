using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable

namespace Undersoft.SDK.Service.Application.Extensions;

public static class ReflectionExtensions
{
    public static Dictionary<Type, string> KnownTypeNames
        => _knownTypeNames ?? (_knownTypeNames = CreateKnownTypeNamesDictionary());

    private static Dictionary<Type, string> _knownTypeNames;

    public static Dictionary<Type, string> CreateKnownTypeNamesDictionary()
    {
        return new Dictionary<Type, string>()
        {
            {typeof(DateTime), "DateTime"},
            {typeof(double), "double"},
            {typeof(float), "float"},
            {typeof(decimal), "decimal"},
            {typeof(sbyte), "sbyte"},
            {typeof(byte), "byte"},
            {typeof(char), "char"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(bool), "bool"},

            {typeof(void), "void"},

            {typeof(string), "string" }
        };
    }

    public static bool IsNullable(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    public static string ToNameString(this Type type, Func<Type, string> typeNameConverter = null)
    {
        return type.ToNameString(null, typeNameConverter == null ? null : (t, _) => typeNameConverter(t));
    }

    public static string ToNameString(this Type type, Func<Type, Queue<string>, string> typeNameConverter,
        bool invokeTypeNameConverterForGenericType = false)
    {
        return type.ToNameString(null, typeNameConverter, invokeTypeNameConverterForGenericType);
    }

    public static string ToParametersString(this MethodBase methodInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        var parameters = methodInfo.GetParameters();
        if (parameters.Length == 0)
        {
            return "()";
        }

        return "(" + string.Join(", ", parameters.Select(p => p.ToTypeNameString(typeNameConverter, invokeTypeNameConverterForGenericType) + " " + p.Name)) + ")";
    }

    public static string ToTypeNameString(this ParameterInfo parameterInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        if (parameterInfo.ParameterType.IsByRef)
        {
            return (parameterInfo.IsIn ? "in " : parameterInfo.IsOut ? "out " : "ref ") +
                parameterInfo.ParameterType.GetElementType().ToNameStringWithValueTupleNames(
                parameterInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
                invokeTypeNameConverterForGenericType);
        }
        return
            parameterInfo.ParameterType.ToNameStringWithValueTupleNames(
            parameterInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
            invokeTypeNameConverterForGenericType);
    }

    public static string ToTypeNameString(this MethodInfo methodInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        return methodInfo.ReturnType.ToNameStringWithValueTupleNames(
            methodInfo.ReturnParameter?.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
            invokeTypeNameConverterForGenericType);
    }

    public static string ToTypeNameString(this PropertyInfo propertyInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        return propertyInfo.PropertyType.ToNameStringWithValueTupleNames(
            propertyInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
            invokeTypeNameConverterForGenericType);
    }

    public static string ToTypeNameString(this FieldInfo fieldInfo, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        return fieldInfo.FieldType.ToNameStringWithValueTupleNames(
            fieldInfo.GetCustomAttribute<TupleElementNamesAttribute>()?.TransformNames, typeNameConverter,
            invokeTypeNameConverterForGenericType);
    }

    public static string ToNameStringWithValueTupleNames(this Type type, IList<string> tupleNames, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        var tq = tupleNames == null ? null : new Queue<string>(tupleNames);
        return type.ToNameString(tq, typeNameConverter, invokeTypeNameConverterForGenericType);
    }

    public static string ToNameString(this Type type, Queue<string> tupleFieldNames, Func<Type, Queue<string>, string> typeNameConverter = null,
        bool invokeTypeNameConverterForGenericType = false)
    {
        if (type.IsByRef)
        {
            return "ref " +
                type.GetElementType().ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType);
        }

        var decoratedTypeName = type.IsGenericType ? null : typeNameConverter?.Invoke(type, tupleFieldNames);

        if (decoratedTypeName != null &&
            (tupleFieldNames == null || tupleFieldNames.Count == 0))
        {
            return decoratedTypeName;
        }

        string newTypeName = null;
        if (KnownTypeNames.ContainsKey(type))
        {
            newTypeName = KnownTypeNames[type];
        }
        else if (type.IsNullable())
        {
            newTypeName = type.GenericTypeArguments[0].ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType) + "?";
        }
        else if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (GenericTuples.Contains(genericTypeDefinition))
            {
                var tupleFields = type.GetGenericArguments().Select((arg) => (argumentType: arg, argumentName: tupleFieldNames?.Dequeue())).ToList();
                newTypeName = "(" +
                       string.Join(", ", tupleFields
                           .Select(arg => arg.argumentType.ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType) +
                           (arg.argumentName == null ? string.Empty : " " + arg.argumentName))) + ")";
            }
            else if (type.Name.Contains('`'))
            {
                var genericTypeName = invokeTypeNameConverterForGenericType ?
                    typeNameConverter?.Invoke(genericTypeDefinition, tupleFieldNames) : null;
                newTypeName =
                    (genericTypeName ?? type.Name.CleanGenericTypeName()) + "<" +
                    string.Join(", ", type.GetGenericArguments()
                       .Select(argType => argType.ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType))) + ">";
            }
            else
            {
                newTypeName = type.Name;
            }
        }
        else if (type.IsArray)
        {
            newTypeName = type.GetElementType().ToNameString(tupleFieldNames, typeNameConverter, invokeTypeNameConverterForGenericType) +
                   "[" +
                   (type.GetArrayRank() > 1 ? new string(',', type.GetArrayRank() - 1) : string.Empty) +
                   "]";
        }
        else
        {
            newTypeName = type.Name;
        }

        return decoratedTypeName ?? newTypeName;
    }

    private static readonly HashSet<Type> GenericTuples = new(new Type[] {
        typeof(ValueTuple<>),
        typeof(ValueTuple<,>),
        typeof(ValueTuple<,,>),
        typeof(ValueTuple<,,,>),
        typeof(ValueTuple<,,,,>),
        typeof(ValueTuple<,,,,,>),
        typeof(ValueTuple<,,,,,,>),
        typeof(ValueTuple<,,,,,,,>) });

    public static string CleanGenericTypeName(this string genericTypeName)
    {
        var index = genericTypeName.IndexOf('`');
        return index < 0 ? genericTypeName : genericTypeName.Substring(0, index);
    }
}
