using System.Linq;
using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Remote.Repository;

namespace Undersoft.SDK.Service.Data.Remote.Repository;

internal class RemoteRepositoryExpressionVisitor : ExpressionVisitor
{
    private readonly IQueryable newRoot;

    public RemoteRepositoryExpressionVisitor(IQueryable newRoot)
    {
        this.newRoot = newRoot;
    }

    protected override Expression VisitConstant(ConstantExpression node) =>
        node.Type.BaseType != null
        && node.Type.BaseType.IsGenericType
        && node.Type.BaseType.GetGenericTypeDefinition() == typeof(RemoteRepository<>)
            ? Expression.Constant(newRoot)
            : node;
}
