using System;
using System.Threading.Tasks;

namespace Undersoft.SDK.Service.Data.Event.Handler
{

    public delegate Task EventHandlerMethodExecutorAsync(IEventHandler target, object parameter);

    public interface IEventHandlerMethodExecutor
    {
        EventHandlerMethodExecutorAsync ExecutorAsync { get; }
    }

    public class EventHandlerMethodExecutor<TEvent> : IEventHandlerMethodExecutor where TEvent : class
    {
        public EventHandlerMethodExecutorAsync ExecutorAsync => (target, parameter) => (target as IEventHandler<TEvent>).HandleEventAsync(parameter as TEvent);

        public Task ExecuteAsync(IEventHandler target, TEvent parameters)
        {
            return ExecutorAsync(target, parameters);
        }
    }
}