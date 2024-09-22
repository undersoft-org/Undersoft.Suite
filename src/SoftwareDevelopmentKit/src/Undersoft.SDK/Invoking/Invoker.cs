using System.Reflection;
using Undersoft.SDK.Logging;
using Undersoft.SDK.Uniques;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Invoking
{
    public class Invoker : Origin, IInvoker
    {
        private Uscn serialcode;
        private event InvokerDelegate routine;

        public Invoker() { }

        public Invoker(Arguments args)
            : this(args.TargetType.New(), args) { }

        public Invoker(object targetObject, Arguments args)
        {
            Type t = args.TargetType;
            TargetObject = targetObject;

            var methodName = args.MethodName;

            MethodInfo m = t.GetMethods()
                .Where(a =>
                    a.Name == methodName && a.GetParameters().Length <= args.Count && a.IsPublic
                )
                .FirstOrDefault();
            if (m != null)
            {
                Info = m;
                var parameters = m.GetParameters();
                if (parameters.Length > 0)
                {
                    Arguments = args;
                    Parameters = m.GetParameters();
                    NumberOfArguments = Parameters.Length;
                }
            }
            createUniqueKey();
        }

        public Invoker(object targetObject, MethodInfo methodInvokeInfo)
        {
            TargetObject = targetObject;
            MethodInfo m = methodInvokeInfo;

            if (m.GetParameters().Any())
            {
                NumberOfArguments = m.GetParameters().Length;
                Info = m;
                Parameters = m.GetParameters();
                Arguments = new Arguments(m.Name, Parameters);
            }
            createUniqueKey();
        }

        public Invoker(Delegate targetMethod)
        {
            TargetObject = targetMethod.Target;
            Type t = TargetObject.GetType();
            MethodInfo m = targetMethod.Method;
            Info = m;
            if (m.GetParameters().Any())
            {
                NumberOfArguments = m.GetParameters().Length;
                Parameters = m.GetParameters();
                Arguments = new Arguments(m.Name, Parameters);
            }
            createUniqueKey();
        }

        public Invoker(object targetObject, string methodName, params Type[] parameterTypes)
        {
            TargetObject = targetObject;
            Type t = TargetObject.GetType();

            MethodInfo m =
                parameterTypes != null
                    ? t.GetMethod(methodName, parameterTypes)
                    : t.GetMethod(methodName);

            if (m != null)
            {
                if (m.GetParameters().Any())
                {
                    Parameters = m.GetParameters();
                    NumberOfArguments = Parameters.Length;
                    Arguments = new Arguments(methodName, Parameters, t.FullName);
                }
                Info = m;
            }
            createUniqueKey();
        }

        public Invoker(Type targetType, Type[] parameterTypes)
            : this(
                targetType
                    .GetMethods()
                    .Where(m =>
                        m.IsPublic
                        && m.GetParameters().All(p => parameterTypes.Contains(p.ParameterType))
                    )
                    .FirstOrDefault()
            ) { }

        public Invoker(
            Type targetType,
            Type[] parameterTypes,
            params object[] constructorParameters
        )
            : this(
                targetType
                    .GetMethods()
                    .FirstOrDefault(m =>
                        m.IsPublic
                        && m.GetParameters().All(p => parameterTypes.Contains(p.ParameterType))
                    ),
                constructorParameters
            ) { }

        public Invoker(Type targetType, params object[] constructorParameters)
            : this(targetType.GetMethods().FirstOrDefault(m => m.IsPublic), constructorParameters)
        { }

        public Invoker(Type targetType)
            : this(targetType.GetMethods().FirstOrDefault(m => m.IsPublic)) { }

        public Invoker(object targetObject, string methodName)
            : this(targetObject, methodName, null) { }

        public Invoker(
            object targetObject,
            string methodName,
            params object[] constructorParameters
        )
            : this(targetObject, methodName, null) { }

        public Invoker(Type targetType, string methodName)
            : this(InstanceUtilities.New(targetType), methodName, null) { }

        public Invoker(Type targetType, string methodName, params Type[] parameterTypes)
            : this(InstanceUtilities.New(targetType), methodName, parameterTypes) { }

        public Invoker(Type targetType, string methodName, params object[] constructorParams)
            : this(InstanceUtilities.New(targetType, constructorParams), methodName) { }

        public Invoker(
            Type targetType,
            string methodName,
            Type[] parameterTypes,
            params object[] constructorParams
        )
            : this(InstanceUtilities.New(targetType, constructorParams), methodName, parameterTypes)
        { }

        public Invoker(string targetName, string methodName)
            : this(InstanceUtilities.New(targetName), methodName, null) { }

        public Invoker(string targetName, string methodName, params Type[] parameterTypes)
            : this(InstanceUtilities.New(targetName), methodName, parameterTypes) { }

        public Invoker(
            string targetName,
            string methodName,
            Type[] parameterTypes,
            params object[] constructorParams
        )
            : this(InstanceUtilities.New(targetName, constructorParams), methodName, parameterTypes)
        { }

        public Invoker(MethodInfo methodInvokeInfo)
            : this(methodInvokeInfo.DeclaringType.New(), methodInvokeInfo) { }

        public Invoker(MethodInfo methodInvokeInfo, params object[] constructorParams)
            : this(methodInvokeInfo.DeclaringType.New(constructorParams), methodInvokeInfo) { }

        public Uscn Code
        {
            get => serialcode;
            set => serialcode = value;
        }

        public string Name { get; set; }

        public string MethodName => Info.Name;

        public override string TypeName
        {
            get => Type.FullName;
        }

        public string QualifiedName { get; set; }

        public object this[int fieldId]
        {
            get => (fieldId < NumberOfArguments) ? Arguments[fieldId].Deserialize() : null;
            set
            {
                if (fieldId < NumberOfArguments)
                    Arguments[fieldId].Serialize(value);
            }
        }
        public object this[string argumentName]
        {
            get
            {
                if (Arguments.TryGet(argumentName.UniqueKey(), out Argument arg))
                    return arg.Deserialize();
                return null;
            }
            set
            {
                if (Arguments.TryGet(argumentName.UniqueKey(), out Argument arg))
                    arg.Serialize(value);
            }
        }

        public InvokerDelegate MethodInvoker
        {
            get
            {
                if (routine == null)
                    routine += (InvokerDelegate)Method;
                return routine;
            }
        }

        public Object TargetObject { get; set; }

        public Delegate Method { get; set; }

        public MethodInfo Info { get; set; }

        public ParameterInfo[] Parameters { get; set; }

        public Arguments Arguments { get; set; }

        public Type ReturnType => Info.ReturnType;

        public int NumberOfArguments { get; set; }

        public StateOn StateOn { get; set; }

        public IUnique Empty => Uscn.Empty;

        public object[] ValueArray
        {
            get => Arguments.Select(a => a.Deserialize()).ToArray();
            set =>
                Arguments.ForEach(
                    (a, x) =>
                    {
                        if (a.TypeName == value[x].GetType().FullName)
                            a.Serialize(value[x]);
                    }
                );
        }

        public Type Type => TargetObject.GetType();

        public AssemblyName AssemblyName => Type.Assembly.GetName();

        public virtual Task Fire(params object[] parameters)
        {
            try
            {
                return Task.Run(
                    () =>
                        (Method ?? InvokingIL.Create(Info)).DynamicInvoke(TargetObject, parameters)
                );
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual Task Fire(bool withTarget, object target, params object[] parameters)
        {
            try
            {
                if (!withTarget)
                {
                    parameters = new[] { target }.Concat(parameters).ToArray();
                    target = TargetObject;
                }

                if (Method == null)
                {
                    Method = InvokingIL.Create(Info);
                }

                return Task.Run(() => Method.DynamicInvoke(target, parameters));
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual object Invoke(params object[] parameters)
        {
            try
            {
                if (Method == null)
                    Method = InvokingIL.Create(Info);

                var obj = Method.DynamicInvoke(TargetObject, parameters);

                return obj;
            }
            catch (Exception e)
            {
                Method.Error<Instantlog>(
                    "Target invocation of JIT compiled dynamic method delegate in IL throw unhandled exception",
                    null,
                    new TargetInvocationException(e)
                );
            }

            return default!;
        }

        public virtual object Invoke(bool withTarget, object target, params object[] parameters)
        {
            try
            {
                if (!withTarget)
                {
                    parameters = new[] { target }.Concat(parameters).ToArray();
                    target = TargetObject;
                }
                if (Method == null)
                {
                    Method = InvokingIL.Create(Info);
                }

                var obj = Method.DynamicInvoke(target, parameters);

                return obj;
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual T Invoke<T>(params object[] parameters)
        {
            try
            {
                if (Method == null)
                {
                    Method = InvokingIL.Create(Info);
                }

                var obj = Method.DynamicInvoke(TargetObject, parameters);

                return (T)obj;
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual T Invoke<T>(bool withTarget, object target, params object[] parameters)
        {
            try
            {
                if (!withTarget)
                {
                    parameters = new[] { target }.Concat(parameters).ToArray();
                    target = TargetObject;
                }
                if (Method == null)
                {
                    Method = InvokingIL.Create(Info);
                }

                var obj = Method.DynamicInvoke(target, parameters);

                return (T)obj;
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual async Task<object> InvokeAsync(params object[] parameters)
        {
            try
            {
                return await Task.Factory.StartNew(
                    () =>
                    {
                        var obj = Invoke(parameters);
                        if (obj == null)
                            return Task.CompletedTask;

                        var resultProperty = obj.GetType().GetRuntimeProperty("Result");
                        if (resultProperty != null)
                            return resultProperty.GetValue(obj);
                        else
                            return obj;
                    },
                    TaskCreationOptions.AttachedToParent
                );
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual async Task<object> InvokeAsync(
            bool withTarget,
            object target,
            params object[] parameters
        )
        {
            try
            {
                return await Task.Factory.StartNew(
                    () =>
                    {
                        if (!withTarget)
                        {
                            parameters = new[] { target }.Concat(parameters).ToArray();
                            target = TargetObject;
                        }

                        var obj = Invoke(target, parameters);

                        var resultProperty = obj.GetType().GetRuntimeProperty("Result");
                        if (resultProperty != null)
                            return resultProperty.GetValue(obj);
                        else
                            return obj;
                    },
                    TaskCreationOptions.AttachedToParent
                );
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual async Task<T> InvokeAsync<T>(params object[] parameters)
            where T : class
        {
            try
            {
                return await Task.Factory.StartNew(
                    () =>
                    {
                        var obj = Invoke(parameters);

                        return GetObjectTaskResult<T>(obj);
                    },
                    TaskCreationOptions.AttachedToParent
                );
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public virtual async Task<T> InvokeAsync<T>(
            bool withTarget,
            object target,
            params object[] parameters
        )
            where T : class
        {
            try
            {
                return await Task.Factory.StartNew(
                    () =>
                    {
                        if (!withTarget)
                        {
                            parameters = new[] { target }.Concat(parameters).ToArray();
                            target = TargetObject;
                        }

                        var obj = Invoke(target, parameters);

                        return GetObjectTaskResult<T>(obj);
                    },
                    TaskCreationOptions.AttachedToParent
                );
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        private T GetObjectTaskResult<T>(object obj)
            where T : class
        {
            var resultProperty = obj.GetType().GetRuntimeProperty("Result");
            object result = null;
            if (resultProperty != null)
                result = resultProperty.GetValue(obj);
            else
                result = obj;

            if (result.GetType() == typeof(T))
                return (T)result;

            return result.PutTo<T>();
        }

        public virtual async Task<object> InvokeAsync(Arguments arguments)
        {
            return await InvokeAsync(
                arguments
                    .ForOnly(arg => Arguments.ContainsKey(arg.Id), a => a.Deserialize())
                    .Commit()
            );
        }

        public virtual async Task<object> InvokeAsync(
            bool withTarget,
            object target,
            Arguments arguments
        )
        {
            return await InvokeAsync(
                withTarget,
                target,
                arguments
                    .ForOnly(arg => Arguments.ContainsKey(arg.Id), a => a.Deserialize())
                    .Commit()
            );
        }

        public virtual async Task<T> InvokeAsync<T>(Arguments arguments)
            where T : class
        {
            return await InvokeAsync<T>(
                arguments
                    .ForOnly(arg => Arguments.ContainsKey(arg.Id), a => a.Deserialize())
                    .Commit()
            );
        }

        public virtual async Task<T> InvokeAsync<T>(
            bool withTarget,
            object target,
            Arguments arguments
        )
            where T : class
        {
            return await InvokeAsync<T>(
                withTarget,
                target,
                arguments
                    .Where(arg => Arguments.ContainsKey(arg.Id))
                    .Select(a => a.Deserialize())
                    .ToArray()
            );
        }

        public object ConvertType(object source, Type destination)
        {
            object newobject = Convert.ChangeType(source, destination);
            return (newobject);
        }

        private void createUniqueKey()
        {
            Name = getFullName();
            QualifiedName = getQualifiedName();
            Id = QualifiedName.UniqueKey64();
            TypeId = Info.DeclaringType.UniqueKey();
            Time = DateTime.Now;
        }

        private static long getUniqueKey(MethodInfo info, ParameterInfo[] parameters)
        {
            var qualifiedName = getQualifiedName(info, parameters);
            var key = qualifiedName.UniqueKey64();
            return key;
        }

        private static long getUniqueSeed(MethodInfo info)
        {
            return info.UniqueKey();
        }

        private string getFullName()
        {
            return $"{Info.DeclaringType.FullName}." + $"{Info.Name}";
        }

        private static string getFullName(MethodInfo info)
        {
            return $"{info.DeclaringType.FullName}." + $"{info.Name}";
        }

        private string getQualifiedName()
        {
            var name = $"{Info.DeclaringType.FullName}.{Info.Name}";
            if (Parameters != null)
                name +=
                    $"({new String(Parameters.SelectMany(p => ", " + p.ParameterType.Name).ToArray())})";
            return name;
        }

        private static string getQualifiedName(MethodInfo info, ParameterInfo[] parameters)
        {
            var name = $"{info.DeclaringType.FullName}.{info.Name}";
            if (parameters != null)
                name +=
                    $"({new String(parameters.SelectMany(p => ", " + p.ParameterType.Name).ToArray())})";
            return name;
        }

        public static string GetName(Type type, string methodName, params Type[] parameterTypes)
        {
            MethodInfo m =
                parameterTypes != null
                    ? type.GetMethod(methodName, parameterTypes)
                    : type.GetMethod(methodName);
            return getFullName(m);
        }

        public static string GetName(Type type, params Type[] parameterTypes)
        {
            if (parameterTypes != null && parameterTypes.Any())
            {
                return getFullName(
                    type.GetMethods()
                        .FirstOrDefault(m =>
                            m.IsPublic
                            && m.GetParameters().All(p => parameterTypes.Contains(p.ParameterType))
                        )
                );
            }
            else
            {
                return getFullName(type.GetMethods().FirstOrDefault(m => m.IsPublic));
            }
        }

        public static string GetName<T>(string methodName, params Type[] parameterTypes)
        {
            return GetName(typeof(T), methodName, parameterTypes);
        }

        public static string GetName<T>(params Type[] parameterTypes)
        {
            return GetName(typeof(T), parameterTypes);
        }

        public static string GetQualifiedName(
            Type type,
            string methodName,
            params Type[] parameterTypes
        )
        {
            var m = type.GetMethods()
                .FirstOrDefault(m =>
                    m.GetParameters().All(p => parameterTypes.Contains(p.ParameterType))
                    && m.IsPublic
                    && m.Name == methodName
                );
            return getQualifiedName(m, m.GetParameters());
        }

        public static string GetQualifiedName(Type type, params Type[] parameterTypes)
        {
            var m = type.GetMethods()
                .FirstOrDefault(m =>
                    m.IsPublic
                    && m.GetParameters().All(p => parameterTypes.Contains(p.ParameterType))
                );
            return getQualifiedName(m, m.GetParameters());
        }

        public static string GetQualifiedName<T>(params Type[] parameterTypes)
        {
            return GetQualifiedName(typeof(T), parameterTypes);
        }

        public static string GetQualifiedName<T>(string methodName, params Type[] parameterTypes)
        {
            return GetQualifiedName(typeof(T), methodName, parameterTypes);
        }
    }

    public enum ChangeState
    {
        Unchanged,

        Adding,

        Added,

        Modifying,

        Modified,

        Deleting,

        Deleted,

        Upserting,

        Upserted,

        Patching,

        Patched,
    };

    public enum StateOn
    {
        Adding,
        AddComplete,
        Deleting,
        DeleteComplete,
        Getting,
        Patching,
        PatchComplete,
        Upsert,
        UpsertComplete,
        GetComplete,
        Setting,
        SetComplete,
        Saving,
        SaveComplete,
        Filtering,
        FilterComplete,
        Finding,
        FindComplete,
        Mapping,
        MapComplete,
        Exist,
        ExistComplete,
        NonExist,
        NonExistComplete,
        Validating,
        ValidateComplete,
    }
}
