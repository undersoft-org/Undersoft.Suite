// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Application.GUI
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Application.GUI.View;

namespace Undersoft.SCC.Service.Application.GUI.Compound.Presenting.Validatore;

using Undersoft.SCC.Service.Contracts;

/// <summary>
/// The country validator.
/// </summary>
public class CountryValidator : ViewValidator<Country>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountryValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public CountryValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Model.Name!);
            }
        );
    }
}
