using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Repository;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Data.Repository.Source
{
    public interface IRepositorySource<TStore, TEntity> : IRepositorySource where TEntity : class
    {
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);

        IQueryable<TEntity> EntitySet();
    }

    public interface IRepositorySource : IRepositoryContextPool
    {
        IDataStoreContext Context { get; }

        DbContextOptions Options { get; }

        bool Pooled { get; }

        void AcquireAccess();

        IDataStoreContext CreateContext(DbContextOptions options);

        IDataStoreContext CreateContext(Type contextType, DbContextOptions options);

        TContext CreateContext<TContext>(DbContextOptions options) where TContext : IDataStoreContext;

        void CreatePool<TContext>();

        IQueryable EntitySet(Type entityType);

        IQueryable<TEntity> EntitySet<TEntity>() where TEntity : class;

        TContext GetContext<TContext>() where TContext : IDataStoreContext;

        void ReleaseAccess();
    }

    public interface IRepositorySource<TContext> : IRepositoryContextPool<TContext>, IDesignTimeDbContextFactory<TContext>, IDbContextFactory<TContext>, IRepositorySource
        where TContext : DbContext
    {
        TContext CreateContext(DbContextOptions<TContext> options);

        new DbContextOptions<TContext> Options { get; }
    }
}
