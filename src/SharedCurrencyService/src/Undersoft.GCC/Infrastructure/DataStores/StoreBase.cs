using Microsoft.EntityFrameworkCore;

namespace Undersoft.GCC.Infrastructure.DataStores;

using Undersoft.GCC.Domain.Entities;
using Undersoft.GCC.Infrastructure.DataStores.Mappings;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public abstract class StoreBase<TStore, TContext> : DbStore<TStore, TContext>
    where TStore : IDataServerStore
    where TContext : DbContext
{
    public StoreBase(DbContextOptions<TContext> options) : base(options) { }

    public virtual DbSet<Currency>? Currencies { get; set; }
    public virtual DbSet<CurrencyProvider>? CurrencyProviders { get; set; }
    public virtual DbSet<CurrencyRate>? CurrencyRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyMapping(new CurrencyMappings());
        modelBuilder.ApplyMapping(new CurrencyProviderMappings());
        modelBuilder.ApplyMapping(new CurrencyRateMappings());

        base.OnModelCreating(modelBuilder);
    }
}
