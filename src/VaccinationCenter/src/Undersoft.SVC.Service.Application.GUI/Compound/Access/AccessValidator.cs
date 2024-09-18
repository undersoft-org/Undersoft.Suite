// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Access;

/// <summary>
/// The access validator.
/// </summary>
public class AccessValidator : ViewValidator<Credentials>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccessValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
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
