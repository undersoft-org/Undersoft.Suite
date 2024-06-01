using System;
using System.Collections.Generic;
using System.Linq;

namespace Undersoft.SDK.Service.Data.Event.Handler
{
    public class EventHandlerTransientFactory<THandler> : EventHandlerTransientFactory, IEventHandlerFactory where THandler : IEventHandler, new()
    {
        public EventHandlerTransientFactory() : base(typeof(THandler)) { }

        protected override IEventHandler CreateHandler()
        {
            return new THandler();
        }
    }

    public class EventHandlerTransientFactory : IEventHandlerFactory
    {
        public Type HandlerType { get; }

        public EventHandlerTransientFactory(Type handlerType)
        {
            HandlerType = handlerType;
        }

        public virtual IEventHandlerDisposeWrapper GetHandler()
        {
            var handler = CreateHandler();
            return new EventHandlerDisposeWrapper(handler,
                () => (handler as IDisposable)?.Dispose()
            );
        }

        public bool IsInFactories(List<IEventHandlerFactory> handlerFactories)
        {
            return handlerFactories
                .OfType<EventHandlerTransientFactory>()
                .Any(f => f.HandlerType == HandlerType);
        }

        protected virtual IEventHandler CreateHandler()
        {
            return HandlerType.New<IEventHandler>();
        }
    }
}
