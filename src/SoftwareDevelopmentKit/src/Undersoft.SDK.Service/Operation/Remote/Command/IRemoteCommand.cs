using FluentValidation.Results;

namespace Undersoft.SDK.Service.Operation.Remote.Command;
public interface IRemoteCommand : IOperation
{
    long Id { get; set; }

    object[] Keys { get; set; }

    IOrigin Result { get; set; }

    object Model { get; set; }

    ValidationResult ValidationResult { get; set; }

    bool IsValid { get; }
}
