namespace Undersoft.SDK.Service.Operation.Invocation;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;

public class Setup<TStore, TService, TDto> : Invocation<TDto>
    where TDto : class
    where TService : class
    where TStore : IDataServerStore
{
    public Setup() : base() { }

    public Setup(string method, object argument) : base(OperationType.Action, typeof(TService), method, argument) { }

    public Setup(string method, Arguments arguments)
     : base(OperationType.Action, typeof(TService), method, arguments) { }

    public Setup(string method, params object[] arguments)
    : base(OperationType.Action, typeof(TService), method, arguments) { }

    public Setup(string method, params byte[] arguments)
   : base(OperationType.Action, typeof(TService), method, arguments) { }

}
