using System;
using System.Collections.Generic;

namespace Undersoft.SDK.Service.Data.Event.Handler
{
    public interface IEventHandlerFactory
    {
        Type HandlerType { get; }

        IEventHandlerDisposeWrapper GetHandler();

        bool IsInFactories(List<IEventHandlerFactory> handlerFactories);
    }
}
