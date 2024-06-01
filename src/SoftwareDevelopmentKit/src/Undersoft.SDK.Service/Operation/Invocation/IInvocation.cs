using FluentValidation.Results;

namespace Undersoft.SDK.Service.Operation.Invocation;

using Undersoft.SDK.Invoking;

public interface IInvocation : IOperation
{
    long Id { get; set; }

    Arguments Arguments { get; set; }

    string Method => Arguments.MethodName;

    public Type ServiceType => Arguments.TargetType;

    object Response { get; set; }

    ValidationResult Result { get; set; }

    bool IsValid { get; }
}
