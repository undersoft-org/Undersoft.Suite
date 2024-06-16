// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access;

using Undersoft.SVC.Service.Contracts;

/// <summary>
/// The account validator.
/// </summary>
public class AccountValidator : ViewValidator<Account>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public AccountValidator(IServicer servicer) : base(servicer)
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
                ValidateRequired(p => p.Model.Address!.Country);
                ValidateRequired(p => p.Model.Address!.City);
                ValidateRequired(p => p.Model.Address!.Postcode);
                ValidateRequired(p => p.Model.Address!.Street);
                ValidateRequired(p => p.Model.Address!.Building);
                ValidateRequired(p => p.Model.Address!.Apartment);
                ValidateRequired(p => p.Model.Professional!.Profession);
                ValidateRequired(p => p.Model.Professional!.ProfessionIndustry);
            }
        );
    }
}
