﻿using Undersoft.SDK;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command.Validator;
using Undersoft.SDK.Service.Server.Accounts;

namespace Undersoft.SCC.Service.Server.Validators;

public class AccountsValidator : CommandSetValidator<Account>
{
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
