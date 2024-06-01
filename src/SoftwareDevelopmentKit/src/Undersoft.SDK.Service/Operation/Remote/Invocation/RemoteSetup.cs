using Undersoft.SDK.Service.Operation.Invocation;

namespace Undersoft.SDK.Service.Operation.Remote.Invocation;

public class RemoteSetup<TStore, TService, TModel> : Invocation<TModel>
    where TService : class
    where TModel : class
    where TStore : IDataServiceStore
{
    public RemoteSetup() : base() { }

    public RemoteSetup(string method, object argument) : base(OperationType.Setup, typeof(TService), method, argument) { }

    public RemoteSetup(string method, Arguments arguments)
     : base(OperationType.Setup, typeof(TService), method, arguments) { }

    public RemoteSetup(string method, params object[] arguments)
    : base(OperationType.Setup, typeof(TService), method, arguments) { }

    public RemoteSetup(string method, params byte[] arguments)
    : base(OperationType.Setup, typeof(TService), method, arguments) { }
}