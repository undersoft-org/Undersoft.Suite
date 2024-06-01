using System;

namespace Undersoft.SDK.Service.Data.Event.Bus
{
    public interface IEventNameProvider
    {
        string GetName(Type eventType);
    }
}