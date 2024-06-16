// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SCC.Service.Server
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service.Operation.Remote.Command.Validator;

namespace Undersoft.SCC.Service.Server.Validators;

using Undersoft.SCC.Service.Contracts;

/// <summary>
/// The groups validator.
/// </summary>
public class GroupsValidator : RemoteCommandSetValidator<GroupNode>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GroupsValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public GroupsValidator(IServicer servicer) : base(servicer)
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
