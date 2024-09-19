using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public class OperatorService : IHostedService, IAsyncDisposable
    {
        private readonly Task _completedTask = Task.CompletedTask;
        private int _executionCount = 0;
        private Timer? _timer;

        public OperatorService(IServicer servicer)
        {

        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Log.Info<Servicelog>("{Service} is running.", nameof(OperatorService));

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return _completedTask;
        }

        private void DoWork(object state)
        {
            int count = Interlocked.Increment(ref _executionCount);

            Log.Info<Servicelog>(
                "{Service} is working, execution count: {Count:#,0}",
                nameof(OperatorService),
                count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Info<Servicelog>(
                "{Service} is stopping.", nameof(OperatorService));

            _timer?.Change(Timeout.Infinite, 0);

            return _completedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_timer is IAsyncDisposable timer)
            {
                await timer.DisposeAsync();
            }

            _timer = null;
        }
    }
}