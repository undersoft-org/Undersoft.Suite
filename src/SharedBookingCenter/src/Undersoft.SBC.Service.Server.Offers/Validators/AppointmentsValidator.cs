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
/// The groups validator.
/// </summary>
public class AppointmentsValidator : RemoteCommandSetValidator<Appointment>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentsValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public AppointmentsValidator(IServicer servicer) : base(servicer)
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
