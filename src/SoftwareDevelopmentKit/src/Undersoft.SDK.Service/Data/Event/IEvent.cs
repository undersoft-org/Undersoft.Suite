namespace Undersoft.SDK.Service.Data.Event;
public interface IEvent
{
    uint Version { get; set; }
    string EventType { get; set; }
    byte[] Data { get; set; }
    long EntityId { get; set; }
    string EntityTypeName { get; set; }
    DateTime PublishTime { get; set; }
    EventPublishStatus PublishStatus { get; set; }
}