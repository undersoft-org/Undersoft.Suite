using System;
using System.Threading.Tasks;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Data.Event.Bus
{
    public sealed class NullEventBus : IEventBus
    {
        public static NullEventBus Instance { get; } = new NullEventBus();

        private NullEventBus()
        {

        }

        public IDisposable Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
        {
            return null;
        }

        public IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : class
        {
            return null;
        }

        public IDisposable Subscribe<TEvent, THandler>() where TEvent : class where THandler : IEventHandler, new()
        {
            return null;
        }

        public IDisposable Subscribe(Type eventType, IEventHandler handler)
        {
            return null;
        }

        public IDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            return null;
        }

        public IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            return null;
        }

        public void Unsubscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
        {

        }

        public void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : class
        {

        }

        public void Unsubscribe(Type eventType, IEventHandler handler)
        {

        }

        public void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {

        }

        public void Unsubscribe(Type eventType, IEventHandlerFactory factory)
        {

        }

        public void UnsubscribeAll<TEvent>() where TEvent : class
        {

        }

        public void UnsubscribeAll(Type eventType)
        {

        }

        public Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true) where TEvent : class
        {
            return Task.CompletedTask;
        }

        public Task PublishAsync(Type eventType, object eventData, bool onUnitOfWorkComplete = true)
        {
            return Task.CompletedTask;
        }
    }
}