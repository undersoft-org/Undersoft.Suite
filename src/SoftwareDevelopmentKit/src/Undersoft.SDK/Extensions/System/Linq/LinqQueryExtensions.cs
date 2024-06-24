using System.Reflection;

namespace System.Linq
{
    using Collections.Generic;
    using Expressions;
    using Undersoft.SDK;

    public static class LinqQueryExtensions
    {
        public static MemberExpression GetNestedMemberExpression(this string memberAccess, Expression memberHolder)
        {
            Expression body = memberHolder;
            foreach (var member in memberAccess.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            return (MemberExpression)body;
        }

        public static LambdaExpression GetMemberExpression(this string memberAccess, Type modelType)
        {
            var parameter = Expression.Parameter(modelType, "model");
            MemberExpression body = GetNestedMemberExpression(memberAccess, parameter);
            var expression = Expression.Lambda(body, parameter);
            return expression;
        }

        public static LambdaExpression GetMemberExpression(this string memberAccess, Type modelType, out Type propertyType)
        {
            var parameter = Expression.Parameter(modelType, "model");
            MemberExpression body = GetNestedMemberExpression(memberAccess, parameter);
            propertyType = ((PropertyInfo)body.Member).PropertyType;
            var expression = Expression.Lambda(body, parameter);
            return expression;
        }

        public static Expression<Func<TEntity, TMember>> GetMemberExpression<TEntity, TMember>(this string memberAccess, LambdaExpression lambda)
        {
            return Expression.Lambda<Func<TEntity, TMember>>(lambda.Body, lambda.Parameters);
        }

        public static Expression<Func<TEntity, object>> GetMemberExpression<TEntity>(this string memberAccess)
        {
            var lambda = memberAccess.GetMemberExpression(typeof(TEntity));
            return Expression.Lambda<Func<TEntity, object>>(lambda.Body, lambda.Parameters);
        }

        public static IQueryable<TEntity> GetSortQuery<TEntity>(this IQueryable<TEntity> query, IEnumerable<Sorter> sorters)
        {
            var _query = query;

            if (sorters != null && sorters.Any())
            {
                IOrderedQueryable<TEntity> orderedQuery = null;
                foreach (var sorter in sorters)
                {
                    var expression = sorter.GetSortMemberExpression<TEntity>();
                    if (sorter.Direction.Equals(SortDirection.Ascending) || sorter.Direction.Equals(SortDirection.Asc))
                    {
                        orderedQuery = orderedQuery == null ? _query.OrderBy(expression) : orderedQuery.ThenBy(expression);
                    }
                    else
                    {
                        orderedQuery = orderedQuery == null ? _query.OrderByDescending(expression) : orderedQuery.ThenByDescending(expression);
                    }
                }
                return orderedQuery;
            }
            else
            {
                return query;
            }
        }

        public static Expression GetComparisonExpression(this Filter filter, ParameterExpression parameter, Expression connectToExpression = null)
        {
            var property = filter.Member.GetNestedMemberExpression(parameter);
            Expression constant = Expression.Constant(filter.Value);
            if (property.Type.IsNullable())
                constant = Expression.Convert(constant, property.Type);

            Expression comparison;

            if (property.Type == typeof(string))
            {
                switch (filter.Operand)
                {
                    case CompareOperand.Contains:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                    case CompareOperand.NotContains:
                        comparison = Expression.Not(Expression.Call(property, "Contains", Type.EmptyTypes, constant));
                        break;
                    default:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                }
            }
            else
            {
                switch (filter.Operand)
                {
                    case CompareOperand.Equal:
                        comparison = Expression.Equal(property, constant);
                        break;
                    case CompareOperand.NotEqual:
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    case CompareOperand.Greater:
                        comparison = Expression.GreaterThan(property, constant);
                        break;
                    case CompareOperand.GreaterOrEqual:
                        comparison = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case CompareOperand.Less:
                        comparison = Expression.LessThan(property, constant);
                        break;
                    case CompareOperand.LessOrEqual:
                        comparison = Expression.LessThanOrEqual(property, constant);
                        break;
                    case CompareOperand.Contains:
                        comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                        break;
                    case CompareOperand.NotContains:
                        comparison = Expression.Not(Expression.Call(property, "Contains", Type.EmptyTypes, constant));
                        break;
                    default:
                        comparison = Expression.Equal(property, constant);
                        break;
                }
            }

            var filterExpression = connectToExpression;

            if (filterExpression == null)
                filterExpression = comparison;
            else
            {
                switch (filter.Link)
                {
                    case LinkOperand.And:
                        filterExpression = Expression.And(filterExpression, comparison);
                        break;
                    case LinkOperand.Or:
                        filterExpression = Expression.Or(filterExpression, comparison);
                        break;
                    default:
                        filterExpression = Expression.And(filterExpression, comparison);
                        break;
                }
            }
            return filterExpression;
        }

        public static LambdaExpression GetFilterExpression(this IEnumerable<Filter> filters, Type modelType)
        {
            var parameter = Expression.Parameter(modelType, "model");
            Expression filterExpression = null;
            foreach (var filter in filters)
            {
                filterExpression = GetComparisonExpression(filter, parameter, filterExpression);
            }

            return Expression.Lambda(filterExpression, parameter);
        }

        public static Expression<Func<TEntity, bool>> GetFilterExpression<TEntity>(this IEnumerable<Filter> filters)
        {
            if (filters.Any())
            {
                var lambda = filters.GetFilterExpression(typeof(TEntity));
                return Expression.Lambda<Func<TEntity, bool>>(lambda.Body, lambda.Parameters);
            }
            return default!;
        }

        public static IQueryable<TEntity> GetFilterQuery<TEntity>(this IQueryable<TEntity> query, IEnumerable<Filter> Filters)
        {
            var _query = query;

            if (Filters != null && Filters.Any())
            {
                return query.Where(Filters.GetFilterExpression<TEntity>());
            }
            else
            {
                return query;
            }
        }

        public static string GetMemberName(this LambdaExpression memberSelector)
        {
            Func<Expression, string> nameSelector = null; //recursive func
            nameSelector = e => //or move the entire thing to a separate recursive method
            {
                switch (e.NodeType)
                {
                    case ExpressionType.Parameter:
                        return ((ParameterExpression)e).Name;
                    case ExpressionType.MemberAccess:
                        return ((MemberExpression)e).Member.Name;
                    case ExpressionType.Call:
                        return ((MethodCallExpression)e).Method.Name;
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                        return nameSelector(((UnaryExpression)e).Operand);
                    case ExpressionType.Invoke:
                        return nameSelector(((InvocationExpression)e).Expression);
                    case ExpressionType.ArrayLength:
                        return "Length";
                    default:
                        throw new Exception("not a proper member selector");
                }
            };

            return nameSelector(memberSelector.Body);
        }

        public static PropertyInfo GetPropertyInfo(this LambdaExpression propertyLambda)
        {
            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda.ToString()}' refers to a method, not a property."
                );

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(
                    $"Expression '{propertyLambda.ToString()}' refers to a field, not a property."
                );

            if (!propertyLambda.Parameters.Any())
                throw new ArgumentException(
                    $"Expression '{propertyLambda.ToString()}' does not have any parameters. A property expression needs to have at least 1 parameter."
                );

            var type = propertyLambda.Parameters[0].Type;

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(
                    $"Expression '{propertyLambda.ToString()}' refers to a property that is not from type {type}."
                );

            return propInfo;
        }
    }
}
