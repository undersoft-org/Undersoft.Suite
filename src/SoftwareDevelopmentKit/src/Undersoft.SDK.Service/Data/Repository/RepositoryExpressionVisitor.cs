using System.Linq.Expressions;
using Undersoft.SDK.Service.Data.Repository;

namespace Undersoft.SDK.Service.Data.Repository
{
    internal class RepositoryExpressionVisitor : ExpressionVisitor
    {
        private readonly IQueryable newRoot;

        public RepositoryExpressionVisitor(IQueryable newRoot)
        {
            this.newRoot = newRoot;
        }

        protected override Expression VisitConstant(ConstantExpression node) =>
            node.Type.BaseType != null
            && node.Type.BaseType.IsGenericType
            && node.Type.BaseType.GetGenericTypeDefinition() == typeof(Repository<>)
                ? Expression.Constant(newRoot)
                : node;
    }
}
