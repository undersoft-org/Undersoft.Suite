namespace Undersoft.SDK.Utilities
{
    public static class InstanceUtilities
    {
        public static object New(string fullyQualifiedName)
        {
            Type type = Type.GetType(fullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(fullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }

            return null;
        }

        public static object New(string fullyQualifiedName, params object[] constructorParams)
        {
            Type type = Type.GetType(fullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type, constructorParams);

            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(fullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type, constructorParams);
            }

            return null;
        }

        public static object New(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public static object New(Type type, params object[] ctorArguments)
        {
            return Activator.CreateInstance(type, ctorArguments);
        }

        public static T New<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static T New<T>(params object[] ctorArguments)
        {
            return (T)Activator.CreateInstance(typeof(T), ctorArguments);
        }
    }

    public static class InstanceExtensions
    {
        public static object New(this Type type, params object[] ctorArguments)
        {
            return InstanceUtilities.New(type, ctorArguments);
        }

        public static object New(this Type type)
        {
            return InstanceUtilities.New(type);
        }

        public static T New<T>(this Type type, params object[] ctorArguments)
        {
            return (T)InstanceUtilities.New(type, ctorArguments);
        }

        public static T New<T>(this Type type)
        {
            return (T)InstanceUtilities.New(type);
        }

        public static T New<T>(this T objectType, params object[] ctorArguments)
        {
            return InstanceUtilities.New<T>(ctorArguments);
        }

        public static T New<T>(this T objectType)
        {
            return InstanceUtilities.New<T>();
        }
    }
}
