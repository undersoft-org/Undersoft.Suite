using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public class WorkerService : BackgroundService
    {
        public IServicer _servicer { get; }

        public WorkerService(IServicer servicer)
        {
            _servicer = servicer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        public virtual Task DoWork(CancellationToken stoppingToken)
        {
            return default(Task);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
    }
}