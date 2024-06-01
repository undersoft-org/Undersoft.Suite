﻿using Microsoft.OData.Client;
using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Remote.Repository;

using Logging;
using Proxies;
using Series;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Mapper;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Data.Repository;
using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Updating;

public class RemoteRepository<TStore, TEntity>
    : RemoteRepository<TEntity>,
        IRemoteRepository<TStore, TEntity>
    where TEntity : class, IOrigin, IInnerProxy
    where TStore : IDataServiceStore
{
    public RemoteRepository(IRepositoryContextPool<OpenDataClient<TStore>> pool)
        : base(pool.ContextPool)
    {
        mapper = DataMapper.GetMapper();
    }

    public RemoteRepository(
        IRepositoryContextPool<OpenDataClient<TStore>> pool,
        IEntityCache<TStore, TEntity> cache
    ) : base(pool.ContextPool)
    {
        mapper = cache.Mapper;
        this.cache = cache;
    }

    public RemoteRepository(
        IRepositoryContextPool<OpenDataClient<TStore>> pool,
        IEntityCache<TStore, TEntity> cache,
        IAuthorization authorization
    ) : base(pool.ContextPool)
    {
        RemoteContext.SetAuthorization(authorization.Credentials.SessionToken);
        mapper = cache.Mapper;
        this.cache = cache;
    }

    public override Task<int> Save(bool asTransaction, CancellationToken token = default)
    {
        return ContextLease.Save(asTransaction, token);
    }
}

public partial class RemoteRepository<TEntity> : Repository<TEntity>, IRemoteRepository<TEntity>
    where TEntity : class, IOrigin, IInnerProxy
{
    protected OpenDataContext RemoteContext => (OpenDataContext)InnerContext;
    protected DataServiceQuery<TEntity> RemoteQuery;

    public RemoteRepository() { }

    public RemoteRepository(IRepositoryClient repositorySource) : base(repositorySource)
    {
        if (RemoteContext != null)
        {
            RemoteQuery = RemoteContext.CreateQuery<TEntity>(Name);
            Expression = Expression.Constant(this);
            Provider = new RemoteRepositoryExpressionProvider<TEntity>(Query);
        }
    }

    public RemoteRepository(OpenDataContext context) : base(context)
    {
        if (RemoteContext != null)
        {
            RemoteQuery = RemoteContext.CreateQuery<TEntity>(Name);
            Expression = Expression.Constant(this.AsEnumerable());
            Provider = new RemoteRepositoryExpressionProvider<TEntity>(RemoteQuery);
        }
    }

    public RemoteRepository(IRepositoryContextPool context) : base(context)
    {
        if (RemoteContext != null)
        {
            RemoteQuery = RemoteContext.CreateQuery<TEntity>(Name);
            Expression = Expression.Constant(this);
            Provider = new RemoteRepositoryExpressionProvider<TEntity>(Query);
        }
    }

    public RemoteRepository(IQueryProvider provider, Expression expression)
    {
        ElementType = typeof(TEntity).GetDataType();
        Provider = provider;
        Expression = expression;
    }

    public override TEntity this[params object[] keys]
    {
        get => lookup(keys);
        set
        {
            TEntity entity = lookup(keys);
            if (entity != null)
            {
                InnerPatch(value, entity);
            }
            else
                Add(value);
        }
    }

    public override TEntity this[object[] keys, params Expression<
        Func<TEntity, object>
    >[] expanders]
    {
        get
        {
            DataServiceQuerySingle<TEntity> query = FindQuerySingle(keys);
            if (expanders != null)
                foreach (Expression<Func<TEntity, object>> expander in expanders)
                    query = query.Expand(expander);

            return query.GetValue();
        }
        set
        {
            DataServiceQuerySingle<TEntity> query = FindQuerySingle(keys);
            if (expanders != null)
                if (expanders != null)
                    foreach (Expression<Func<TEntity, object>> expander in expanders)
                        query = query.Expand(expander);

            TEntity entity = query.GetValue();
            if (entity != null)
            {
                InnerPatch(value, entity);
            }
            else
            {
                Add(value);
            }
        }
    }

    public override object this[Expression<
        Func<TEntity, object>
    > selector, object[] keys, params Expression<Func<TEntity, object>>[] expanders]
    {
        get
        {
            DataServiceQuery<TEntity> query = (DataServiceQuery<TEntity>)FindQuery(keys);
            if (expanders != null)
                foreach (Expression<Func<TEntity, object>> expander in expanders)
                    query = query.Expand(expander);

            return query.Select(selector).FirstOrDefault();
        }
        set
        {
            DataServiceQuery<TEntity> query = (DataServiceQuery<TEntity>)FindQuery(keys);
            if (expanders != null)
                foreach (Expression<Func<TEntity, object>> expander in expanders)
                    query = query.Expand(expander);

            TEntity entity = query.FirstOrDefault();
            if (entity != null)
            {
                object item = entity.ToQueryable().Select(selector).FirstOrDefault();
                RemoteContext.UpdateObject(item);
                value.PatchTo(item, PatchingInvoker);
            }
        }
    }

    public override IEnumerable<TEntity> lookup<TModel>(IEnumerable<TModel> entities)
    {
        return entities.ForEach(e => lookup(e.Id)).Commit();
    }

    public override TEntity lookup<TModel>(TModel entity)
    {
        return lookup(entity.Id);
    }

    private TEntity lookup(object key)
    {
        var item = cache.Lookup<TEntity>(key);
        if (item == null)
            item = FindQuerySingle(key).GetValue();
        return item;
    }

    private TEntity lookup(params object[] keys)
    {
        var item = cache.Lookup<TEntity>(keys);
        if (item == null)
            item = FindQuerySingle(keys).GetValue();
        return item;
    }

    private async Task<TEntity> lookupAsync(params object[] keys)
    {
        var item = cache.Lookup<TEntity>(keys);
        if (item == null)
            item = await FindQuerySingle(keys).GetValueAsync();
        return item;
    }

    public override TEntity InnerPatch<TModel>(TModel source, TEntity target) where TModel : class
    {
        return Patch((TEntity)source.PatchTo(target.Proxy, PatchingInvoker).Target);
    }

    public override TEntity InnerPut<TModel>(TModel source, TEntity target) where TModel : class
    {

        return Update((TEntity)source.PutTo(target.Proxy, PatchingInvoker).Target);
    }

    public override TEntity InnerSet<TModel>(TModel source, TEntity target) where TModel : class
    {
        return InnerPut(source, target);
    }

    public override TEntity Delete(TEntity entity)
    {
        var item = lookup(entity.Id);
        if (item != null)
            RemoteContext.Command(CommandType.DELETE, item, Name);
        return item;
    }

    public override TEntity Update(TEntity entity)
    {
        if (entity != null)
        {
            entity = Stamp(entity);
            RemoteContext.Command(CommandType.PUT, entity, Name);
            return entity;
        }
        return null;
    }

    public TEntity Patch(TEntity entity)
    {
        if (entity != null)
        {
            entity = Stamp(entity);
            RemoteContext.Command(CommandType.PATCH, entity, Name);
            return entity;
        }
        return null;
    }

    public override TEntity Add(TEntity entity)
    {
        if (entity != null)
        {
            entity = Sign(entity);
            RemoteContext.Command(CommandType.POST, entity, Name);
            return entity;
        }
        return null;
    }

    public override IEnumerable<TEntity> Add(IEnumerable<TEntity> entity)
    {
        foreach (TEntity e in entity)
            yield return Add(e);
    }

    public override IEnumerable<TEntity> Put(
        IEnumerable<TEntity> entities,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate,
        params Func<TEntity, Expression<Func<TEntity, bool>>>[] conditions
    )
    {
        ISeries<TEntity> deck = null;
        if (predicate != null)
        {
            var query = Query;
            deck = entities.SelectMany(e => Query.Where(predicate(e))).ToCatalog();
        }
        else
        {
            var dtos = entities.ToCatalog();
            deck = lookup<TEntity>(entities).ToCatalog();
            if (deck.Count < dtos.Count)
                deck.Add(
                    this[
                        Query.Where(
                            p =>
                                dtos.Where(id => !deck.ContainsKey(id))
                                    .Select(id => id.Id)
                                    .Contains(p.Id)
                        )
                    ]
                );
        }

        foreach (var entity in entities)
        {
            foreach (var condition in conditions)
                if (!Query.Any(condition(entity)))
                    yield return null;

            if (deck.TryGet(entity.Id, out TEntity settin))
            {
                RemoteContext.UpdateObject(settin);
                yield return Stamp(entity.PatchTo(settin, PatchingInvoker));
            }
            else
                yield return Add(entity);
        }
    }

    public override IAsyncEnumerable<TEntity> AddAsync(IEnumerable<TEntity> entity)
    {
        return entity.ForEachAsync((e) => Add(e));
    }

    public override Task<TEntity> Find(params object[] keys)
    {
        return lookupAsync(keys);
    }

    public async Task<IEnumerable<TEntity>> FindMany(params object[] keys)
    {
        var _batch = new Chain<DataServiceRequest>();
        foreach (object key in keys)
        {
            if (key.GetType().IsAssignableTo(typeof(object[])))
                _batch.Put(key, RemoteContext.CreateQuery<TEntity>(KeyString((object[])key), true));
            else
                _batch.Put(key, RemoteContext.CreateQuery<TEntity>(KeyString(key), true));
        }
        return (await RemoteContext.ExecuteBatchAsync(_batch.ToArray())).SelectMany(
            o => o as QueryOperationResponse<TEntity>
        );
    }

    public string KeyString(params object[] keys)
    {
        return $"{Name}({(keys.Length > 1 ? keys.Aggregate(string.Empty, (a, b) => $"{a},{b}") : keys[0])})";
    }

    public override TEntity NewEntry(params object[] parameters)
    {
        TEntity entity = Sign(typeof(TEntity).New<TEntity>(parameters));
        Add(entity);
        return entity;
    }

    public DataServiceQuerySingle<TEntity> FindQuerySingle(params object[] keys)
    {
        if (keys != null)
            return Context.CreateFunctionQuerySingle<TEntity>(KeyString(keys), null, true);
        return null;
    }

    public IQueryable<TEntity> FindQuery(object[] keys)
    {
        if (keys != null)
            return Query.WhereIn(e => e.Id, keys);
        return null;
    }

    public override IQueryable<TEntity> AsQueryable()
    {
        return Query;
    }

    public OpenDataContext Context => RemoteContext;

    public override string Name => Context.GetMappedName(ElementType);

    public override string FullName => Context.GetMappedFullName(ElementType);

    public override DataServiceQuery<TEntity> Query =>
        RemoteContext.CreateQuery<TEntity>(Name, true);

    public virtual async Task<ISeries<TEntity>> LoadAsync(int offset = 0, int limit = 0)
    {
        if (limit > 0)
        {
            return await Query.Skip(offset).Take(limit).ToListingAsync();
        }
        else
        {
            return await Query.ToListingAsync();
        }
    }

    public void SetAuthorizationToken(string token)
    {
        RemoteContext.SetAuthorization(token);
    }

    protected override async Task<int> SaveAsTransaction(CancellationToken token = default)
    {
        try
        {
            return (
                await RemoteContext.SaveChangesAsync(
                    SaveChangesOptions.BatchWithSingleChangeset,
                    token
                )
            ).Count();
        }
        catch (Exception e)
        {
            RemoteContext.Failure<Datalog>(
                $"{$"Fail on update dataservice as singlechangeset, using context:{RemoteContext.GetType().Name}, "}{$"TimeStamp: {DateTime.Now.ToString()}"}",
                ex: e
            );
        }

        return -1;
    }

    protected override async Task<int> SaveChanges(CancellationToken token = default)
    {
        try
        {
            return (
                await RemoteContext.SaveChangesAsync(
                    SaveChangesOptions.BatchWithIndependentOperations
                        | SaveChangesOptions.ContinueOnError,
                    token
                )
            ).Count();
        }
        catch (Exception e)
        {
            RemoteContext.Failure<Datalog>(
                $"{$"Fail on update dataservice as independent operations, using context:{RemoteContext.GetType().Name}, "}{$"TimeStamp: {DateTime.Now.ToString()}"}",
                ex: e
            );
        }

        return -1;
    }

    public override object TracePatching(
        object source,
        string propertyName,
        object target = null,
        Type type = null
    )
    {
        if (target == null)
        {
            if (!RemoteContext.EntityTracker.Entities.Select(e => ((IIdentifiable)e.Entity).Id).Contains(((IIdentifiable)((IProxy)source).Target).Id))
                RemoteContext.LoadProperty(((IProxy)source).Target, propertyName);
            target = ((IProxy)source)[propertyName];
            if (target == null)
                ((IProxy)source)[propertyName] = target = type.New();
        }

        return target;
    }

    public override object TraceAdding(
        object source,
        string propertyName,
        object target = null,
        Type type = null
    )
    {
        if (target == null)
        {
            if (!RemoteContext.EntityTracker.Entities.Select(e => ((IIdentifiable)e.Entity).Id).Contains(((IIdentifiable)((IProxy)source).Target).Id))
                target = ((IProxy)source)[propertyName];
            if (target == null)
                ((IProxy)source)[propertyName] = target = type.New();
        }
        return target;
    }
}
