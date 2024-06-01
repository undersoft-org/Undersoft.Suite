using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;
using System;
using Undersoft.SDK.Service.Data.Client;

namespace Undersoft.SDK.Service.Data.Repository.Client
{
    public class RepositoryClients : Registry<IRepositoryClient>, IRepositoryClients
    {
        public RepositoryClients() : base(false, 17) { }

        public IRepositoryClient this[string contextName]
        {
            get => base[contextName.UniqueKey64()];
            set => base.Set(contextName.UniqueKey64(), value);
        }
        public IRepositoryClient this[OpenDataContext context]
        {
            get => base[context.GetType()];
            set => base.Set(context.GetType(), value);
        }
        public IRepositoryClient this[Type contextType]
        {
            get => base[contextType];
            set => base.Set(contextType, value);
        }

        public IRepositoryClient Get(Type contextType)
        {
            return base[contextType];
        }
        public IRepositoryClient<TContext> Get<TContext>() where TContext : OpenDataContext
        {
            return (IRepositoryClient<TContext>)base[typeof(TContext)];
        }

        public bool TryGet(Type contextType, out IRepositoryClient repoSource)
        {
            return base.TryGet(contextType, out repoSource);
        }
        public bool TryGet<TContext>(out IRepositoryClient<TContext> repoSource) where TContext : OpenDataContext
        {
            if (!TryGet(typeof(TContext), out IRepositoryClient _repo))
            {
                repoSource = (IRepositoryClient<TContext>)_repo;
                return false;
            }
            repoSource = null;
            return false;
        }

        public bool TryAdd(Type contextType, IRepositoryClient repoSource)
        {
            return base.Add(contextType, repoSource);
        }
        public override bool TryAdd(IRepositoryClient repoSource)
        {
            return base.Add(repoSource.ContextType, repoSource);
        }
        public bool TryAdd<TContext>(IRepositoryClient<TContext> repoSource) where TContext : OpenDataContext
        {
            return base.Add(typeof(TContext), repoSource);
        }

        public IRepositoryClient<TContext> Add<TContext>(IRepositoryClient<TContext> repoSource) where TContext : OpenDataContext
        {
            return (IRepositoryClient<TContext>)base.Put(typeof(TContext), repoSource).Value;
        }
        public override void Add(IRepositoryClient repoSource)
        {
            base.Put(repoSource.ContextType, repoSource);
        }

        public bool Remove<TContext>() where TContext : OpenDataContext
        {
            return TryRemove(typeof(TContext));
        }

        public int PoolCount(Type contextType)
        {
            return Get(contextType).Count;
        }
        public int PoolCount<TContext>() where TContext : OpenDataContext
        {
            return Get<TContext>().Count;
        }

        public IRepositoryClient<TContext> Put<TContext>(IRepositoryClient<TContext> repoSource)
            where TContext : OpenDataContext
        {
            return (IRepositoryClient<TContext>)base.Put(typeof(TContext), repoSource).Value;
        }

        public IRepositoryClient<TContext> New<TContext>(Uri route) where TContext : OpenDataContext
        {
            return Add(new RepositoryClient<TContext>(route));
        }
        public IRepositoryClient New(Type contextType, Uri route)
        {
            return Put(new RepositoryClient(contextType, route)).Value;
        }

        public long GetKey(IRepositoryClient item)
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
