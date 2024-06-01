using System;

namespace Undersoft.SDK.Service.Data.Event.Handler
{
    public interface IEventHandlerDisposeWrapper : IDisposable
    {
        IEventHandler EventHandler { get; }
    }
}
