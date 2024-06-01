using System.Linq.Expressions;
using System.Reflection;

namespace Undersoft.SDK.Service.Data.Store.Repository;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;

public class StoreRepositoryExpressionProvider<TEntity> : IQueryProvider
    where TEntity : class, IOrigin
{
    private readonly Type queryType;
    private IDataStoreContext _context;

    public StoreRepositoryExpressionProvider(IDataStoreContext context)
    {
        queryType = typeof(StoreRepository<>);
        _context = context;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        try
        {
            return queryType.MakeGenericType(typeof(TEntity)).New<IQueryable>(this, expression);
        }
        catch (TargetInvocationException tie)
        {
            throw tie.InnerException;
        }
    }

    public IQueryable<T> CreateQuery<T>(Expression expression)
    {
        return queryType.MakeGenericType(typeof(TEntity)).New<IQueryable<T>>(this, expression);
    }

    public object Execute(Expression expression)
    {
        try
        {
            return GetType().GetGenericMethod(nameof(Execute)).Invoke(this, new[] { expression });
        }
        catch (TargetInvocationException tie)
        {
            throw tie.InnerException;
        }
    }

    public TResult Execute<TResult>(Expression expression)
    {
        IQueryable<TEntity> newRoot = _context.Query<TEntity>();
        var treeCopier = new StoreRepositoryExpressionVisitor(newRoot);
        var newExpressionTree = treeCopier.Visit(expression);
        var isEnumerable =

                typeof(TResult).IsGenericType
                && typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<TEntity>)

            || typeof(TResult).Name == "IEnumerable`1";
        if (isEnumerable)
        {
            return (TResult)newRoot.Provider.CreateQuery(newExpressionTree);
        }
        var result = newRoot.Provider.Execute(newExpressionTree);
        return (TResult)result;
    }

    public TResult ExecuteAsync<TResult>(
        Expression expression,
        CancellationToken cancellationToken = default
    )
    {
        var task = Task.FromResult(Execute<TResult>(expression));
        task.ConfigureAwait(true);
        return task.Result;
    }
}
