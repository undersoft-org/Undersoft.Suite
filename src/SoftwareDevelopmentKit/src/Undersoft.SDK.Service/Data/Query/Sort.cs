using System.Linq.Expressions;

namespace Undersoft.SDK.Service.Data.Query;
using Rubrics;

public class Sort<TEntity> : Sort
{
    private SortExpression<TEntity> sortExpression;

    public Sort() { }

    public Sort(Sort sort) : base(sort) { }

    public Sort(SortItem item) : base(item) { }

    public Sort(MemberRubric sortedRubric, SortDirection direction = SortDirection.Ascending) : base(sortedRubric, direction) { }

    public Sort(string rubricName, string direction = "Ascending") : base(rubricName, direction) { }

    public Sort(
        Expression<Func<TEntity, object>> expressionItem,
        SortDirection direction = SortDirection.Ascending
    )
    {
        ExpressionItem = expressionItem;
        Direction = direction;
    }

    public void Assign(SortExpression<TEntity> sortExpression)
    {
        var fe = sortExpression;
        this.sortExpression = fe;
        if (fe.Rubrics.TryGet(Property, out MemberRubric rubric))
        {
            Rubric = rubric;
            var parameter = Expression.Parameter(typeof(TEntity), "entity");
            var property = Expression.Property(parameter, rubric.RubricName);
            ExpressionItem = Expression.Lambda<Func<TEntity, object>>(property, parameter);
        }
    }

    public Expression<Func<TEntity, object>> ExpressionItem { get; set; }

    public bool Compare(Sort<TEntity> term)
    {
        if (Property != term.Property || Direction != term.Direction)
            return false;

        return true;
    }
}

public class Sort
{
    public Sort() { }

    public Sort(Sort sort)
    {
        Direction = sort.Direction;
        Rubric = sort.Rubric;
        Property = sort.Property;
    }

    public Sort(MemberRubric sortedRubric, SortDirection direction = SortDirection.Ascending)
    {
        Direction = direction;
        Rubric = sortedRubric;
        Property = Rubric.Name;
    }

    public Sort(string rubricName, string direction = "Ascending")
    {
        Property = rubricName;
        SortDirection sortDirection;
        Enum.TryParse(direction, true, out sortDirection);
        Direction = sortDirection;
    }

    public Sort(SortItem item) : this(item.Property, item.Direction) { }

    public SortDirection Direction { get; set; }

    public int Position { get; set; }

    public string Property { get; set; }

    public MemberRubric Rubric { get; set; }
}
