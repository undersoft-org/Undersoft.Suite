using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Operation.Command.Validator;

namespace Undersoft.GCC.Service.API.Validators;

public class CurrencyProviderCommandSetValidator : CommandSetValidator<Contracts.CurrencyProvider>
{
    public CurrencyProviderCommandSetValidator(IServicer servicer) : base(servicer)
    {
        ValidationScope(
            OperationType.Create | OperationType.Upsert,
            () =>
            {
                ValidateRequired(p => p.Contract.Name);
                ValidateRequired(p => p.Contract.FullName);
                ValidateRequired(p => p.Contract.BaseCurrency);
                ValidateRequired(p => p.Contract.BaseCurrency != null ? p.Contract.BaseCurrency.CurrencyCode : null);
                ValidateRequired(p => p.Contract.BaseCurrency != null ? p.Contract.BaseCurrency.Name : null);
                ValidateRequired(p => p.Contract.BaseUri);
                ValidateRequired(p => p.Contract.UpdateHour);
                ValidateRequired(p => p.Contract.UpdateMinute);
                ValidateRequired(p => p.Contract.HistorySince);
            }
        );
        ValidationScope(
            OperationType.Create,
            () =>
            {
                ValidateNotExist<IReportStore, CurrencyProvider>(
                    (cmd) =>
                        a =>
                            a.Name == cmd.Name || a.FullName == cmd.FullName || a.BaseUri == cmd.BaseUri
                );
            }
        );
        ValidationScope(
            OperationType.Update | OperationType.Change | OperationType.Delete,
            () =>
            {
                ValidateExist<IReportStore, CurrencyProvider>(
                    (cmd) =>
                        a =>
                            a.Id == cmd.Id && a.Name == cmd.Name
                );
            }
        );
    }
}
