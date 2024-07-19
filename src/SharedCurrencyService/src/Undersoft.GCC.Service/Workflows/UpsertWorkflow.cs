using Quartz;
using Undersoft.GCC.Infrastructure.Currencies;
using Undersoft.GCC.Service.Commands;
using Undersoft.SDK.Service;
using Undersoft.SDK.Workflows;

namespace Undersoft.GCC.Service.API.Workflows
{
    public class UpsertWorkflow<T> : Workflow<T>, IUpsertWorkflow<T> where T : CurrenciesContext
    {
        protected IServicer _servicer { get; }
        protected T _context { get; }

        public UpsertWorkflow(IServicer servicer)
        {
            _servicer = servicer;
            _context = _servicer.GetService<T>();
            ConfigureWork(this);
            ConfigureFlow(this);
        }

        public override void ConfigureWork(Workflow workflow)
        {
            workflow
                .Aspect<T>()
                    .AddWork(_context, c => c.GetAllRates)
                    .AddWork(_context, c => c.GetCurrencies)
                    .AddWork(_context, c => c.GetLatestRates)
                .Allocate(3);

            workflow
                .Aspect<UpsertCommands>()
                    .AddWork<UpsertCommands>(a => a.UpsertCurrencies, _servicer)
                    .AddWork<UpsertCommands>(a => a.UpsertAllRates, _servicer)
                    .AddWork<UpsertCommands>(a => a.UpsertLatestRates, _servicer)
                .Allocate(3);
        }

        public override void ConfigureFlow(Workflow workflow)
        {
            workflow
                .Aspect<T>()
                    .Work<T>(_context, w => w.GetCurrencies)
                        .FlowTo<UpsertCommands>(a => a.UpsertCurrencies)

                    .Work<T>(_context, w => w.GetAllRates)
                        .FlowTo<UpsertCommands>(a => a.UpsertAllRates)

                    .Work<T>(_context, w => w.GetLatestRates)
                        .FlowTo<UpsertCommands>(a => a.UpsertLatestRates);
        }

        public void Start(T target, Func<T, Delegate> method, params object[] args)
        {
            this.Aspect<T>().Work(target, method).Start(args);
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Start(_context, w => w.GetCurrencies);
            Start(_context, w => w.GetLatestRates);
            Start(_context, w => w.GetAllRates);

            await Task.CompletedTask;
        }
    }
}
