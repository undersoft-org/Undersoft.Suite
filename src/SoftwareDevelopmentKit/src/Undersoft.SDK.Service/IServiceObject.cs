namespace Undersoft.SDK.Service
{
    public interface IServiceObject<out T> : IServiceObject where T : class
    {
       new  T Value { get; }
    }

    public interface IServiceObject
    {
        object Value { get; }
    }
}