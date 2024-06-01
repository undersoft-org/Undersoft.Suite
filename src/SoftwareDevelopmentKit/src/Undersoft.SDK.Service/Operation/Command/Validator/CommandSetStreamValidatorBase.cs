using Undersoft.SDK.Service.Operation.Command;

namespace Undersoft.SDK.Service.Operation.Command.Validator;

public abstract class CommandSetStreamValidatorBase<TCommand> : CommandSetValidatorBase<TCommand> where TCommand : ICommandSet
{
    public CommandSetStreamValidatorBase(IServicer servicer) : base(servicer)
    {
    }
}