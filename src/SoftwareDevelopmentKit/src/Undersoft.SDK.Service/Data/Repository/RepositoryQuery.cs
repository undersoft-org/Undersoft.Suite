using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Undersoft.SDK.Series;
using Undersoft.SDK.Uniques;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using Undersoft.SDK.Service.Data.Query;
using Undersoft.SDK.Service.Data.Repository;
using Undersoft.SDK.Proxies;

namespace Undersoft.SDK.Service.Data.Repository;

public abstract partial class Repository<TEntity> : IRepositoryQuery<TEntity> where TEntity : class, IOrigin, IInnerProxy
{
    public virtual Task<IList<TResult>> Filter<TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector
    )
    {
        return Task.Run(
            () =>
                (IList<TResult>)(
                    (take > 0)
                        ? Query.Select(selector).Skip(skip).Take(take).ToRegistry()
                        : Query.Select(selector).ToArray()
                ),
            Cancellation
        );
    }

    public virtual Task<IList<TResult>> Filter<TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(
            () => (IList<TResult>)this[skip, take, this[expanders]].Select(selector).ToArray(),
            Cancellation
        );
    }

    public virtual Task<IList<TResult>> Filter<TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector
    )
    {
        return Task.Run(
            () => (IList<TResult>)this[skip, take, this[predicate]].Select(selector).ToArray(),
            Cancellation
        );
    }

    public virtual Task<IList<TResult>> Filter<TResult>(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(
            () =>
                (IList<TResult>)
                    this[skip, take, this[predicate, sortTerms, expanders]]
                        .Select(selector)
                        .ToArray(),
            Cancellation
        );
    }

    public virtual Task<ISeries<TEntity>> Filter(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms
    )
    {
        return Task.Run(() => this[skip, take, sortTerms], Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Filter(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => (ISeries<TEntity>)this[skip, take, sortTerms, expanders].ToChain(), Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Filter(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate
    )
    {
        return Task.Run(() => this[skip, take, predicate], Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Filter(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms
    )
    {
        return Task.Run(() => this[skip, take, predicate, sortTerms], Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Filter(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => this[skip, take, predicate, expanders], Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Filter(
        int skip,
        int take,
        Expression<Func<TEntity, bool>> predicate,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => this[skip, take, predicate, sortTerms, expanders], Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Filter(IQueryable<TEntity> query)
    {
        return Task.Run(() => query.ToRegistry() as ISeries<TEntity>, Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Get(
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => this[0, 0, expanders], Cancellation);
    }

    public virtual Task<IList<TResult>> Get<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(
            () => (IList<TResult>)this[0, 0, this[expanders]].Select(selector).ToArray(),
            Cancellation
        );
    }

    public virtual Task<IList<TResult>> Get<TResult>(
        Expression<Func<TEntity, TResult>> selector
    )
    {
        return Filter(0, 0, selector);
    }

    public virtual Task<ISeries<TEntity>> Get(
        int skip,
        int take,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => this[skip, take, expanders], Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Get(
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => (ISeries<TEntity>)this[0, 0, sortTerms, expanders].ToChain(), Cancellation);
    }

    public virtual Task<ISeries<TEntity>> Get(
        int skip,
        int take,
        SortExpression<TEntity> sortTerms,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => (ISeries<TEntity>)this[skip, take, sortTerms, expanders].ToChain(), Cancellation);
    }

    public virtual async Task<TEntity> Find(params object[] keys)
    {
        return await Task.Run(() => this[keys], Cancellation);
    }

    public virtual Task<TEntity> Find(
        object[] keys,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => this[keys, expanders], Cancellation);
    }

    public virtual Task<TResult> Find<TResult>(
        object[] keys,
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    ) where TResult : class
    {
        return Task.Run(
            () =>
            {
                var entity = this[keys];
                if (entity == null)
                    return null;
                var query = entity.ToQueryable();
                if (expanders != null)
                {
                    foreach (var expander in expanders)
                    {
                        query = query.Include(expander);
                    }
                }
                return query.Select(selector).FirstOrDefault();
            },
            Cancellation
        );
    }

    public virtual Task<TEntity> Find(
        Expression<Func<TEntity, bool>> predicate,
        bool reverse = false
    )
    {
        return Task.Run(() => this[reverse, predicate], Cancellation);
    }

    public virtual Task<TEntity> Find(
        Expression<Func<TEntity, bool>> predicate,
        bool reverse = false,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(() => this[reverse, predicate, expanders], Cancellation);
    }

    public virtual Task<TResult> Find<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector
    )
    {
        return Task.Run(() => this[predicate].Select(selector).FirstOrDefault(), Cancellation);
    }

    public virtual Task<TResult> Find<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        params Expression<Func<TEntity, object>>[] expanders
    )
    {
        return Task.Run(
            () => this[predicate, expanders].Select(selector).FirstOrDefault(),
            Cancellation
        );
    }

    public virtual async Task<bool> Exist(
        TEntity entity,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate
    )
    {
        return await Exist(predicate.Invoke(entity));
    }

    public virtual async Task<bool> Exist(Expression<Func<TEntity, bool>> predicate)
    {
        return await Task.Run(() => Query.Any(predicate), Cancellation);
    }

    public virtual async Task<bool> Exist<TException>(
        Expression<Func<TEntity, bool>> predicate,
        string message
    ) where TException : Exception
    {
        bool exist = await Exist(predicate);
        if (exist)
            throw typeof(TException).New<TException>(message);
        return false;
    }

    public virtual async Task<bool> Exist<TException>(object instance, string message)
        where TException : Exception
    {
        return await Task.Run(
            () =>
            {
                Type exType = typeof(TException);

                if (instance != null)
                {
                    Type type = instance.GetType();
                    if (type == typeof(string))
                    {
                        if (!(string.IsNullOrWhiteSpace((string)instance)))
                            throw exType.New<TException>(message);
                    }
                    else if (!(instance.Equals(type.Default())))
                    {
                        throw exType.New<TException>(message);
                    }
                }

                return false;
            },
            Cancellation
        );
    }

    public virtual async Task<bool> Exist(
        Type exceptionType,
        Expression<Func<TEntity, bool>> predicate,
        string message
    )
    {
        bool exist = await Exist(predicate);
        if (exist)
            throw exceptionType.New<Exception>(message);
        return false;
    }

    public virtual async Task<bool> Exist(Type exceptionType, object instance, string message)
    {
        return await Task.Run(
            () =>
            {
                Type type = instance.GetType();
                Type exType = exceptionType;
                if (type == typeof(string))
                {
                    if (!(string.IsNullOrWhiteSpace((string)instance)))
                        throw exType.New<Exception>(message);
                }
                else if (!(instance.Equals(type.Default())))
                {
                    throw exType.New<Exception>(message);
                }

                return false;
            },
            Cancellation
        );
    }

    public virtual async Task<bool> NotExist(
        TEntity entity,
        Func<TEntity, Expression<Func<TEntity, bool>>> predicate
    )
    {
        return await NotExist(predicate.Invoke(entity));
    }

    public virtual async Task<bool> NotExist(Expression<Func<TEntity, bool>> predicate)
    {
        return await Task.Run(() => !Query.Any(predicate), Cancellation);
    }

    public virtual async Task<bool> NotExist(
        Type exceptionType,
        Expression<Func<TEntity, bool>> predicate,
        string message
    )
    {
        bool notexist = await NotExist(predicate);
        if (notexist)
            throw exceptionType.New<Exception>(message);
        return false;
    }

    public virtual async Task<bool> NotExist(
        Type exceptionType,
        object instance,
        string message
    )
    {
        return await Task.Run(
            () =>
            {
                Type type = instance.GetType();
                Type exType = exceptionType;
                if (type == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace((string)instance))
                        throw exType.New<Exception>(message);
                }
                else if (instance.Equals(type.Default()))
                {
                    throw exType.New<Exception>(message);
                }

                return false;
            },
            Cancellation
        );
    }

    public virtual async Task<bool> NotExist<TException>(
        Expression<Func<TEntity, bool>> predicate,
        string message
    ) where TException : Exception
    {
        bool notexist = await NotExist(predicate);
        if (notexist)
            throw typeof(TException).New<TException>(message);
        return false;
    }

    public virtual async Task<bool> NotExist<TException>(object instance, string message)
        where TException : Exception
    {
        return await Task.Run(
            () =>
            {
                Type exType = typeof(TException);

                if (instance == null)
                    throw exType.New<TException>(message);

                Type type = instance.GetType();

                if (type == typeof(string))
                {
                    if (string.IsNullOrWhiteSpace((string)instance))
                        throw exType.New<TException>(message);
                }
                else if (instance.Equals(type.Default()))
                {
                    throw exType.New<TException>(message);
                }

                return false;
            },
            Cancellation
        );
    }

    public IQueryable<TEntity> Sort(
        IQueryable<TEntity> query,
        SortExpression<TEntity> sortTerms
    )
    {
        return sortTerms != null && sortTerms.SortItems.Any() ? sortTerms.Sort(query) : query;
    }
}
