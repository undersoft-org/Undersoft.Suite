using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Remote.Invocation.Notification;

using Command;
using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Invocation;
using Undersoft.SDK.Service.Operation.Remote.Invocation;

public class RemoteSetupInvoked<TStore, TService, TModel> : RemoteInvokeNotification<Invocation<TModel>>
    where TModel : class, IOrigin
    where TService : class
    where TStore : IDataServiceStore
{
    public RemoteSetupInvoked(RemoteSetup<TStore, TService, TModel> command) : base(command)
    {
    }
}
