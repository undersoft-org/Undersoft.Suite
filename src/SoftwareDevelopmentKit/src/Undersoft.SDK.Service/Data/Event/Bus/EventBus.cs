using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Undersoft.SDK.Service.Data.Event.Handler;

namespace Undersoft.SDK.Service.Data.Event.Bus
{
    public class EventBus : EventBusBase, IEventBus
    {
        public ILogger<EventBus> Logger { get; set; }

        protected EventBusOptions Options { get; }

        protected Registry<EventWithHandlerFactories> HandlerFactories { get; set; }

        public EventBus(
            IOptions<EventBusOptions> options,
            IServiceScopeFactory serviceScopeFactory,
            IEventHandlerInvoker eventHandlerInvoker)
            : base(serviceScopeFactory, eventHandlerInvoker)
        {
            Options = options.Value;
            Logger = NullLogger<EventBus>.Instance;

            HandlerFactories = new Registry<EventWithHandlerFactories>();
            SubscribeHandlers(Options.Handlers);
        }

        public virtual IDisposable Subscribe<TEvent>(IEventHandler<TEvent> handler) where TEvent : class
        {
            return Subscribe(typeof(TEvent), handler);
        }

        public override IDisposable Subscribe(Type eventType, IEventHandlerFactory factory)
        {
            var factories = GetOrCreateHandlerFactories(eventType, factory);

            if (!factory.IsInFactories(factories.EventHandlerFactories))
            {
                factories.EventHandlerFactories.Add(factory);
            }

            return new EventHandlerFactoryUnregistrar(this, eventType, factory);
        }

        public override void Unsubscribe<TEvent>(Func<TEvent, Task> action)
        {
            GetOrCreateHandlerFactories(typeof(TEvent)).EventHandlerFactories.RemoveAll(
                        factory =>
                        {
                            var singleInstanceFactory = factory as EventHandlerSingletonFactory;
                            if (singleInstanceFactory == null)
                            {
                                return false;
                            }

                            var actionHandler = singleInstanceFactory.HandlerInstance as ActionEventHandler<TEvent>;
                            if (actionHandler == null)
                            {
                                return false;
                            }

                            return actionHandler.Action == action;
                        });
        }

        public override void Unsubscribe(Type eventType, IEventHandler handler)
        {
            GetOrCreateHandlerFactories(eventType).EventHandlerFactories.RemoveAll(
                        factory =>
                            factory is EventHandlerSingletonFactory &&
                            (factory as EventHandlerSingletonFactory).HandlerInstance == handler
                    );
        }

        public override void Unsubscribe(Type eventType, IEventHandlerFactory factory)
        {
            GetOrCreateHandlerFactories(eventType).EventHandlerFactories.Remove(factory);
        }

        public override void UnsubscribeAll(Type eventType)
        {
            GetOrCreateHandlerFactories(eventType).EventHandlerFactories.Clear();
        }

        protected override async Task PublishToEventBusAsync(Type eventType, object eventData)
        {
            await PublishAsync(new EventMessage(eventData, eventType));
        }

        public virtual async Task PublishAsync(EventMessage localEventMessage)
        {
            await TriggerHandlersAsync(Type.GetType(localEventMessage.EventType), localEventMessage.Data);
        }

        protected override IEnumerable<EventWithHandlerFactories> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<EventWithHandlerFactories>();

            foreach (var handlerFactory in HandlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.EventType)))
            {
                handlerFactoryList.Add(handlerFactory);
            }

            return handlerFactoryList;
        }

        private EventWithHandlerFactories GetOrCreateHandlerFactories(Type eventType, IEventHandlerFactory factory = null)
        {

            return HandlerFactories.EnsureGet(eventType, new EventWithHandlerFactories(eventType, factory != null ? new List<IEventHandlerFactory>() { factory } : null)).Value;
        }

        private static bool ShouldTriggerEventForHandler(Type targetEventType, Type handlerEventType)
        {
            if (handlerEventType == targetEventType)
            {
                return true;
            }

            if (handlerEventType.IsAssignableFrom(targetEventType))
            {
                return true;
            }

            return false;
        }
    }
}
