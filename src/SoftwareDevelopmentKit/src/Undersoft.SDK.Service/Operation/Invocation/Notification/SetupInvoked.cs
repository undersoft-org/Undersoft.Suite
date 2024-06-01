namespace Undersoft.SDK.Service.Operation.Invocation.Notification;

using Command;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Invocation;

public class SetupInvoked<TStore, TType, TDto> : InvokeNotification<Invocation<TDto>>
    where TType : class
    where TDto : class, IOrigin
    where TStore : IDataServerStore
{
    public SetupInvoked(Setup<TStore, TType, TDto> command) : base(command)
    {
    }
}
