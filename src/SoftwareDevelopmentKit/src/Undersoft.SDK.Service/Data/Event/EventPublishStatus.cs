namespace Undersoft.SDK.Service.Data.Event
{
    public enum EventPublishStatus
    {
        None = 0,
        Ready = 1,
        Processing = 2,
        Complete = 3,
        Uncomplete = 4,
        Canceled = 8,
        Error = 9
    }
}
