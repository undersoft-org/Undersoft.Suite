using MediatR;
using System.Text.Json;

namespace Undersoft.SDK.Service.Operation.Remote.Invocation.Notification;

using Command;
using Logging;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Object;
using Undersoft.SDK.Service.Operation.Invocation;
using Uniques;

public abstract class RemoteInvokeNotification<TCommand> : Event, INotification where TCommand : InvocationBase
{
    public TCommand Command { get; }

    protected RemoteInvokeNotification(TCommand command)
    {
        var aggregateTypeFullName = command.Response.GetDataFullName();
        var eventTypeFullName = GetType().FullName;

        Command = command;
        Id = Unique.NewId;
        EntityId = command.Id;
        EntityTypeName = aggregateTypeFullName;
        EventType = eventTypeFullName;
        var response = command.Response;
        PublishStatus = EventPublishStatus.Ready;
        PublishTime = Log.Clock;

        Data = JsonSerializer.SerializeToUtf8Bytes((InvocationBase)command);
    }
}
