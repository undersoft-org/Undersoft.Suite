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
using Undersoft.SVC.Service.Contracts.Catalogs;

namespace Undersoft.SVC.Service.Application.Server.Validators;

/// <summary>
/// The group validator.
/// </summary>
public class ManufacturerValidator : RemoteCommandValidator<Manufacturer>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManufacturerValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public ManufacturerValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Model.Name);
            }
        );
    }
}
