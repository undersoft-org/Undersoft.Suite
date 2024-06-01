using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Undersoft.SDK.Service.Operation.Command;

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Event;

public abstract class CommandBase : ICommand
{
    private IOrigin entity;

    public virtual long Id { get; set; }

    public object[] Keys { get; set; }

    [JsonIgnore]
    public virtual IOrigin Result
    {
        get => entity;
        set
        {
            entity = value;
            if (Id == 0 && entity.Id != 0)
                Id = entity.Id;
        }
    }

    [JsonIgnore]
    public virtual object Contract { get; set; }

    [JsonIgnore]
    public ValidationResult ValidationResult { get; set; }

    public string ErrorMessages => ValidationResult.ToString();

    public OperationType CommandMode { get; set; }

    public EventPublishMode PublishMode { get; set; }

    public virtual object Input => Contract;

    public virtual object Output => IsValid ? Id : ErrorMessages;

    public bool IsValid => ValidationResult.IsValid;

    protected CommandBase()
    {
        ValidationResult = new ValidationResult();
    }

    protected CommandBase(OperationType commandMode, EventPublishMode publishMode) : this()
    {
        CommandMode = commandMode;
        PublishMode = publishMode;
    }

    protected CommandBase(object entryData, OperationType commandMode, EventPublishMode publishMode)
        : this(commandMode, publishMode)
    {
        Contract = entryData;
    }

    protected CommandBase(
        object entryData,
        OperationType commandMode,
        EventPublishMode publishMode,
        params object[] keys
    ) : this(commandMode, publishMode, keys)
    {
        Contract = entryData;
    }

    protected CommandBase(
        OperationType commandMode,
        EventPublishMode publishMode,
        params object[] keys
    ) : this(commandMode, publishMode)
    {
        Keys = keys;
    }
}
