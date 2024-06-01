using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service
{

    public class ServiceManagerFactory : IServiceProviderFactory<IServiceCollection>
    {
        readonly ServiceProviderOptions _options;
        readonly IServiceManager _manager;
        readonly DefaultServiceProviderFactory _providerFactory;

        public ServiceManagerFactory()
        {
            _options = new ServiceProviderOptions();
            _providerFactory = new DefaultServiceProviderFactory(_options);
            _manager = new ServiceManager();
        }

        public ServiceManagerFactory(IServiceManager manager)
        {
            _options = new ServiceProviderOptions();
            _providerFactory = new DefaultServiceProviderFactory(_options);
            _manager = manager;
        }

        public ServiceManagerFactory(IServiceManager manager, ServiceProviderOptions options)
        {
            _options = options;
            _providerFactory = new DefaultServiceProviderFactory(_options);
            _manager = manager;
        }

        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            if (!ReferenceEquals(_manager.Registry, services))
                _manager.Registry.MergeServices(services, true);
            return services;
        }

        public virtual IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            if (!ReferenceEquals(_manager.Registry, containerBuilder))
            {
                _manager.Registry.MergeServices(containerBuilder, true);
                var provider = _providerFactory.CreateServiceProvider(_manager.Registry);
                _manager.ReplaceProvider(provider);
                return provider;
            }
            else
            {
                return _providerFactory.CreateServiceProvider(_manager.Registry);
            }
        }
    }
}
