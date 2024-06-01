namespace Undersoft.SDK.Updating
{
    public interface IUpdater<T> : IUpdater
    {
        T Devisor { get; set; }

        new T Clone();
        T Patch(T item);
        T PatchFrom(T source);
        T Put(T item);
        T PutFrom(T source);
    }
}
