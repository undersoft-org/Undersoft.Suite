using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Data.Event.Bus
{
    public abstract class EventBusBase : IEventBus
    {
        protected IServiceScopeFactory ServiceScopeFactory { get; }


        protected IEventHandlerInvoker EventHandlerInvoker { get; }

        protected EventBusBase(
            IServiceScopeFactory serviceScopeFactory,
            IEventHandlerInvoker eventHandlerInvoker)
        {
            ServiceScopeFactory = serviceScopeFactory;
            EventHandlerInvoker = eventHandlerInvoker;
        }

        public virtual IDisposable Subscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class
        {
            return Subscribe(typeof(TEvent), new ActionEventHandler<TEvent>(action));
        }

        public virtual IDisposable Subscribe<TEvent, THandler>()
            where TEvent : class
            where THandler : IEventHandler, new()
        {
            return Subscribe(typeof(TEvent), new EventHandlerTransientFactory<THandler>());
        }

        public virtual IDisposable Subscribe(Type eventType, IEventHandler handler)
        {
            return Subscribe(eventType, new EventHandlerSingletonFactory(handler));
        }

        public virtual IDisposable Subscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            return Subscribe(typeof(TEvent), factory);
        }

        public abstract IDisposable Subscribe(Type eventType, IEventHandlerFactory factory);

        public abstract void Unsubscribe<TEvent>(Func<TEvent, Task> action) where TEvent : class;

        public virtual void Unsubscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : class
        {
            Unsubscribe(typeof(TEvent), handler);
        }

        public abstract void Unsubscribe(Type eventType, IEventHandler handler);

        public virtual void Unsubscribe<TEvent>(IEventHandlerFactory factory) where TEvent : class
        {
            Unsubscribe(typeof(TEvent), factory);
        }

        public abstract void Unsubscribe(Type eventType, IEventHandlerFactory factory);

        public virtual void UnsubscribeAll<TEvent>() where TEvent : class
        {
            UnsubscribeAll(typeof(TEvent));
        }

        public abstract void UnsubscribeAll(Type eventType);

        public Task PublishAsync<TEvent>(TEvent eventData, bool onUnitOfWorkComplete = true)
            where TEvent : class
        {
            return PublishAsync(typeof(TEvent), eventData, onUnitOfWorkComplete);
        }

        public virtual async Task PublishAsync(
            Type eventType,
            object eventData,
            bool onUnitOfWorkComplete = true)
        {

            await PublishToEventBusAsync(eventType, eventData);
        }

        protected abstract Task PublishToEventBusAsync(Type eventType, object eventData);

        public virtual async Task TriggerHandlersAsync(Type eventType, object eventData)
        {
            var exceptions = new List<Exception>();

            await TriggerHandlersAsync(eventType, eventData, exceptions);

            if (exceptions.Any())
            {
                ThrowOriginalExceptions(eventType, exceptions);
            }
        }

        protected virtual async Task TriggerHandlersAsync(Type eventType, object eventData, List<Exception> exceptions)
        {
            await new SynchronizationContextRemover();

            foreach (var handlerFactories in GetHandlerFactories(eventType))
            {
                foreach (var handlerFactory in handlerFactories.EventHandlerFactories)
                {
                    await TriggerHandlerAsync(handlerFactory, handlerFactories.EventType, eventData, exceptions);
                }
            }

            if (eventType.GetTypeInfo().IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.GetTypeInfo().BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(baseArg);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    var baseEventData = Activator.CreateInstance(baseEventType, constructorArgs);
                    await PublishToEventBusAsync(baseEventType, baseEventData);
                }
            }
        }

        protected virtual async Task TriggerHandlerAsync(IEventHandlerFactory asyncHandlerFactory, Type eventType,
           object eventData, List<Exception> exceptions)
        {
            using (var eventHandlerWrapper = asyncHandlerFactory.GetHandler())
            {
                try
                {
                    await EventHandlerInvoker.InvokeAsync(eventHandlerWrapper.EventHandler, eventData, eventType);
                }
                catch (TargetInvocationException ex)
                {
                    exceptions.Add(ex.InnerException);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
        }


        protected void ThrowOriginalExceptions(Type eventType, List<Exception> exceptions)
        {

            throw new AggregateException(
                "More than one error has occurred while triggering the event: " + eventType,
                exceptions
            );
        }

        protected virtual void SubscribeHandlers(IList<IEventHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                var handlertype = handler.GetType();
                var interfaces = handlertype.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (!typeof(IEventHandler).GetTypeInfo().IsAssignableFrom(@interface))
                    {
                        continue;
                    }

                    var genericArgs = @interface.GetGenericArguments();
                    if (genericArgs.Length == 1)
                    {
                        Subscribe(genericArgs[0], new EventHandlerScopeFactory(ServiceScopeFactory, handlertype));
                    }
                }
            }
        }

        protected abstract IEnumerable<EventWithHandlerFactories> GetHandlerFactories(Type eventType);


        protected class EventWithHandlerFactories
        {
            public Type EventType { get; }

            public List<IEventHandlerFactory> EventHandlerFactories { get; }

            public EventWithHandlerFactories(Type eventType, List<IEventHandlerFactory> eventHandlerFactories = null)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactories ?? new List<IEventHandlerFactory>();
            }
            public EventWithHandlerFactories(Type eventType, IEventHandlerFactory eventHandlerFactory)
            {
                EventType = eventType;
                EventHandlerFactories = eventHandlerFactory != null ? new List<IEventHandlerFactory>() { eventHandlerFactory } : new List<IEventHandlerFactory>();
            }
        }

        protected struct SynchronizationContextRemover : INotifyCompletion
        {
            public bool IsCompleted
            {
                get { return SynchronizationContext.Current == null; }
            }

            public void OnCompleted(Action continuation)
            {
                var prevContext = SynchronizationContext.Current;
                try
                {
                    SynchronizationContext.SetSynchronizationContext(null);
                    continuation();
                }
                finally
                {
                    SynchronizationContext.SetSynchronizationContext(prevContext);
                }
            }

            public SynchronizationContextRemover GetAwaiter()
            {
                return this;
            }

            public void GetResult()
            {
            }
        }
    }
}
