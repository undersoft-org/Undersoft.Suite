namespace Undersoft.SDK
{
    using System.Linq.Expressions;
    using Undersoft.SDK.Uniques;

    public class Sorter : ISorter
    {
        public Sorter()
        {
        }

        public Sorter(LambdaExpression expression, SortDirection direction = SortDirection.Ascending) : this(expression.GetMemberName(), direction)
        {
        }

        public Sorter(string member, SortDirection direction = SortDirection.Ascending)
        {
            Direction = direction;
            Member = member;
            Id = member.UniqueKey();
            TypeId = (int)Direction;
        }

        public void SetMember(LambdaExpression expression)
        {
            Member = expression.GetMemberName();
        }

        public void SetDirection(string operand)
        {
            if (Enum.TryParse<SortDirection>(operand, true, out SortDirection _operand))
                Direction = _operand;
            Direction = SortDirection.Ascending;
        }

        public Expression<Func<TEntity, object>> GetSortMemberExpression<TEntity>()
        {
            return Member.GetMemberExpression<TEntity>();
        }

        public long Id { get; set; }

        public long TypeId { get; set; }

        public string Member { get; set; }

        public SortDirection Direction { get; set; }
    }
}
