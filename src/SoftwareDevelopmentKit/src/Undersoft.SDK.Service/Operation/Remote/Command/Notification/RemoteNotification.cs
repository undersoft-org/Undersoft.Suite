using MediatR;
using System.Text.Json;

namespace Undersoft.SDK.Service.Operation.Remote.Command.Notification;

using Remote.Command;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Object;

public abstract class RemoteNotification<TCommand> : Event, INotification where TCommand : RemoteCommandBase
{
    public TCommand Command { get; }

    protected RemoteNotification(TCommand command)
    {
        var aggregateTypeFullName = command.Result.GetDataFullName();
        var eventTypeFullName = GetType().FullName;

        Command = command;
        Id = Unique.NewId;
        EntityId = command.Id;
        EntityTypeName = aggregateTypeFullName;
        EventType = eventTypeFullName;
        var dto = command.Result;
        TypeName = dto.TypeName;
        Modifier = dto.Modifier;
        Modified = dto.Modified;
        Creator = dto.Creator;
        Created = dto.Created;
        PublishStatus = EventPublishStatus.Ready;
        PublishTime = Log.Clock;

        Data = JsonSerializer.SerializeToUtf8Bytes((RemoteCommandBase)command);
    }
}
