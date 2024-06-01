using Undersoft.SDK.Service.Operation.Invocation;

namespace Undersoft.SDK.Service.Operation.Remote.Invocation;

public class RemoteAccess<TStore, TService, TModel> : Invocation<TModel>
    where TService : class
    where TModel : class
    where TStore : IDataServiceStore
{
    public RemoteAccess() : base() { }

    public RemoteAccess(string method, object argument)
        : base(OperationType.Access, typeof(TService), method, argument) { }

    public RemoteAccess(string method, Arguments arguments)
        : base(OperationType.Access, typeof(TService), method, arguments) { }

    public RemoteAccess(string method, object[] arguments)
        : base(OperationType.Access, typeof(TService), method, arguments) { }

    public RemoteAccess(string method, byte[] arguments)
        : base(OperationType.Access, typeof(TService), method, arguments) { }
}
