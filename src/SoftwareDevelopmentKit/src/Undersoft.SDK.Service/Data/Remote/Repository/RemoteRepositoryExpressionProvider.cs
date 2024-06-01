using Microsoft.EntityFrameworkCore.Query;
using Microsoft.OData.Client;
using System.Linq.Expressions;
using System.Reflection;

namespace Undersoft.SDK.Service.Data.Remote.Repository;

public class RemoteRepositoryExpressionProvider<TEntity> : IAsyncQueryProvider
    where TEntity : class, IOrigin
{
    private readonly Type queryType;
    private IQueryable<TEntity> query;

    public RemoteRepositoryExpressionProvider(DataServiceQuery<TEntity> targetDsSet)
    {
        queryType = typeof(RemoteRepository<>);
        query = targetDsSet;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        var elementType = expression.Type.GetEnumerableElementType();
        try
        {
            return queryType.MakeGenericType(elementType).New<IQueryable>(this, expression);
        }
        catch (TargetInvocationException tie)
        {
            throw tie.InnerException;
        }
    }

    public IQueryable<T> CreateQuery<T>(Expression expression)
    {
        return queryType
            .MakeGenericType(expression.Type.GetEnumerableElementType())
            .New<IQueryable<T>>(this, expression);
    }

    public object Execute(Expression expression)
    {
        try
        {
            return GetType()
                .GetGenericMethod(nameof(Execute))
                .Invoke(this, new[] { expression });
        }
        catch (TargetInvocationException tie)
        {
            throw tie.InnerException;
        }
    }

    public TResult Execute<TResult>(Expression expression)
    {
        IQueryable<TEntity> newRoot = query;
        var treeCopier = new RemoteRepositoryExpressionVisitor(newRoot);
        var newExpressionTree = treeCopier.Visit(expression);
        var isEnumerable =
            typeof(TResult).IsGenericType
            && typeof(TResult).GetGenericTypeDefinition() == typeof(IEnumerable<>);
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
        return Task.FromResult(Execute<TResult>(expression)).Result;
    }
}
