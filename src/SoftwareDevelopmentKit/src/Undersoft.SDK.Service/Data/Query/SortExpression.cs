using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Query;

using Proxies;
using Rubrics;

public class SortExpression<TEntity>
{
    private ProxyCreator sleeve;
    public IRubrics Rubrics;
    private Expression<Func<TEntity, object>> sortExpression { get; set; }

    public IList<Sort<TEntity>> SortItems { get; } = new List<Sort<TEntity>>();

    public SortExpression()
    {
        sleeve = ProxyFactory.GetCompiledCreator<TEntity>();
        Rubrics = sleeve.Rubrics;
    }
    public SortExpression(Expression<Func<TEntity, object>> expressionItem, SortDirection direction) : this()
    {
        Add(new Sort<TEntity>(expressionItem, direction));
    }
    public SortExpression(params Sort<TEntity>[] sortItems) : this()
    {
        sortItems.ForEach(fi => Add(fi));
    }
    public SortExpression(IEnumerable<Sort<TEntity>> sortItems) : this()
    {
        sortItems.ForEach(fi => Add(fi));
    }

    public SortExpression(IEnumerable<Sort> sortItems) : this()
    {
        sortItems.ForEach(fi => Add(new Sort<TEntity>(fi))).Commit();
    }

    public SortExpression(IEnumerable<SortItem> sortItems) : this()
    {
        sortItems.ForEach(fi => Add(new Sort<TEntity>(fi))).ToList();
    }

    public IQueryable<TEntity> Sort(IQueryable<TEntity> query)
    {
        return Sort(query, SortItems);
    }
    public IQueryable<TEntity> Sort(IQueryable<TEntity> query, IEnumerable<Sort<TEntity>> sortItems)
    {

        if (sortItems != null && sortItems.Any())
        {
            if (!SortItems.Any())
                sortItems.ForEach(fi => Add(fi));

            bool first = true;
            IOrderedQueryable<TEntity> orderedQuery = null;
            foreach (var sortItem in SortItems)
            {
                if (sortItem.Direction.Equals(SortDirection.Ascending))
                {
                    orderedQuery = first ? query.OrderBy(sortItem.ExpressionItem) : orderedQuery.ThenBy(sortItem.ExpressionItem);
                }
                else
                {
                    orderedQuery = first
                        ? query.OrderByDescending(sortItem.ExpressionItem)
                        : orderedQuery.ThenByDescending(sortItem.ExpressionItem);
                }

                first = false;
            }

            return orderedQuery;
        }
        else
        {
            return query;
        }
    }

    public Sort<TEntity> Add(Sort<TEntity> item)
    {
        item.Assign(this);
        SortItems.Add(item);
        return item;
    }
    public IEnumerable<Sort<TEntity>> Add(IEnumerable<Sort<TEntity>> sortItems)
    {
        sortItems.ForEach(fi => Add(fi));
        return SortItems;
    }
}