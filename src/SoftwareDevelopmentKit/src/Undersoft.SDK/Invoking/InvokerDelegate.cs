using System.Reflection;
using System.Reflection.Emit;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;

namespace Undersoft.SDK.Invoking
{
    public delegate object InvokerDelegate(object target, params object[] parameters);

    public delegate R InvokerDelegate<T, R>(T target, params object[] parameters);
}
