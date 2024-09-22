using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Workflows
{
    public class WorkItem<T> : WorkItem
    {
        public WorkItem(Func<T, string> method) : base(new Invoker<T>(method)) { }
    }
}
