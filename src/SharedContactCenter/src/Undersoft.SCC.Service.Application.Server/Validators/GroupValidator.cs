using Undersoft.SCC.Service.Application.ViewModels;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Operation.Remote.Command.Validator;

namespace Undersoft.SCC.Service.Application.Server.Validators;

public class GroupValidator : RemoteCommandValidator<Group>
{
    public GroupValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Model.Name!);
            }
        );
    }
}
