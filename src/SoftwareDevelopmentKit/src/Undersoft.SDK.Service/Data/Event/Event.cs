namespace Undersoft.SDK.Service.Data.Event;

using System.Runtime.Serialization;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Model;
using Undersoft.SDK.Service.Data.Object;

[DataContract]
public class Event : DataObject, IEvent, IEntity, IContract
{
    public Event() : base() { }

    [DataMember(Order = 12)]
    public virtual uint Version { get; set; }

    [DataMember(Order = 13)]
    public virtual string EventType { get; set; }

    [DataMember(Order = 14)]
    public virtual long EntityId { get; set; }

    [DataMember(Order = 15)]
    public virtual string EntityTypeName { get; set; }

    [DataMember(Order = 16)]
    public virtual byte[] Data { get; set; }

    [DataMember(Order = 17)]
    public virtual DateTime PublishTime { get; set; }

    [DataMember(Order = 18)]
    public virtual EventPublishStatus PublishStatus { get; set; }
}