namespace System
{
    public interface ITransitObject
    {
        object Locate(object path = null);

        object Merge(object source, string name = null);
    }
}
