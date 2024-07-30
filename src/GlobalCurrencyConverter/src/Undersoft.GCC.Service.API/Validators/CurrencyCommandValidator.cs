using Undersoft.GCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.GCC.Service.API.Validators;

public class CurrencyCommandValidator : CommandValidator<Currency>
{
    public CurrencyCommandValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Contract.CurrencyCode);
            }
        );

        ValidationScope(
            OperationType.Create | OperationType.Upsert,
            () =>
            {
                ValidateRequired(p => p.Contract.Name);
            }
        );
        ValidationScope(
            OperationType.Create,
            () =>
            {
                ValidateNotExist<IReportStore, Domain.Entities.Currency>(
                    (cmd) => a => cmd.CurrencyCode == cmd.CurrencyCode
                );
            }
        );
        ValidationScope(
            OperationType.Update | OperationType.Change | OperationType.Delete,
            () =>
            {
                ValidateExist<IReportStore, Domain.Entities.Currency>(
                    (cmd) => a => a.CurrencyCode == cmd.CurrencyCode
                );
            }
        );
    }
}
