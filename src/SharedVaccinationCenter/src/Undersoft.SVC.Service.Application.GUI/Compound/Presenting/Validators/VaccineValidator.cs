﻿// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SVC.Service.Application.GUI.Compound.Presenting.Validators;

using Undersoft.SVC.Service.Application.ViewModels.Catalogs;

/// <summary>
/// The group validator.
/// </summary>
public class VaccineValidator : ViewValidator<Vaccine>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OfficeValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public VaccineValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Model.Specification!.Name);
            }
        );
    }
}
