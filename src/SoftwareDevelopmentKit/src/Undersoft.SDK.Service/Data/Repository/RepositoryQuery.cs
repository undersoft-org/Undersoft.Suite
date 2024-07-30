using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Undersoft.SDK.Utilities;

namespace Undersoft.SDK.Service.Data.Repository;

public abstract partial class Repository<TEntity> : IRepositoryQuery<TEntity> where TEntity : class, IOrigin, IInnerProxy
{
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
