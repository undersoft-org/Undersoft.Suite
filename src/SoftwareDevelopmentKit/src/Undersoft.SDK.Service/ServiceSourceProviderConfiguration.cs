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
                    case SourceProvider.Sqlite:
                        _registry.AddEntityFrameworkSqlite();
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
            switch (provider)
            {
                case SourceProvider.MemoryDb:
                    return builder
                        .UseInternalServiceProvider(new ServiceManager())
                        .UseInMemoryDatabase(connectionString)
                        .UseLazyLoadingProxies()
                        .ConfigureWarnings(
                            w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)
                        );

                case SourceProvider.Sqlite:
                    return builder.UseSqlite(connectionString).UseLazyLoadingProxies();

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
