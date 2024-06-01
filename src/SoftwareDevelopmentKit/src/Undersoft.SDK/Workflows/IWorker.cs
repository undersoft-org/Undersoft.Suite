using Undersoft.SDK.Invoking;

namespace Undersoft.SDK.Workflows
{
    using Notes;

    public interface IWorker
    {
        object GetInput();
        void SetInput(object value);

        object GetOutput();
        void SetOutput(object value);

        WorkNoteEvokers Evokers { get; set; }

        string Name { get; set; }

        IInvoker Process { get; set; }

        WorkAspect FlowTo<T>(string methodName = null);

        WorkAspect FlowTo<T>(Func<T, Delegate> methodName) where T : class, new();

        WorkAspect FlowTo(WorkItem Recipient);

        WorkAspect FlowTo(WorkItem Recipient, params WorkItem[] RelationWorks);

        WorkAspect FlowTo(string RecipientName, string methodName);

        WorkAspect FlowTo(string RecipientName, params string[] RelationNames);

        WorkAspect FlowFrom<T>(string methodName = null);

        WorkAspect FlowFrom<T>(Func<T, Delegate> methodName) where T : class, new();

        WorkAspect FlowFrom(WorkItem Recipient);

        WorkAspect FlowFrom(WorkItem Recipient, params WorkItem[] RelationWorks);

        WorkAspect FlowFrom(string RecipientName, string methodName);

        WorkAspect FlowFrom(string RecipientName, params string[] RelationNames);
    }
}
