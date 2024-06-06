namespace Undersoft.SDK.Uniques
{
    public interface IUniqueOne<T>
    {
        IQueryable<T> Queryable { get; }
    }
}
