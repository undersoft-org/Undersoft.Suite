using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Workflows
{
    public class Work<T> : WorkItem
    {
        public Work(Func<T, string> method) : base(new Invoker<T>(method)) { }
    }
}
