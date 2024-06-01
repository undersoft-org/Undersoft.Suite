using Undersoft.GCC.Service.Contracts;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.GCC.Service.API.Validators;

public class CurrencyRateCommandSetValidator : CommandSetValidator<CurrencyRate>
{
    public CurrencyRateCommandSetValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Any,
            () =>
            {
                ValidateRequired(p => p.Contract.SourceCurrencyId);
                ValidateRequired(p => p.Contract.TargetCurrencyId);
            }
        );

        ValidationScope(
            OperationType.Create | OperationType.Upsert,
            () =>
            {
                ValidateRequired(p => p.Contract.ProviderId);
                ValidateRequired(p => p.Contract.SourceRate);
                ValidateRequired(p => p.Contract.TargetRate);
                ValidateRequired(p => p.Contract.PublishDate);
            }
        );
        ValidationScope(
            OperationType.Create,
            () =>
            {
                ValidateNotExist<IReportStore, Domain.Entities.CurrencyRate>(
                    (cmd) =>
                        a =>
                            a.SourceCurrencyId == cmd.SourceCurrencyId
                            && a.TargetCurrencyId == cmd.TargetCurrencyId
                            && a.ProviderId == cmd.ProviderId
                            && a.PublishDate == cmd.PublishDate
                );
            }
        );
        ValidationScope(
            OperationType.Update | OperationType.Change | OperationType.Delete,
            () =>
            {
                ValidateExist<IReportStore, Domain.Entities.CurrencyRate>(
                    (cmd) =>
                        a =>
                            a.SourceCurrencyId == cmd.SourceCurrencyId
                            && a.TargetCurrencyId == cmd.TargetCurrencyId
                            && a.ProviderId == cmd.ProviderId
                            && a.PublishDate == cmd.PublishDate
                );
            }
        );
    }
}
