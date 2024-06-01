namespace Undersoft.SDK.Workflows
{
    public interface IWorkspace
    {
        void Close(bool SafeClose);

        WorkAspect Allocate(int antcount = 1);

        void Run(WorkItem labor);

        void Reset(int antcount = 1);
    }
}
