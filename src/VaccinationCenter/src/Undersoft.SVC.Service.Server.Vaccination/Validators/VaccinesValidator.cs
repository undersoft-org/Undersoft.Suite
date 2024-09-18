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

namespace Undersoft.SVC.Service.Application.Server.Validators;

using Undersoft.SVC.Service.Contracts.Catalogs;

/// <summary>
/// The contacts validator.
/// </summary>
public class VaccinesValidator : RemoteCommandSetValidator<Vaccine>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PatientsValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public VaccinesValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Model.Specification!.Name);
                ValidateRequired(p => p.Model.Specification!.Dose);
                ValidateRequired(p => p.Model.Safety!.ExpirationDays);
                ValidateRequired(p => p.Model.Safety!.Temperature);
                ValidateRequired(p => p.Model.Manufacturer!.Name);
            }
        );

    }
}
