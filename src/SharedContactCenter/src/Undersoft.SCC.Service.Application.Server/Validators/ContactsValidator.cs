// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Application.Server
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Operation.Remote.Command.Validator;

namespace Undersoft.SCC.Service.Application.Server.Validators;

using Undersoft.SCC.Service.Application.ViewModels;

/// <summary>
/// The contacts validator.
/// </summary>
public class ContactsValidator : RemoteCommandSetValidator<Contact>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactsValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ContactsValidator(IServicer servicer) : base(servicer)
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
