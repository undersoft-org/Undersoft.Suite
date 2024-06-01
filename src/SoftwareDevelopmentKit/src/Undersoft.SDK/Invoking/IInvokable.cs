using System.Reflection;

namespace Undersoft.SDK.Invoking
{
    using Undersoft.SDK.Instant;

    public interface IInvokable : IIdentifiable, IValueArray
    {
        string Name { get; set; }

        string QualifiedName { get; set; }

        string MethodName { get; }

        AssemblyName AssemblyName { get; }

        string TypeName { get; }

        Type Type { get; }

        MethodInfo Info { get; set; }

        Arguments Arguments { get; set; }

        Type ReturnType { get; }
    }
}
