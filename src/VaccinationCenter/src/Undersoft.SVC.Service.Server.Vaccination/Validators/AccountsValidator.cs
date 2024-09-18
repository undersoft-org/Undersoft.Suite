// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   server: Undersoft.SVC.Service.Server
// ********************************************************

using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command.Validator;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SVC.Service.Server.Vaccination.Validators;

/// <summary>
/// The accounts validator.
/// </summary>
public class AccountsValidator : CommandSetValidator<Account>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountsValidator"/> class.
    /// </summary>
    /// <param name="servicer">The servicer.</param>
    public AccountsValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateEmail(p => p.Contract.Credentials.Email);
            }
        );

        ValidationScope(
            OperationType.Create | OperationType.Upsert,
            () =>
            {
                ValidateRequired(p => p.Contract.Credentials.UserName);
                ValidateRequired(p => p.Contract.Credentials.Email);
                ValidateRequired(p => p.Contract.Credentials.Password);
            }
        );
        ValidationScope(
            OperationType.Create,
            () =>
            {
                ValidateNotExist<IReportStore, Account>(
                    (cmd) =>
                        a =>
                            a.User != null
                                ? a.User.Email == cmd.Credentials.Email
                                    || a.User.UserName == cmd.Credentials.UserName
                                : false,
                    "Account already exists"
                );
            }
        );
        ValidationScope(
            OperationType.Update | OperationType.Change | OperationType.Delete,
            () =>
            {
                ValidateRequired(p => p.Contract.Credentials.SessionToken);
                ValidateRequired(p => p.Contract.Credentials.Email);
                ValidateRequired(a => a.Contract.Id);
                ValidateExist<IReportStore, Account>(
                    (cmd) => a => a.User != null ? a.User.Email == cmd.Credentials.Email : false
                );
            }
        );
    }
}
