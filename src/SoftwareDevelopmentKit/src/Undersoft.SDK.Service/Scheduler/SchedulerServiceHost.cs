using Undersoft.SDK.Service.Hosting;

namespace Undersoft.SDK.Service.Scheduler
{
    public class SchedulerServiceHost : ServiceHost
    {
        public SchedulerServiceHost()
        {
            _hostBuilder = new SchedulerHostBuilder(this);
        }

        public new SchedulerServiceHost CreateHost(string[] args = null)
        {
            var sh = new SchedulerServiceHost();
            sh.Configure(args);
            return sh;
        }

        public IServiceHostBuilder GetHostBuilder() => _hostBuilder;
    }
}
