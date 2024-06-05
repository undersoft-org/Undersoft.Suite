using System.Reflection;

namespace System.Linq
{
    using Collections.Generic;
    using Expressions;

    public enum SortDirection
    {
        None,
        Ascending,
        Descending
    }

    public static class LinqExtension
    {
        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> _leftside,
            Expression<Func<T, bool>> _rightside
        )
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.Invoke(_leftside, param),
                    Expression.Invoke(_rightside, param)
                ),
                param
            );
        }

        public static IQueryable<T> ToQueryable<T>(this T entity)
        {
            return ((entity == null) ? new T[0] : new T[1] { entity }).AsQueryable();
        }

        public static IEnumerable<T> ToEnumerable<T>(this T entity)
        {
            return ((entity == null) ? new T[0] : new T[1] { entity }).AsEnumerable();
        }

        public static IEnumerable<T> Concentrate<T>(params IEnumerable<T>[] List)
        {
            foreach (IEnumerable<T> item in List)
            {
                foreach (T subitem in item)
                {
                    yield return subitem;
                }
            }
        }

        public static TValue[] Collect<TItem, TValue>(
            this IEnumerable<TItem> source,
            Expression<Func<TItem, TValue>> valueSelector
        )
        {
            return source.Select(valueSelector.Compile()).ToArray();
        }

        public static TValue[] Collect<TItem, TValue>(
            this IQueryable<TItem> source,
            Expression<Func<TItem, TValue>> valueSelector
        )
        {
            return source.Select(valueSelector).ToArray();
        }

        public static IEnumerable<TItem> ContainsIn<TItem, TValue>(
            this IEnumerable<TItem> source,
            Expression<Func<TItem, TValue>> valueSelector,
            IEnumerable<TValue> values
        )
        {
            return source.Where(GetWhereInExpression(valueSelector, values).Compile());
        }

        public static void ForKeys<TSource, TKey>(
            this IEnumerable<TSource> source,
            Action<TKey> applyBehavior,
            Func<TSource, TKey> keySelector
        )
        {
            foreach (var item in source)
            {
                var target = keySelector(item);
                applyBehavior(target);
            }
        }

        public static Expression<Func<T, bool>> Greater<T>(
            this Expression<Func<T, bool>> _leftside,
            Expression<Func<T, bool>> _rightside
        )
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThan(
                    Expression.Invoke(_leftside, param),
                    Expression.Invoke(_rightside, param)
                ),
                param
            );
        }

        public static Expression<Func<T, bool>> GreaterOrEqual<T>(
            this Expression<Func<T, bool>> _leftside,
            Expression<Func<T, bool>> _rightside
        )
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(
                Expression.GreaterThanOrEqual(
                    Expression.Invoke(_leftside, param),
                    Expression.Invoke(_rightside, param)
                ),
                param
            );
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            JoinComparerProvider<TInner, TKey> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector
        )
        {
            return outer.Join(
                inner.Inner,
                outerKeySelector,
                innerKeySelector,
                resultSelector,
                inner.Comparer
            );
        }

        public static Expression<Func<T, bool>> Less<T>(
            this Expression<Func<T, bool>> _leftside,
            Expression<Func<T, bool>> _rightside
        )
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(
                Expression.LessThan(
                    Expression.Invoke(_leftside, param),
                    Expression.Invoke(_rightside, param)
                ),
                param
            );
        }

        public static Expression<Func<T, bool>> LessOrEqual<T>(
            this Expression<Func<T, bool>> _leftside,
            Expression<Func<T, bool>> _rightside
        )
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(
                Expression.LessThanOrEqual(
                    Expression.Invoke(_leftside, param),
                    Expression.Invoke(_rightside, param)
                ),
                param
            );
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> _leftside,
            Expression<Func<T, bool>> _rightside
        )
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    Expression.Invoke(_leftside, param),
                    Expression.Invoke(_rightside, param)
                ),
                param
            );
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            SortDirection sortOrder,
            IComparer<TKey> comparer
        )
        {
            if (sortOrder == SortDirection.Ascending)
                return source.OrderBy(keySelector);
            else
                return source.OrderByDescending(keySelector);
        }

        public static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
            this IOrderedQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            SortDirection sortOrder,
            IComparer<TKey> comparer
        )
        {
            if (sortOrder == SortDirection.Ascending)
                return source.OrderBy(keySelector);
            else
                return source.OrderByDescending(keySelector);
        }

        public static IQueryable<TItem> WhereIn<TItem, TValue>(
            this IQueryable<TItem> source,
            Expression<Func<TItem, TValue>> propertySelector,
            IEnumerable<TValue> values
        )
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<TItem> WhereIn<TItem, TValue>(
            this IQueryable<TItem> source,
            Expression<Func<TItem, TValue>> propertySelector,
            params TValue[] values
        )
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<TItem> ExceptIn<TItem, TValue>(
            this IQueryable<TItem> source,
            Expression<Func<TItem, TValue>> propertySelector,
            IEnumerable<TValue> values
        )
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<TItem> ExceptIn<TItem, TValue>(
            this IQueryable<TItem> source,
            Expression<Func<TItem, TValue>> propertySelector,
            params TValue[] values
        )
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static JoinComparerProvider<T, TKey> WithComparer<T, TKey>(
            this IEnumerable<T> inner,
            IEqualityComparer<TKey> comparer
        )
        {
            return new JoinComparerProvider<T, TKey>(inner, comparer);
        }

        public static Expression<Func<TItem, bool>> GetWhereInExpression<TItem, TValue>(
            Expression<Func<TItem, TValue>> propertySelector,
            IEnumerable<TValue> values
        )
        {
            if (propertySelector == null || values == null || !values.Any())
                return null;

            ParameterExpression p = propertySelector.Parameters.Single();

            var equals = values.Select(
                value =>
                    (Expression)
                        Expression.Equal(
                            propertySelector.Body,
                            Expression.Constant(value, typeof(TValue))
                        )
            );
            var body = equals.Aggregate<Expression>(
                (accumulate, equal) => Expression.Or(accumulate, equal)
            );

            return Expression.Lambda<Func<TItem, bool>>(body, p);
        }

        public static Expression<Func<TItem, bool>> GetExceptInExpression<TItem, TValue>(
            Expression<Func<TItem, TValue>> propertySelector,
            IEnumerable<TValue> values
        )
        {
            if (propertySelector == null || values == null || !values.Any())
                return null;

            ParameterExpression p = propertySelector.Parameters.Single();

            var equals = values.Select(
                value =>
                    (Expression)
                        Expression.NotEqual(
                            propertySelector.Body,
                            Expression.Constant(value, typeof(TValue))
                        )
            );
            var body = equals.Aggregate<Expression>(
                (accumulate, equal) => Expression.And(accumulate, equal)
            );

            return Expression.Lambda<Func<TItem, bool>>(body, p);
        }

        public static Expression<Func<TItem1, bool>> GetEqualityExpression<TItem0, TItem1, TValue>(
            Expression<Func<TItem1, TValue>> propertySelector,
            Func<TItem0, TValue> valueSelector,
            TItem0 origin
        )
        {
            ParameterExpression p = propertySelector.Parameters.Single();

            var body = (Expression)
                Expression.Equal(
                    propertySelector.Body,
                    Expression.Constant(valueSelector.Invoke(origin), typeof(TValue))
                );

            return Expression.Lambda<Func<TItem1, bool>>(body, p);
        }

        public class Filter
        {
            public string Property { get; set; }

            public string Value { get; set; }
        }

        public static Expression GetFilterExpression(IEnumerable<Filter> filters, Type modelType)
        {
            var parameter = Expression.Parameter(modelType, "model");
            Expression filterExpression = null;
            foreach (var filter in filters)
            {
                var property = Expression.Property(parameter, filter.Property);
                var constant = Expression.Constant(filter.Value);
                Expression comparison;

                if (property.Type == typeof(string))
                {
                    comparison = Expression.Call(property, "Contains", Type.EmptyTypes, constant);
                }
                else
                {
                    //Convert values based on property type
                    constant = Expression.Constant(Convert.ToInt32(filter.Value));
                    comparison = Expression.Equal(property, constant);
                }

                filterExpression = filterExpression == null
                    ? comparison
                    : Expression.And(filterExpression, comparison);
            }
            return filterExpression;
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

        /// <summary>
        /// Get metadata of property referenced by expression.
        /// </summary>
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

        public sealed class JoinComparerProvider<T, TKey>
        {
            internal JoinComparerProvider(IEnumerable<T> inner, IEqualityComparer<TKey> comparer)
            {
                Inner = inner;
                Comparer = comparer;
            }

            public IEqualityComparer<TKey> Comparer { get; private set; }

            public IEnumerable<T> Inner { get; private set; }
        }
    }
}
