namespace Undersoft.SDK.Service.Operation.Invocation;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;

public class Action<TStore, TService, TDto> : Invocation<TDto>
    where TDto : class
    where TService : class
    where TStore : IDataServerStore
{
    public Action() : base() { }

    public Action(string method, object argument) : base(OperationType.Action, typeof(TService), method, argument) { }

    public Action(string method, Arguments arguments)
     : base(OperationType.Action, typeof(TService), method, arguments) { }

    public Action(string method, params object[] arguments)
    : base(OperationType.Action, typeof(TService), method, arguments) { }

    public Action(string method, params byte[] arguments)
   : base(OperationType.Action, typeof(TService), method, arguments) { }

}
