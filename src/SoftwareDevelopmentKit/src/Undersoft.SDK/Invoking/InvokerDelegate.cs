namespace Undersoft.SDK.Invoking
{
    public delegate object InvokerDelegate(object target, params object[] parameters);

    public delegate R InvokerDelegate<T, R>(T target, params object[] parameters);
}
