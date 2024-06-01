using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Undersoft.SDK.Service.Data.Store;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections;
using Undersoft.SDK.Service;

public class DataStoreContext<TStore> : DataStoreContext, IDataStoreContext<TStore>
    where TStore : IDataServerStore
{
    protected virtual Type StoreType { get; }

    public DataStoreContext(DbContextOptions options, IServicer servicer = null)
        : base(options, servicer)
    {
        StoreType = typeof(TStore);
    }
}

public class DataStoreContext : DbContext, IDataStoreContext, IResettableService
{
    public virtual IServicer servicer { get; }

    public override IModel Model
    {
        get { return base.Model; }
    }

    public DataStoreContext(DbContextOptions options, IServicer servicer = null) : base(options)
    {
        this.servicer = servicer;
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class
    {
        return EntitySet<TEntity>();
    }

    public IQueryable<TEntity> EntitySet<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public IQueryable EntitySet(Type type)
    {
        return (IQueryable)this.GetEntitySet(type);
    }

    public new TEntity Add<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Add(entity).Entity;
    }

    public object AttachProperty(object item, string propertyName, Type type = null)
    {
        if (type == null)
        {
            Attach(item);
            return item;
        }
        else if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            var list = Entry(item).Collection(propertyName);
            Attach(list.EntityEntry.Entity);
            list.Load();
            return list.CurrentValue;
        }
        else
        {
            var obj = Entry(item).Reference(propertyName);
            Attach(obj.EntityEntry.Entity);
            obj.Load();
            return obj.CurrentValue;
        }
    }

    public async new ValueTask<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
    {
        return await ValueTask.FromResult((await base.AddAsync(entity)).Entity);
    }

    public new object Add(object entity)
    {
        return base.Add(entity).Entity;
    }

    public new TEntity Update<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Update(entity).Entity;
    }

    public new TEntity Remove<TEntity>(TEntity entity) where TEntity : class
    {
        var _entity = Find<TEntity>(((IIdentifiable)entity).Id);
        return base.Remove(_entity).Entity;
    }

    public new object Attach(object entity)
    {
        return base.Attach(entity).Entity;
    }

    public new TEntity Attach<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Attach(entity).Entity;
    }

    public virtual Task<int> Save(bool asTransaction, CancellationToken token = default)
    {
        return saveEndpoint(asTransaction, token);
    }

    private async Task<int> saveEndpoint(bool asTransaction, CancellationToken token = default)
    {
        if (ChangeTracker.HasChanges())
        {
            if (asTransaction)
                return await saveAsTransaction(token);
            else
                return await saveChanges(token);
        }
        return 0;
    }

    private async Task<int> saveAsTransaction(CancellationToken token = default)
    {
        await using var tr = await Database.BeginTransactionAsync(token);
        try
        {
            var changes = await SaveChangesAsync(token);

            await tr.CommitAsync(token);

            return changes;
        }
        catch (DbUpdateException e)
        {
            if (e is DbUpdateConcurrencyException)
                tr.Warning<Datalog>(
                    $"Concurrency update exception data changed by: {e.Source}, "
                        + $"entries involved in detail data object",
                    e.Entries,
                    e
                );
            else
                tr.Failure<Datalog>(
                    $"Fail on update database transaction Id:{tr.TransactionId}, using context:{GetType().Name},"
                        + $" context Id:{ContextId}, TimeStamp:{DateTime.Now.ToString()} {e.StackTrace}",
                    e.Entries
                );

            await tr.RollbackAsync(token);

            tr.Warning<Datalog>($"Transaction Id:{tr.TransactionId} Rolling Back !!");
        }
        return -1;
    }

    private async Task<int> saveChanges(CancellationToken token = default)
    {
        try
        {
            return await SaveChangesAsync(token);
        }
        catch (DbUpdateException e)
        {
            if (e is DbUpdateConcurrencyException)
                this.Warning<Datalog>(
                    $"Concurrency update exception data changed by: {e.Source}, "
                        + $"entries involved in detail data object",
                    e.Entries,
                    e
                );
            else
                this.Failure<Datalog>(
                    $"Fail on update database, using context:{GetType().Name}, "
                        + $"context Id: {ContextId}, TimeStamp: {DateTime.Now.ToString()}"
                );
        }

        return -1;
    }
}
