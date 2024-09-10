using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SSC.Service.Application.GUI.Compound.Access;

public class AccessValidator : ViewValidator<Credentials>
{
    public AccessValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Access | OperationType.Create | OperationType.Update,
            () =>
            {
                ValidateEmail(p => p.Model.Email);
                ValidateRequired(p => p.Model.Email);
            }
        );
        ValidationScope(
            OperationType.Access | OperationType.Create | OperationType.Change,
            () =>
            {
                ValidateRequired(p => p.Model.Password);
            }
        );
        ValidationScope(
            OperationType.Create,
            () =>
            {
                ValidateRequired(p => p.Model.FirstName);
                ValidateRequired(p => p.Model.LastName);
                ValidateEqual(p => p.Model.RetypedPassword, p => p.Model.Password);
            }
        );
        ValidationScope(
            OperationType.Change,
            () =>
            {
                ValidateRequired(p => p.Model.NewPassword);
                ValidateEqual(p => p.Model.RetypedPassword, p => p.Model.NewPassword);
            }
        );
        ValidationScope(
            OperationType.Setup,
            () =>
            {
                ValidateRequired(p => p.Model.EmailConfirmationToken);
            }
        );
        ValidationScope(
            OperationType.Delete,
            () =>
            {
                ValidateRequired(p => p.Model.PasswordResetToken);
            }
        );
    }
}
