using Quartz;
using Undersoft.GCC.Infrastructure.Currencies;
using Undersoft.SDK.Workflows;

namespace Undersoft.GCC.Service.API.Workflows
{
    public interface IUpsertWorkflow<T> : IJob where T : CurrenciesContext
    {
        void ConfigureFlow(Workflow workflow);
        void ConfigureWork(Workflow workflow);
        Task Execute(IJobExecutionContext context);
        void Start(T target, Func<T, Delegate> method, params object[] args);
    }
}