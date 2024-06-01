using MediatR;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK;
using Undersoft.SDK.Proxies;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Uniques;

public class Command<TDto> : CommandBase, IRequest<Command<TDto>>, IIdentifiable where TDto : class, IOrigin, IInnerProxy
{
    [JsonIgnore]
    public override TDto Contract => base.Contract as TDto;

    protected Command() { }

    protected Command(OperationType commandMode, TDto dataObject)
    {
        CommandMode = commandMode;
        base.Contract = dataObject;
    }

    protected Command(OperationType commandMode, EventPublishMode publishMode, TDto dataObject)
        : base(dataObject, commandMode, publishMode) { }

    protected Command(
        OperationType commandMode,
        EventPublishMode publishMode,
        TDto dataObject,
        params object[] keys
    ) : base(dataObject, commandMode, publishMode, keys) { }

    protected Command(OperationType commandMode, EventPublishMode publishMode, params object[] keys)
        : base(commandMode, publishMode, keys) { }

    public override long Id
    {
        get => Contract != null ? Contract.Id : Keys.UniqueKey64();
        set
        {
            if (Contract != null) Contract.Id = value;
            else Keys = new object[] { value };
        }
    }

    public long TypeId
    {
        get => Contract.TypeId;
        set => Contract.TypeId = value;
    }
}
