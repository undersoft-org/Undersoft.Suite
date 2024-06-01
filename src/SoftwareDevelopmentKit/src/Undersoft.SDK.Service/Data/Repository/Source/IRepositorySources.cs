using Microsoft.EntityFrameworkCore;

namespace Undersoft.SDK.Service.Data.Repository.Source
{
    public interface IRepositorySources : ISeries<IRepositorySource>
    {
        IRepositorySource this[DbContext context] { get; set; }
        IRepositorySource this[string contextName] { get; set; }
        IRepositorySource this[Type contextType] { get; set; }

        IRepositorySource<TContext> Add<TContext>(IRepositorySource<TContext> repoSource) where TContext : DbContext;
        IRepositorySource Get(Type contextType);
        IRepositorySource<TContext> Get<TContext>() where TContext : DbContext;
        long GetKey(IRepositorySource item);
        IRepositorySource New(Type contextType, DbContextOptions options);
        IRepositorySource<TContext> New<TContext>(DbContextOptions<TContext> options) where TContext : DbContext, IDataStoreContext;
        int PoolCount(Type contextType);
        int PoolCount<TContext>() where TContext : DbContext;
        IRepositorySource<TContext> Put<TContext>(IRepositorySource<TContext> repoSource) where TContext : DbContext;
        bool Remove<TContext>() where TContext : DbContext;
        bool TryAdd(Type contextType, IRepositorySource repoSource);
        bool TryAdd<TContext>(IRepositorySource<TContext> repoSource) where TContext : DbContext;
        bool TryGet(Type contextType, out IRepositorySource repoSource);
        bool TryGet<TContext>(out IRepositorySource<TContext> repoSource) where TContext : DbContext;
    }
}