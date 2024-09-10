using Undersoft.SDK.Uniques;

namespace Undersoft.SDK
{
    public interface IOrigin<V> : IOrigin
    {
    }

    public interface IOrigin : IIdentifiable
    {
        long OriginId { get; set; }
        string CodeNo { get; set; }
        DateTime Created { get; set; }
        string Creator { get; set; }
        DateTime Modified { get; set; }
        string Modifier { get; set; }
        string Label { get; set; }
        int Index { get; set; }
        string TypeName { get; set; }
        DateTime Time { get; set; }
        long AutoId();
        byte GetPriority();
        TEntity Sign<TEntity>(TEntity entity) where TEntity : class, IOrigin;
        TEntity Stamp<TEntity>(TEntity entity) where TEntity : class, IOrigin;
        void GetFlag(DataFlags state);
        void SetFlag(DataFlags state, bool flag);
        long SetId(long id);
        long SetId(object id);
    }
}
