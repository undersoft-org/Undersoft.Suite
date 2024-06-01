using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;

namespace Undersoft.SDK.Service.Scheduler
{
    public class SchedulerHostedService : IHostedService, IAsyncDisposable
    {
        private readonly Task _completedTask = Task.CompletedTask;

        private IServicer _servicer;
        protected IScheduler _scheduler = default!;
        private SchedulerServiceHost _host = default!;
        private Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> _registry;

        public SchedulerHostedService(IServicer servicer)
        {
            _servicer = servicer;
            _host = _servicer.GetService<SchedulerServiceHost>();
            _registry = _servicer.GetService<Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>>();
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            var options = _servicer.Registry.GetRequiredService<IOptions<SchedulerOptions>>().Value;

            _scheduler = _servicer.Registry.GetRequiredService<IScheduler>();

            if (_scheduler != null)
            {
                if (_registry != null)
                {
                    await _scheduler.ScheduleJobs(_registry, true);
                }

                await options.StartSchedulerFactory.Invoke(_scheduler);
            }
        }

        public IScheduler GetScheduler() { return _scheduler; }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            return _completedTask;
        }

        public async ValueTask DisposeAsync()
        {
            await _scheduler.Shutdown();
            await _host?.StopAsync();
        }
    }
}