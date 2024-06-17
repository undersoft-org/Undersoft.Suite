namespace Undersoft.SDK.Service.Data.Event;
public interface IEvent : IEventInfo
{
    byte[] Data { get; set; }
}

public interface IEventInfo
{
    uint Version { get; set; }
    string EventType { get; set; }
    long EntityId { get; set; }
    string EntityTypeName { get; set; }
    DateTime PublishTime { get; set; }
    EventPublishStatus PublishStatus { get; set; }
}