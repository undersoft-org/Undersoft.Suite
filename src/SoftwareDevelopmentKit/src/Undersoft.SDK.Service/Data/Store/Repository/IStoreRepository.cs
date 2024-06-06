using Microsoft.EntityFrameworkCore.Storage;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service.Data.Store.Repository
{
    public interface IStoreRepository<TStore, TEntity> : IStoreRepository<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
    }

    public interface IStoreRepository<TEntity> : IRepository<TEntity> where TEntity : class, IOrigin, IInnerProxy
    {
        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CommitTransaction(IDbContextTransaction transaction);
        Task CommitTransaction(Task<IDbContextTransaction> transaction);
    }
}