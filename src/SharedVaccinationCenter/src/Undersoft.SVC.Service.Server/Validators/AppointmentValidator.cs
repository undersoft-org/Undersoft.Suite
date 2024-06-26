// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Application.Server
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Operation.Remote.Command.Validator;
using Undersoft.SVC.Service.Contracts.Vaccination;

namespace Undersoft.SVC.Service.Application.Server.Validators;

/// <summary>
/// The group validator.
/// </summary>
public class AppointmentValidator : RemoteCommandValidator<Appointment>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public AppointmentValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateEmail(p => p.Model.Personal!.Email);
                ValidateRequired(p => p.Model.Personal!.FirstName);
                ValidateRequired(p => p.Model.Personal!.LastName);
                ValidateRequired(p => p.Model.Personal!.PhoneNumber);
                ValidateRequired(p => p.Model.Personal!.Birthdate);
                ValidateRequired(p => p.Model.Campaign!.Name);
            }
        );
    }
}
