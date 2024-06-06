// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Application.Server
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Operation.Remote.Command.Validator;

namespace Undersoft.SCC.Service.Application.Server.Validators;

using Undersoft.SCC.Service.Application.ViewModels;

/// <summary>
/// The group validator.
/// </summary>
public class GroupValidator : RemoteCommandValidator<Group>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public GroupValidator(IServicer servicer) : base(servicer)
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
