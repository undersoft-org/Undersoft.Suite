using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Server
{
    public class ServerSourceProviderConfiguration : ISourceProviderConfiguration
    {
        public ServerSourceProviderConfiguration(IServiceRegistry registry) { _registry = registry; }

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
                    case SourceProvider.SqlServer:
                        _registry.AddEntityFrameworkSqlServer();
                        break;
                    case SourceProvider.AzureSql:
                        _registry.AddEntityFrameworkSqlServer();
                        break;
                    case SourceProvider.PostgreSql:
                        _registry.AddEntityFrameworkNpgsql();
                        break;
                    case SourceProvider.Sqlite:
                        _registry.AddEntityFrameworkSqlite();
                        break;
                    case SourceProvider.MariaDb:
                        _registry.AddEntityFrameworkMySql();
                        break;
                    case SourceProvider.MySql:
                        _registry.AddEntityFrameworkMySql();
                        break;
                    case SourceProvider.Oracle:
                        _registry.AddEntityFrameworkOracle();
                        break;
                    case SourceProvider.CosmosDb:
                        _registry.AddEntityFrameworkCosmos();
                        break;
                    case SourceProvider.MemoryDb:
                        _registry.AddEntityFrameworkInMemoryDatabase();
                        break;
                    default:
                        break;
                }
                _registry.AddEntityFrameworkProxies();

            return _registry;
        }

        public virtual DbContextOptionsBuilder BuildOptions(
         DbContextOptionsBuilder builder,
         SourceProvider provider,
         string connectionString)
        {
            switch (provider)
            {
                case SourceProvider.SqlServer:
                    return builder
                        .UseSqlServer(connectionString)
                        .UseLazyLoadingProxies();
                case SourceProvider.AzureSql:
                    return builder
                        .UseSqlServer(connectionString)
                        .UseLazyLoadingProxies();

                case SourceProvider.PostgreSql:
                    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                    AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
                    return builder.UseInternalServiceProvider(
                         _registry.Manager)
                        .UseNpgsql(connectionString);
                        //.UseLazyLoadingProxies();

                case SourceProvider.Sqlite:
                    return builder
                        .UseSqlite(connectionString)
                        .UseLazyLoadingProxies();

                case SourceProvider.MariaDb:
                    return builder
                        .UseMySql(
                            ServerVersion
                            .AutoDetect(connectionString))
                        .UseLazyLoadingProxies();

                case SourceProvider.MySql:
                    return builder
                        .UseMySql(
                            ServerVersion
                            .AutoDetect(connectionString))
                        .UseLazyLoadingProxies();

                case SourceProvider.Oracle:
                    return builder
                        .UseOracle(connectionString)
                        .UseLazyLoadingProxies();

                case SourceProvider.CosmosDb:
                    return builder
                        .UseCosmos(
                            connectionString.Split('#')[0],
                            connectionString.Split('#')[1],
                            connectionString.Split('#')[2])
                        .UseLazyLoadingProxies();

                case SourceProvider.MemoryDb:
                    return builder.UseInternalServiceProvider(_registry.Manager)
                        .UseInMemoryDatabase(connectionString)
                        .UseLazyLoadingProxies()
                        .ConfigureWarnings(
                            w => w.Ignore(
                                InMemoryEventId
                                .TransactionIgnoredWarning));
                default:
                    break;
            }
            builder.ConfigureWarnings(warnings => warnings
                    .Ignore(CoreEventId.RedundantIndexRemoved));
            return builder;
        }
    }
}
