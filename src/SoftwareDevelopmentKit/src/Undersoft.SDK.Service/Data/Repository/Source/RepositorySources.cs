using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SDK.Service.Data.Repository.Source
{
    public class RepositorySources : Registry<IRepositorySource>, IRepositorySources
    {
        public RepositorySources() : base(false, 17) { }

        public IRepositorySource this[string contextName]
        {
            get => base[contextName.UniqueKey64()];
            set => base.Set(contextName.UniqueKey64(), value);
        }
        public IRepositorySource this[DbContext context]
        {
            get => base[context.GetType()];
            set => base.Set(context.GetType(), value);
        }
        public IRepositorySource this[Type contextType]
        {
            get => base[contextType];
            set => base.Set(contextType, value);
        }

        public IRepositorySource Get(Type contextType)
        {
            return base[contextType];
        }
        public IRepositorySource<TContext> Get<TContext>() where TContext : DbContext
        {
            return (IRepositorySource<TContext>)base[typeof(TContext)];
        }

        public bool TryGet(Type contextType, out IRepositorySource repoSource)
        {
            return base.TryGet(contextType, out repoSource);
        }
        public bool TryGet<TContext>(out IRepositorySource<TContext> repoSource) where TContext : DbContext
        {
            if (TryGet(typeof(TContext), out IRepositorySource _repo))
            {
                repoSource = (IRepositorySource<TContext>)_repo;
                return false;
            }
            repoSource = null;
            return false;
        }

        public bool TryAdd(Type contextType, IRepositorySource repoSource)
        {
            return base.Add(contextType, repoSource);
        }
        public override bool TryAdd(IRepositorySource repoSource)
        {
            return base.Add(repoSource.ContextType, repoSource);
        }
        public bool TryAdd<TContext>(IRepositorySource<TContext> repoSource) where TContext : DbContext
        {
            return base.Add(typeof(TContext), repoSource);
        }

        public IRepositorySource<TContext> Add<TContext>(IRepositorySource<TContext> repoSource) where TContext : DbContext
        {
            return (IRepositorySource<TContext>)base.Put(typeof(TContext), repoSource).Value;
        }
        public override void Add(IRepositorySource repoSource)
        {
            base.Put(repoSource.ContextType, repoSource);
        }

        public bool Remove<TContext>() where TContext : DbContext
        {
            return TryRemove(typeof(TContext));
        }

        public int PoolCount(Type contextType)
        {
            return Get(contextType).Count;
        }
        public int PoolCount<TContext>() where TContext : DbContext
        {
            return Get<TContext>().Count;
        }

        public IRepositorySource<TContext> Put<TContext>(IRepositorySource<TContext> repoSource)
            where TContext : DbContext
        {
            return (IRepositorySource<TContext>)base.Put(typeof(TContext), repoSource).Value;
        }

        public IRepositorySource<TContext> New<TContext>(DbContextOptions<TContext> options) where TContext : DbContext, IDataStoreContext
        {
            return Add(new RepositorySource<TContext>(options));
        }
        public IRepositorySource New(Type contextType, DbContextOptions options)
        {
            return Put(new RepositorySource(contextType, options)).Value;
        }

        public long GetKey(IRepositorySource item)
        {
            return item.Id;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    base.Dispose(true);
                }
                disposedValue = true;
            }
        }
    }

}
