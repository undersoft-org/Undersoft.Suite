using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.SCC.Service.Server.Validators;

public class ContactsValidator : CommandSetValidator<Contact>
{
    public ContactsValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateEmail(p => p.Contract.Personal!.Email);
                ValidateRequired(p => p.Contract.Personal!.Email);
                ValidateRequired(p => p.Contract.Personal!.PhoneNumber);
                ValidateRequired(p => p.Contract.Personal!.FirstName);
                ValidateRequired(p => p.Contract.Personal!.LastName);
            }
        );

    }
}
