using Undersoft.SCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Access;

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

        //ValidationScope(
        //    OperationType.Create,
        //    () =>
        //    {
        //        ValidateNotExist<IDataStore, Contact>(c => p => p.Personal!.Email.Equals(c.Personal!.Email));
        //    }
        //);

        //ValidationScope(
        //    OperationType.Delete | OperationType.Update,
        //    () =>
        //    {
        //        ValidateExist<IDataStore, Contact>(c => p => p.Personal!.Email.Equals(c.Personal!.Email));
        //    }
        //);
    }
}
