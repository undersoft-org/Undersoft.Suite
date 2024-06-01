using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Infrastructure.Database;

public partial class DbStore<TStore, TContext> : DataStoreContext<TStore>
    where TStore : IDataServerStore
    where TContext : DbContext
{
    public DbStore(DbContextOptions<TContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyIdentity<TContext>();
        base.OnModelCreating(modelBuilder);
    }
}
