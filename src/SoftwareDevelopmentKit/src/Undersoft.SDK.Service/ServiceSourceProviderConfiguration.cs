using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Data.Repository.Source;

namespace Undersoft.SDK.Service
{
    public class ServiceSourceProviderConfiguration : ISourceProviderConfiguration
    {
        public ServiceSourceProviderConfiguration() { }

        public ServiceSourceProviderConfiguration(IServiceRegistry registry)
        {
            _registry = registry;
        }

        IServiceRegistry _registry { get; set; }

        public virtual IServiceRegistry AddSourceProvider(SourceProvider provider)
        {
            if (!DataStoreRegistry.SourceProviders.ContainsKey((int)provider))
            {
                RegisterSourceProvider(provider);
                DataStoreRegistry.SourceProviders.Add((int)provider, provider);
            }
            return _registry;
        }

        public virtual IServiceRegistry RegisterSourceProvider(SourceProvider provider)
        {
                switch (provider)
                {
                    case SourceProvider.MemoryDb:
                        _registry.AddEntityFrameworkInMemoryDatabase();
                        break;
                    default:
                        break;
                }
                //_registry.AddEntityFrameworkProxies();

            return _registry;
        }

        public virtual DbContextOptionsBuilder BuildOptions(
            DbContextOptionsBuilder builder,
            SourceProvider provider,
            string connectionString
        )
        {
            builder.UseInternalServiceProvider(_registry.Manager);
            //.UseLazyLoadingProxies();

            switch (provider)
            {
                case SourceProvider.MemoryDb:
                    return builder
                        .UseInMemoryDatabase(connectionString)
                        .ConfigureWarnings(
                            w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)
                        );

                default:
                    break;
            }

            builder.ConfigureWarnings(
                warnings => warnings.Ignore(CoreEventId.RedundantIndexRemoved)
            );
            return builder;
        }
    }
}
