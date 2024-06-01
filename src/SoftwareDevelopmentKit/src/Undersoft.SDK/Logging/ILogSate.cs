namespace Undersoft.SDK.Logging
{
    public interface ILogSate
    {
        object DataObject { get; set; }
        Exception Exception { get; set; }
    }

    public interface ILogSate<T>
    {
        T DataObject { get; set; }
        Exception Exception { get; set; }
    }
}
