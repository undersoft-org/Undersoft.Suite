using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Undersoft.SDK.Service.Hosting
{
    public partial class ServiceHostSetup : IServiceHostSetup
    {
        IHostBuilder _host;
        IHostEnvironment _environment;
        IServiceManager _manager;

        public ServiceHostSetup(IHostBuilder host, IServiceManager manager)
        {
            _host = host;
            _manager = manager;
        }

        public ServiceHostSetup(IHostBuilder host, IServiceManager manager, IHostEnvironment environment, bool useSwagger) : this(host, manager)
        {
            _environment = environment;
        }

        public ServiceHostSetup(IHostBuilder host, IServiceManager manager, IHostEnvironment environment, string[] apiVersions = null) : this(host, manager)
        {
            _environment = environment;
        }

        public virtual IServiceHostSetup RebuildProviders()
        {
            UseInternalProvider();
            return this;
        }

        public IServiceHostSetup UseServiceClients()
        {
            this.LoadOpenDataEdms().ConfigureAwait(true);
            return this;
        }

        public IServiceHostSetup UseDataMigrations()
        {
            using (var session = _manager.CreateScope())
            {
                try
                {
                    IServicer us = session.ServiceProvider.GetRequiredService<IServicer>();
                    us.GetSources().ForEach(e => ((DbContext)e.Context).Database.Migrate());
                }
                catch (Exception ex)
                {
                    this.Error<Applog>("Object migration initial create - unable to connect the database engine", null, ex);
                }
            }

            return this;
        }

        public virtual IServiceHostSetup UseInternalProvider()
        {
            _manager.Registry.MergeServices();
            _host.UseServiceProviderFactory(_manager.GetProviderFactory());
            return this;
        }

        public virtual IServiceManager Manager => _manager;
    }
}