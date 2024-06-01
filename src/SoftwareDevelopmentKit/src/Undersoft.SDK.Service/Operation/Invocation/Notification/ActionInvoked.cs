namespace Undersoft.SDK.Service.Operation.Invocation.Notification;

using Command;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Invocation;

public class ActionInvoked<TStore, TType, TDto> : InvokeNotification<Invocation<TDto>>
    where TType : class
    where TDto : class, IOrigin
    where TStore : IDataServerStore
{
    public ActionInvoked(Action<TStore, TType, TDto> command) : base(command)
    {
    }
}
