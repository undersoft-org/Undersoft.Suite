using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Undersoft.SDK.Service.Data.Event.Handler
{
    public class EventHandlerScopeFactory : IEventHandlerFactory, IDisposable
    {
        public Type HandlerType { get; }

        protected IServiceScopeFactory ScopeFactory { get; }

        public EventHandlerScopeFactory(IServiceScopeFactory scopeFactory, Type handlerType)
        {
            ScopeFactory = scopeFactory;
            HandlerType = handlerType;
        }

        public IEventHandlerDisposeWrapper GetHandler()
        {
            var scope = ScopeFactory.CreateScope();
            return new EventHandlerDisposeWrapper(
                (IEventHandler)scope.ServiceProvider.GetRequiredService(HandlerType),
                () => scope.Dispose()
            );
        }

        public bool IsInFactories(List<IEventHandlerFactory> handlerFactories)
        {
            return handlerFactories
                .OfType<EventHandlerScopeFactory>()
                .Any(f => f.HandlerType == HandlerType);
        }

        public void Dispose()
        {

        }
    }
}