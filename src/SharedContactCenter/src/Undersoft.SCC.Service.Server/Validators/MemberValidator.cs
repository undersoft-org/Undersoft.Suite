﻿// ********************************************************
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
public class MemberValidator : CommandValidator<MemberNode>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MemberValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public MemberValidator(IServicer servicer) : base(servicer)
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
