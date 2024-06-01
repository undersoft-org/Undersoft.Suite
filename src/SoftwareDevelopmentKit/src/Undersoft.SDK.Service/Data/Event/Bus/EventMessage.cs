using System;
using System.Text.Json;

namespace Undersoft.SDK.Service.Data.Event.Bus
{
    public class EventMessage : Event
    {
        public EventMessage(object eventData, Type eventType)
        {
            Data = JsonSerializer.SerializeToUtf8Bytes(eventData);
            TypeName = eventType.FullName;
        }
    }
}