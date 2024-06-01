using System.Reflection;
using System.Threading.Tasks;

namespace Undersoft.SDK.Invoking
{
    using Undersoft.SDK.Instant;
    using Uniques;

    public interface IInvoker : IInvokable
    {
        object TargetObject { get; set; }

        ParameterInfo[] Parameters { get; set; }

        InvokerDelegate MethodInvoker { get; }

        Delegate Method { get; }

        Task Fire(params object[] parameters);
        Task Fire(bool firstAsTarget, object target, params object[] parameters);

        object Invoke(params object[] parameters);
        object Invoke(bool firstAsTarget, object target, params object[] parameters);

        Task<object> InvokeAsync(params object[] parameters);
        Task<object> InvokeAsync(bool firstAsTarget, object target, params object[] parameters);

        Task<T> InvokeAsync<T>(params object[] parameters) where T : class;
        Task<T> InvokeAsync<T>(bool firstAsTarget, object target, params object[] parameters) where T : class;

        Task<object> InvokeAsync(Arguments arguments);

        Task<object> InvokeAsync(bool withTarget, object target, Arguments arguments);
    }
}
