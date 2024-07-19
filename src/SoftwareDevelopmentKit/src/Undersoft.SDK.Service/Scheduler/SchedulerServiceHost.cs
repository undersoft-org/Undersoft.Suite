using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Scheduler
{
    public class SchedulerServiceHost : ServiceHost
    {
        public SchedulerServiceHost()
        {
            _hostBuilder = new SchedulerHostBuilder(this);
        }

        public IServiceHostBuilder GetHostBuilder() => _hostBuilder;
    }
}
