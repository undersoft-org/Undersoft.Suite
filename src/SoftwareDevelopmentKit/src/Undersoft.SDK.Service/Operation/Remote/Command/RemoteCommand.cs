using MediatR;

using System.Text.Json.Serialization;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteCommand<TModel> : RemoteCommandBase, IRequest<RemoteCommand<TModel>>, IIdentifiable where TModel : class, IOrigin, IInnerProxy
{
    [JsonIgnore]
    public override TModel Model => base.Model as TModel;

    protected RemoteCommand() { }

    protected RemoteCommand(OperationType commandMode, TModel dataObject)
    {
        CommandMode = commandMode;
        base.Model = dataObject;
    }

    protected RemoteCommand(OperationType commandMode, EventPublishMode publishMode, TModel dataObject)
        : base(dataObject, commandMode, publishMode) { }

    protected RemoteCommand(
        OperationType commandMode,
        EventPublishMode publishMode,
        TModel dataObject,
        params object[] keys
    ) : base(dataObject, commandMode, publishMode, keys) { }

    protected RemoteCommand(OperationType commandMode, EventPublishMode publishMode, params object[] keys)
        : base(commandMode, publishMode, keys) { }

    public override long Id
    {
        get => Model.Id;
        set => Model.Id = value;
    }

    public long TypeId
    {
        get => Model.TypeId;
        set => Model.TypeId = value;
    }
}
