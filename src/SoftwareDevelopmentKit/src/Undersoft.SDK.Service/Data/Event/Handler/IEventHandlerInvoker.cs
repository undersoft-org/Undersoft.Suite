using System;
using System.Threading.Tasks;

namespace Undersoft.SDK.Service.Data.Event.Handler
{
    public interface IEventHandlerInvoker
    {
        Task InvokeAsync(IEventHandler eventHandler, object eventData, Type eventType);
    }
}