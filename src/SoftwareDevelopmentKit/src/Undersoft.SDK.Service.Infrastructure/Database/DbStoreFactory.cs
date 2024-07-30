using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Undersoft.SDK.Service.Data.Store;

using Configuration;
using Undersoft.SDK.Service.Data.Repository.Source;
using Undersoft.SDK.Utilities;

public class DbStoreFactory<TContext, TSourceProvider> : IDesignTimeDbContextFactory<TContext>, IDbContextFactory<TContext> where TContext : DbContext where TSourceProvider : class, ISourceProviderConfiguration
{
    public TContext CreateDbContext(string[] args)
    {
        var config = new ServiceConfiguration();
        var configSource = config.Source(typeof(TContext).FullName);
        var provider = config.SourceProvider(configSource);
        StoreOptionsBuilder.AddRootEntityFrameworkSourceProvider<TSourceProvider>(provider);
        var options = StoreOptionsBuilder.BuildOptions<TContext>(provider, config.SourceConnectionString(configSource)).Options;
        return typeof(TContext).New<TContext>(options);
    }

    public TContext CreateDbContext()
    {
        return this.CreateDbContext(null);
    }
}