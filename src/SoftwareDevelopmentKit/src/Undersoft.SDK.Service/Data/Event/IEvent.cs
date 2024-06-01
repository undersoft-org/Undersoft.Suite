namespace Undersoft.SDK.Service.Data.Event;

using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Model;

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