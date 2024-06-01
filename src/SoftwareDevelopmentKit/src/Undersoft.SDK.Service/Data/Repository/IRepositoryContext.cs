using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Repository
{
    public interface IRepositoryContext : IResettableService, IIdentifiable, IDisposable, IAsyncDisposable
    {
        DataSite Site { get; set; }

        IRepositoryContext ContextLease { get; set; }

        IRepositoryContextPool ContextPool { get; set; }

        Type ContextType { get; }

        object InnerContext { get; set; }

        bool Leased { get; }

        void SnapshotConfiguration();

        bool Release();

        Task<bool> ReleaseAsync(CancellationToken token = default);

        void Lease(IRepositoryContext lease);

        Task LeaseAsync(IRepositoryContext lease, CancellationToken token = default);

        Task<int> Save(bool asTransaction, CancellationToken token = default);
    }

    public interface IRepositoryContext<out TContext> : IRepositoryContext where TContext : class
    {
        TContext Context => (TContext)InnerContext;
    }

    public class DbContextConfigurationSnapshot
    {
        public DbContextConfigurationSnapshot(
            bool? autoDetectChangesEnabled,
            QueryTrackingBehavior? queryTrackingBehavior,
            AutoTransactionBehavior autoTransactionsBehaviour,
            bool? lazyLoadingEnabled,
            CascadeTiming? cascadeDeleteTiming,
            CascadeTiming? deleteOrphansTiming)
        {
            AutoDetectChangesEnabled = autoDetectChangesEnabled;
            QueryTrackingBehavior = queryTrackingBehavior;
            this.AutoTransactionBehavior = autoTransactionsBehaviour;
            LazyLoadingEnabled = lazyLoadingEnabled;
            CascadeDeleteTiming = cascadeDeleteTiming;
            DeleteOrphansTiming = deleteOrphansTiming;
        }

        public virtual bool? AutoDetectChangesEnabled { get; }

        public virtual bool? LazyLoadingEnabled { get; }

        public virtual CascadeTiming? CascadeDeleteTiming { get; }

        public virtual CascadeTiming? DeleteOrphansTiming { get; }

        public virtual QueryTrackingBehavior? QueryTrackingBehavior { get; }

        public virtual AutoTransactionBehavior AutoTransactionBehavior { get; }
    }

}