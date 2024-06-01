using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

namespace Undersoft.SDK.Service.Data.Store;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel;
using Undersoft.SDK.Service.Data.Object;
using Uniques;

public interface IDataStoreContext<TStore> : IDataStoreContext where TStore : IDataServerStore { }

public interface IDataStoreContext : IResettableService, IDisposable, IAsyncDisposable
{
    DatabaseFacade Database { get; }

    IQueryable<TEntity> Query<TEntity>() where TEntity : class;

    IModel Model { get; }

    IQueryable<TEntity> EntitySet<TEntity>() where TEntity : class;

    IQueryable EntitySet(Type entityType);

    TEntity Add<TEntity>(TEntity entity) where TEntity : class;

    ValueTask<TEntity> AddAsync<TEntity>(
            TEntity entity,
            CancellationToken cancellationToken = default) where TEntity : class;

    object Add(object entity);

    TEntity Update<TEntity>(TEntity entity) where TEntity : class;

    TEntity Remove<TEntity>(TEntity entity) where TEntity : class;

    object Attach(object entity);

    TEntity Attach<TEntity>(TEntity entity) where TEntity : class;

    void AddRange(params object[] entities);

    void AddRange(IEnumerable<object> entities);

    Task AddRangeAsync(params object[] entities);

    void AttachRange(params object[] entities);

    void UpdateRange(params object[] entities);

    void RemoveRange(params object[] entities);

    Task AddRangeAsync(
            IEnumerable<object> entities,
            CancellationToken cancellationToken = default);

    void AttachRange(IEnumerable<object> entities);

    void UpdateRange(IEnumerable<object> entities);

    void RemoveRange(IEnumerable<object> entities);

    object AttachProperty(object item, string propertyName, Type type = null);

    object Find(Type entityType, params object[] keyValues);

    ValueTask<object> FindAsync(Type entityType, params object[] keyValues);

    TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;

    ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class;

    ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : class;

    ChangeTracker ChangeTracker { get; }

    Task<int> SaveChangesAsync(CancellationToken token);

    int SaveChanges();

    Task<int> Save(bool asTransaction, CancellationToken token = default);
}