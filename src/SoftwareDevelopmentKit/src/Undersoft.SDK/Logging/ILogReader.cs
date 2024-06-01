namespace Undersoft.SDK.Logging
{
    public interface ILogReader
    {
        void Clear(DateTime olderThen);

        LogMessage[] Read(DateTime afterDate);
    }
}
