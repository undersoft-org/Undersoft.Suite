using FluentValidation.Results;

using System.Text.Json.Serialization;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public abstract class RemoteCommandBase : IRemoteCommand
{
    protected RemoteCommandBase()
    {
        ValidationResult = new ValidationResult();
    }

    protected RemoteCommandBase(OperationType commandMode, EventPublishMode publishMode) : this()
    {
        CommandMode = commandMode;
        PublishMode = publishMode;
    }

    protected RemoteCommandBase(object entryData, OperationType commandMode, EventPublishMode publishMode)
        : this(commandMode, publishMode)
    {
        Model = entryData;
    }

    protected RemoteCommandBase(
        object entryData,
        OperationType commandMode,
        EventPublishMode publishMode,
        params object[] keys
    ) : this(commandMode, publishMode, keys)
    {
        Model = entryData;
    }

    protected RemoteCommandBase(
        OperationType commandMode,
        EventPublishMode publishMode,
        params object[] keys
    ) : this(commandMode, publishMode)
    {
        Keys = keys;
    }

    private IOrigin contract;

    public virtual long Id { get; set; }

    public object[] Keys { get; set; }

    [JsonIgnore]
    public virtual IOrigin Result
    {
        get => contract;
        set
        {
            contract = value;
            if (Id == 0 && contract.Id != 0)
                Id = contract.Id;
        }
    }

    [JsonIgnore]
    public virtual object Model { get; set; }

    [JsonIgnore]
    public ValidationResult ValidationResult { get; set; }

    public string ErrorMessages => ValidationResult.ToString();

    public OperationType CommandMode { get; set; }

    public EventPublishMode PublishMode { get; set; }

    public virtual object Input => Model;

    public virtual object Output => IsValid ? Id : ErrorMessages;

    public bool IsValid => ValidationResult.IsValid;
}
