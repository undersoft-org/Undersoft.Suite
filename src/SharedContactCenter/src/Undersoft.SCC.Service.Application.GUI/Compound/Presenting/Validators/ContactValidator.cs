using Undersoft.SCC.Service.Application.ViewModels;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Validatore;

public class ContactValidator : ViewValidator<Contact>
{
    public ContactValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateEmail(p => p.Model.Personal!.Email);
                ValidateRequired(p => p.Model.Personal!.Email);
                ValidateRequired(p => p.Model.Personal!.PhoneNumber);
                ValidateRequired(p => p.Model.Personal!.FirstName);
                ValidateRequired(p => p.Model.Personal!.LastName);
            }
        );

    }
}
