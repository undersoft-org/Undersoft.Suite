namespace Undersoft.SDK.Service.Data.Event;

using System.Runtime.Serialization;

[DataContract]
public class Event : EventInfo, IEvent
{
    public Event() : base() { }

    [DataMember(Order = 16)]
    public virtual byte[] Data { get; set; }
}