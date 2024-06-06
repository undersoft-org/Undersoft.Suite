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
/// The country validator.
/// </summary>
public class CountryValidator : CommandValidator<Country>
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
                ValidateRequired(p => p.Contract.Name!);
            }
        );
    }
}
