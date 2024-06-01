using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Rubrics.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InvokeAttribute : RubricAttribute
    {
        public string Target;

        public Type Type;

        public string Method;

        public Invoker Invoker;

        public InvokeAttribute() { }

        public InvokeAttribute(string targetName, string method)
        {
            Target = targetName;
            Type = Assemblies.FindType(targetName);
            if (Type == null)
                Type = Assemblies.FindTypeByFullName(targetName);
            Method = method;
            if (Type != null)
                Invoker = new Invoker(Type, Method);
        }

        public InvokeAttribute(Type targetType, string method)
        {
            Type = targetType;
            Method = method;
            Target = targetType.FullName;
            Invoker = new Invoker(Type, Method);
        }
    }
}
