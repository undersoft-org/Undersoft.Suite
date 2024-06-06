// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.SCC.Service.Server.Validators;

using Undersoft.SCC.Service.Contracts;

/// <summary>
/// The contact validator.
/// </summary>
public class ContactValidator : CommandValidator<Contact>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ContactValidator(IServicer servicer) : base(servicer)
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
