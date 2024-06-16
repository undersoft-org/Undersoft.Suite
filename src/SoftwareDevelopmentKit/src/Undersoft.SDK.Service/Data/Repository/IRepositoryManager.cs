using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Data.Repository.Source;
using Undersoft.SDK.Service.Data.Store.Repository;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryManager
    {

        IRepositoryClient AddClient(IRepositoryClient client);
        void AddClientPool(Type contextType, int poolSize, int minSize = 1);
        Task AddClientPools();
        Task AddPools();
        IRepositorySource AddSource(IRepositorySource source);
        void AddSourcePool(Type contextType, int poolSize, int minSize = 1);
        ValueTask DisposeAsyncCore();
        IRepositoryClient GetClient<TStore, TEntity>() where TEntity : class, IOrigin, IInnerProxy;
        IEnumerable<IRepositoryClient> GetClients();
        IRepositorySource GetSource<TStore, TEntity>() where TEntity : class, IOrigin, IInnerProxy;
        IEnumerable<IRepositorySource> GetSources();
        IRemoteRepository<TDto> RemoteSet<TDto>() where TDto : class, IOrigin, IInnerProxy;
        IRemoteRepository<TDto> RemoteSet<TDto>(Type contextType) where TDto : class, IOrigin, IInnerProxy;
        IRemoteRepository<TDto> RemoteSet<TStore, TDto>()
            where TStore : IDataServiceStore
            where TDto : class, IOrigin, IInnerProxy;
        bool TryGetClient(Type contextType, out IRepositoryClient source);
        bool TryGetClient<TContext>(out IRepositoryClient<TContext> source) where TContext : OpenDataContext;
        bool TryGetSource(Type contextType, out IRepositorySource source);
        bool TryGetSource<TContext>(out IRepositorySource<TContext> source) where TContext : DbContext;
        IStoreRepository<TEntity> StoreSet<TEntity>() where TEntity : class, IOrigin, IInnerProxy;
        IStoreRepository<TEntity> StoreSet<TEntity>(Type contextType) where TEntity : class, IOrigin, IInnerProxy;
        IStoreRepository<TEntity> StoreSet<TStore, TEntity>()
            where TStore : IDataServerStore
            where TEntity : class, IOrigin, IInnerProxy;
    }
}