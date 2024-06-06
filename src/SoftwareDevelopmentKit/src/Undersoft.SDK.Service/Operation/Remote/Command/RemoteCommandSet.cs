using FluentValidation.Results;
using MediatR;
using Undersoft.SDK.Service.Data.Event;

namespace Undersoft.SDK.Service.Operation.Remote.Command;

public class RemoteCommandSet<TModel>
    : Registry<RemoteCommand<TModel>>,
        IRequest<RemoteCommandSet<TModel>>,
        IRemoteCommandSet<TModel> where TModel : class, IOrigin, IInnerProxy
{
    public OperationType CommandMode { get; set; }

    public EventPublishMode PublishMode { get; set; }

    protected RemoteCommandSet() : base(true) { }

    protected RemoteCommandSet(OperationType commandMode) : base()
    {
        CommandMode = commandMode;
    }

    protected RemoteCommandSet(
        OperationType commandMode,
        EventPublishMode publishPattern,
        RemoteCommand<TModel>[] DtoCommands
    ) : base(DtoCommands)
    {
        CommandMode = commandMode;
        PublishMode = publishPattern;
    }

    public IEnumerable<RemoteCommand<TModel>> Commands
    {
        get => AsValues();
    }

    public ValidationResult Result { get; set; } = new ValidationResult();

    public object Input => Commands.Select(c => c.Model);

    public object Output => Commands.ForEach(c => c.ValidationResult.IsValid ? c.Id as object : c.ValidationResult);

    public Delegate Processings { get; set; }

    IEnumerable<IRemoteCommand> IRemoteCommandSet.Commands
    {
        get => this.Cast<IRemoteCommand>();
    }
}
