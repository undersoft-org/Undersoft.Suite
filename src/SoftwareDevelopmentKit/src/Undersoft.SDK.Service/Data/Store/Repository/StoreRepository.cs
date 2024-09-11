using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Store.Repository;


using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Remote.Repository;
using Undersoft.SDK.Service.Data.Repository;
using Undersoft.SDK.Service.Data.Repository.Source;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Updating;
using Undersoft.SDK.Utilities;

public partial class StoreRepository<TEntity> : Repository<TEntity>, IStoreRepository<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    IQueryable<TEntity> _query;

    public StoreRepository() : base() { }

    public StoreRepository(IRepositorySource repositorySource) : base(repositorySource)
    {
        TrackingEvents();
        Expression = Expression.Constant(this.AsEnumerable());
        Provider = new StoreRepositoryExpressionProvider<TEntity>(StoreContext);
    }

    public StoreRepository(DataStoreContext context) : base(context)
    {
        TrackingEvents();

        Expression = Expression.Constant(this.AsEnumerable());
        Provider = new StoreRepositoryExpressionProvider<TEntity>(context);
    }

    public StoreRepository(IRepositoryContextPool context) : base(context)
    {
        TrackingEvents();

        Expression = Expression.Constant(this.AsEnumerable());
        Provider = new StoreRepositoryExpressionProvider<TEntity>(StoreContext);
    }

    public StoreRepository(IQueryProvider provider, Expression expression)
    {
        ElementType = typeof(TEntity).GetDataType();
        Provider = provider;
        Expression = expression;
    }

    protected IDataStoreContext StoreContext => (IDataStoreContext)InnerContext;

    private TEntity lookup(params object[] keys)
    {
        var item = cache.Lookup<TEntity>(keys);
        if (item != null)
            return StoreContext.Attach(item);
        else
            return StoreContext.Find<TEntity>(keys);
    }

    public override TEntity this[params object[] keys]
    {
        get { return lookup(keys); }
        set
        {
            object current = null;
            TEntity entity = lookup(keys);

            if (entity != null)
                current = value.PatchTo(Stamp(entity));
            else
                current = StoreContext.Add(Sign(value));
        }
    }

    public override TEntity this[object[] keys, params Expression<
        Func<TEntity, object>
    >[] expanders]
    {
        get
        {
            TEntity entity = this[keys];
            if (entity == null)
                return entity;
            if (expanders != null)
            {
                IQueryable<TEntity> _query = Query.Where(e => e.Id == entity.Id);
                if (expanders != null)
                {
                    foreach (Expression<Func<TEntity, object>> expander in expanders)
                    {
                        _query = _query.Include(expander);
                    }
                }
                entity = _query.FirstOrDefault();
            }
            return entity;
        }
        set
        {
            TEntity entity = this[keys];
            if (entity != null)
            {

                if (expanders != null)
                {
                    IQueryable<TEntity> _query = Query.Where(e => e.Id == entity.Id);
                    foreach (Expression<Func<TEntity, object>> expander in expanders)
                    {
                        _query = _query.Include(expander);
                    }
                    entity = _query.FirstOrDefault();
                }

                TEntity current = value.PatchTo(Stamp(entity));
            }
        }
    }

    public override object this[Expression<
        Func<TEntity, object>
    > selector, object[] keys, params Expression<Func<TEntity, object>>[] expanders]
    {
        get
        {
            TEntity entity = this[keys];
            if (entity == null)
                return entity;
            if (expanders != null)
            {
                IQueryable<TEntity> _query = Query.Where(e => e.Id == entity.Id);
                foreach (Expression<Func<TEntity, object>> expander in expanders)
                {
                    _query = _query.Include(expander);
                }
                entity = _query.FirstOrDefault();
            }
            return entity.ToQueryable().Select(selector).FirstOrDefault();
        }
        set
        {
            TEntity entity = this[keys];

            if (expanders != null)
            {
                IQueryable<TEntity> _query = Query.Where(e => e.Id == entity.Id);
                foreach (Expression<Func<TEntity, object>> expander in expanders)
                {
                    _query = _query.Include(expander);
                }
                entity = _query.FirstOrDefault();
            }
            object s = entity.ToQueryable().Select(selector).FirstOrDefault();
            if (s != null)
            {
                value.PatchTo(s);
            }
        }
    }

    public override TEntity Add(TEntity entity)
    {
        return StoreContext.Add(Sign(entity));
    }

    public override TEntity Update(TEntity entity)
    {
        return StoreContext.Update(Stamp(entity));
    }

    public override TEntity Patch(TEntity entity)
    {
        return StoreContext.Update(Stamp(entity));
    }

    public override TEntity Delete(TEntity entity)
    {
        return StoreContext.Remove(entity);
    }

    public override IAsyncEnumerable<TEntity> AddAsync(IEnumerable<TEntity> entity)
    {
        return entity.ForEachAsync((e) => StoreContext.Add(Sign(e)));
    }

    public void AutoTransaction(bool enable)
    {
        StoreContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return StoreContext.Database.BeginTransaction();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return StoreContext.Database.BeginTransactionAsync(Cancellation);
    }

    public void ChangeDetecting(bool enable = true)
    {
        if (InnerContext != null)
        {
            StoreContext.ChangeTracker.AutoDetectChangesEnabled = enable;
        }
    }

    public void ClearTracker()
    {
        StoreContext.ChangeTracker.Clear();
    }

    public virtual async Task CommitTransaction(Task<IDbContextTransaction> transaction)
    {
        await (await transaction).CommitAsync(Cancellation);
    }

    public virtual void CommitTransaction(IDbContextTransaction transaction)
    {
        transaction.Commit();
    }

    public void LazyLoading(bool enable)
    {
        StoreContext.ChangeTracker.LazyLoadingEnabled = enable;
    }

    public override TEntity NewEntry(params object[] parameters)
    {
        return StoreContext.Add(Sign(typeof(TEntity).New<TEntity>(parameters)));
    }

    public void QueryTracking(bool enable)
    {
        if (!enable)
            StoreContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        else
            StoreContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    public void TrackingEvents(bool enable = true)
    {
        if (InnerContext != null)
        {
            if (enable)
            {
                StoreContext.ChangeTracker.StateChanged += AuditStateEvent;
                StoreContext.ChangeTracker.StateChanged += LoadRemotesEvent;
                StoreContext.ChangeTracker.Tracked += LoadRemotesEvent;
            }
            else
            {
                StoreContext.ChangeTracker.StateChanged -= AuditStateEvent;
                StoreContext.ChangeTracker.StateChanged -= LoadRemotesEvent;
                StoreContext.ChangeTracker.Tracked -= LoadRemotesEvent;
            }
        }
    }

    public override object TracePatching(object source, string propertyName, object target = null, Type type = null)
    {
        if (target == null)
            return StoreContext.AttachProperty(source, propertyName, type);
        else
            return StoreContext.AttachProperty(target, propertyName, type);
    }

    public override object TraceAdding(object source, string propertyName, object target = null, Type type = null)
    {
        if (target == null)
            return StoreContext.Attach(source);
        else
            return StoreContext.Attach(target);
    }

    public override IQueryable<TEntity> AsQueryable()
    {
        return Query;
    }

    public override IQueryable<TEntity> Query => _query ??= StoreContext.EntitySet<TEntity>();

    protected override async Task<int> SaveAsTransaction(
        CancellationToken token = default
    )
    {
        await using (
            IDbContextTransaction tr = await StoreContext.Database.BeginTransactionAsync(token)
        )
        {
            try
            {
                int changes = await StoreContext.Save(true, token);

                await tr.CommitAsync(token);

                return changes;
            }
            catch (DbUpdateException e)
            {
                if (e is DbUpdateConcurrencyException)
                    tr.Warning<Datalog>(
                        $"{$"Concurrency update exception data changed by: {e.Source}, "}{$"entries involved in detail data object"}",
                        e.Entries,
                        e
                    );
                tr.Failure<Datalog>(
                    $"{$"Fail on update database transaction Id:{tr.TransactionId}, using context:{StoreContext.GetType().Name},"}{$" TimeStamp:{DateTime.Now.ToString()}, changes made count"}"
                );

                await tr.RollbackAsync(token);

                tr.Warning<Datalog>($"Transaction Id:{tr.TransactionId} Rolling Back !!");
            }

            return -1;
        }
    }

    protected override async Task<int> SaveChanges(
        CancellationToken token = default
    )
    {
        try
        {
            return await StoreContext.SaveChangesAsync(token);
        }
        catch (DbUpdateException e)
        {
            if (e is DbUpdateConcurrencyException)
                StoreContext.Warning<Datalog>(
                    $"{$"Concurrency update exception data changed by: {e.Source}, "}{$"entries involved in detail data object"}",
                    e.Entries,
                    e
                );
            StoreContext.Failure<Datalog>(
                $"{$"Fail on update database, using context:{StoreContext.GetType().Name}, "}{$"TimeStamp: {DateTime.Now.ToString()}"}"
            );
        }

        return -1;
    }
}

public class StoreRepository<TStore, TEntity>
    : StoreRepository<TEntity>,
        IStoreRepository<TStore, TEntity>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataStore
{
    public StoreRepository(
        IRepositoryContextPool<DataStoreContext<TStore>> pool,
        IEntityCache<TStore, TEntity> cache,
        IEnumerable<IRemoteProperty<IDataStore, TEntity>> remoteProps,
        IRemoteSynchronizer synchronizer
    ) : base(pool.ContextPool)
    {
        this.cache = cache;
        synchronizer.AddRepository(this);
        RemoteProperties = remoteProps.DoEach(
            (o) =>
            {
                o.Host = this;
                return o;
            }
        );
    }

    public override Task<int> Save(
        bool asTransaction,
        CancellationToken token = default
    )
    {
        return ContextLease.Save(asTransaction, token);
    }
}
