namespace Undersoft.SDK.Utilities
{
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Undersoft.SDK.Plugging;

    public static partial class AssemblyUtilities
    {
        public static string AssemblyCode
        {
            get
            {
                object[] attributes;

                var entryAssembly = Assembly.GetEntryAssembly();
                if (entryAssembly is null)
                    attributes = Assembly
                        .GetCallingAssembly()
                        .GetCustomAttributes(typeof(GuidAttribute), false);
                else
                    attributes = entryAssembly.GetCustomAttributes(typeof(GuidAttribute), false);

                if (attributes.Length == 0)
                    return string.Empty;

                return ((GuidAttribute)attributes[0]).Value.ToUpper();
            }
        }

        public static Type ForceFindType(string name, string nameSpace = null)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var namespaceFirstBlock = AppDomain.CurrentDomain.FriendlyName.FirstDelimited('.');
            var plugin = new Plugin();
            foreach (var asm in asms)
            {
                if (asm.IsDynamic)
                    continue;

                foreach (var refassembly in asm.GetReferencedAssemblies())
                {
                    var types = plugin.LoadFromAssemblyName(refassembly)?.GetExportedTypes();
                    if (types != null)
                        foreach (var extype in types)
                        {
                            if (
                                extype.Namespace.Contains(namespaceFirstBlock)
                                && (nameSpace == null || extype.Namespace == nameSpace)
                            )
                            {
                                if (extype.Name.Equals(name))
                                    return extype;
                            }
                        }
                }
            }
            return null;
        }

        public static Type FindTypeByFullName(string fullname)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            string namespaceFirstBlock = fullname.FirstDelimited('.');
            foreach (var asm in asms)
            {
                if (asm.IsDynamic)
                    continue;

                var extypes = asm.GetExportedTypes();

                foreach (var extype in extypes)
                {
                    if (namespaceFirstBlock == null || extype.Namespace == null || !extype.Namespace.Contains(namespaceFirstBlock) || extype.FullName == null)
                        continue;

                    if (extype.FullName.Equals(fullname))
                        return extype;
                }
            }
            return null;
        }

        public static Type FindType(string name, string nameSpace = null)
        {
            if (name.Contains('.'))
                return FindTypeByFullName(name);

            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var namespaceFirstBlock = AppDomain.CurrentDomain.FriendlyName.FirstDelimited('.');
            foreach (var asm in asms)
            {

                if (asm.IsDynamic)
                    continue;

                var extypes = asm.GetExportedTypes();

                foreach (var extype in extypes)
                {
                    if (
                        extype.Namespace.Contains(namespaceFirstBlock)
                        && (nameSpace == null || extype.Namespace == nameSpace)
                    )
                    {
                        if (extype.Name.Equals(name))
                            return extype;
                    }
                }
            }
            return null;
        }

        public static IList<Type> FindTypes(IList<Type> assignableTypes, string nameSpace = null)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var namespaceFirstBlock = AppDomain.CurrentDomain.FriendlyName.FirstDelimited('.');
            var typeList = new List<Type>();
            foreach (var asm in asms)
            {
                if (asm.IsDynamic)
                    continue;

                var extypes = asm.GetExportedTypes();

                foreach (var extype in extypes)
                {
                    if (
                        extype.Namespace.Contains(namespaceFirstBlock)
                        && (nameSpace == null || extype.Namespace == nameSpace)
                    )
                    {
                        if (assignableTypes.All(t => extype.IsAssignableTo(t)))
                            typeList.Add(extype);
                    }
                }
            }
            return typeList;
        }

        public static IList<Type> FindTypes(Type genericType, params Type[] genericArgumentTypes)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var namespaceFirstBlock = AppDomain.CurrentDomain.FriendlyName.FirstDelimited('.');
            var typeList = new List<Type>();
            foreach (var asm in asms)
            {
                if (asm.IsDynamic)
                    continue;

                var extypes = asm.GetExportedTypes();

                foreach (var extype in extypes)
                {
                    if (extype.Namespace.Contains(namespaceFirstBlock))
                    {
                        if (
                            extype.IsGenericType
                            && genericArgumentTypes == null
                            && extype.Equals(genericType)
                        )
                        {
                            typeList.Add(extype);
                        }
                        else if (
                            extype.IsGenericType
                            && extype.Equals(genericType)
                            && extype
                                .GetGenericArguments()
                                .Where((t, x) => t.Equals(genericArgumentTypes[x]))
                                .Count() == genericArgumentTypes.Length
                        )
                            typeList.Add(extype);
                    }
                }
            }
            return typeList;
        }

        public static IList<Type> FindTypes(
            Type attributeArgumentType,
            object attributeArgumentValue,
            Type attributeType = null,
            string nameSpace = null
        )
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var typeList = new List<Type>();

            foreach (var asm in asms)
            {
                Type[] types =
                    nameSpace != null
                        ? asm.GetTypes().Where(n => n.Namespace == nameSpace).ToArray()
                        : asm.GetTypes();

                if (
                    attributeType != null
                    && attributeArgumentType != null
                    && attributeArgumentValue != null
                )
                {
                    foreach (var type in types)
                        if (
                            type.GetCustomAttributesData()
                                .Where(a => a.AttributeType == attributeType)
                                .Any(
                                    s =>
                                        s.ConstructorArguments.Any(
                                            o =>
                                                o.ArgumentType == attributeArgumentType
                                                && o.Value.Equals(attributeArgumentValue)
                                        )
                                )
                        )
                            typeList.Add(type);
                }
                else if (
                    attributeType == null
                    && attributeArgumentType == null
                    && attributeArgumentValue != null
                )
                {
                    foreach (var type in types)
                        if (
                            type.GetCustomAttributesData()
                                .Any(
                                    s =>
                                        s.ConstructorArguments.Any(
                                            o => o.Value.Equals(attributeArgumentValue)
                                        )
                                )
                        )
                            typeList.Add(type);
                }
                else if (
                    attributeType != null
                    && attributeArgumentType == null
                    && attributeArgumentValue != null
                )
                {
                    foreach (var type in types)
                        if (
                            type.GetCustomAttributesData()
                                .Where(a => a.AttributeType == attributeType)
                                .Any(
                                    s =>
                                        s.ConstructorArguments.Any(
                                            o => o.Value.Equals(attributeArgumentValue)
                                        )
                                )
                        )
                            typeList.Add(type);
                }
                else if (
                    attributeType != null
                    && attributeArgumentType == null
                    && attributeArgumentValue == null
                )
                {
                    foreach (var type in types)
                        if (
                            type.GetCustomAttributesData()
                                .Where(a => a.AttributeType == attributeType)
                                .Any()
                        )
                            typeList.Add(type);
                }
                else
                    foreach (var type in types)
                        if (
                            type.GetCustomAttributesData()
                                .Any(
                                    s =>
                                        s.ConstructorArguments.Any(
                                            o =>
                                                o.ArgumentType == attributeArgumentType
                                                && o.Value.Equals(attributeArgumentValue)
                                        )
                                )
                        )
                            typeList.Add(type);
            }
            return typeList;
        }
    }
}
