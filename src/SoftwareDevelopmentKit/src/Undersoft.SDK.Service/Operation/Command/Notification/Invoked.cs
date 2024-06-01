namespace Undersoft.SDK.Service.Operation.Command.Notification;

using Command;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Invocation;
using Undersoft.SDK.Service.Operation.Invocation.Notification;

public class Invoked<TStore, TType, TDto> : InvokeNotification<Invocation<TDto>>
    where TType : class
    where TDto : class, IOrigin
    where TStore : IDataServerStore
{
    public Invoked(Action<TStore, TType, TDto> command) : base(command)
    {
    }
}
