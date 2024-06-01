using FluentValidation.Results;

namespace Undersoft.SDK.Service.Operation.Command;


using Undersoft.SDK.Service.Data.Entity;
using Undersoft.SDK.Service.Data.Object;

public interface ICommand : IOperation
{
    long Id { get; set; }

    object[] Keys { get; set; }

    IOrigin Result { get; set; }

    object Contract { get; set; }

    ValidationResult ValidationResult { get; set; }

    bool IsValid { get; }
}
