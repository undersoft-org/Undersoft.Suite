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

        private async Task DoWork(CancellationToken stoppingToken)
        {
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }
    }
}