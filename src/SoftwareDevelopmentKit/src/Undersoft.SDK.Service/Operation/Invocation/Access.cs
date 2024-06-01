namespace Undersoft.SDK.Service.Operation.Invocation;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;

public class Access<TStore, TService, TDto> : Invocation<TDto>
    where TDto : class
    where TService : class
    where TStore : IDataServerStore
{
    public Access() : base() { }

    public Access(string method, object argument) : base(OperationType.Access, typeof(TService), method, argument) { }

    public Access(string method, Arguments arguments)
     : base(OperationType.Access, typeof(TService), method, arguments) { }

    public Access(string method, params object[] arguments)
    : base(OperationType.Access, typeof(TService), method, arguments) { }

    public Access(string method, params byte[] arguments)
   : base(OperationType.Access, typeof(TService), method, arguments) { }

}
