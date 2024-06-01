using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Service.Commands
{
    public class UpsertCommands
    {
        IServicer _servicer = default!;

        public UpsertCommands() { }

        public UpsertCommands(IServicer servicer)
        {
            _servicer = servicer;
        }

        public void UpsertCurrencies(IEnumerable<Currency> currencies)
        {
            var repo = _servicer.RemoteSet<IDataStore, Contracts.Currency>();
            var changes = repo.PutBy(currencies, d => e => d.Id == e.Id && d.CurrencyCode == e.CurrencyCode).Commit();
            repo.Save(true);
        }

        public void UpsertLatestRates(IEnumerable<CurrencyRate> currencies)
        {
            var repo = _servicer.RemoteSet<IDataStore, Contracts.CurrencyRate>();
            repo.PutBy(
                    currencies,
                    d =>
                        e =>
                            d.PublishDate == e.PublishDate
                            && d.ProviderId == e.ProviderId
                            && d.TargetCurrencyId == e.TargetCurrencyId
                )
                .Commit();
            repo.Save(true);
        }

        public void UpsertAllRates(IEnumerable<CurrencyRate> currencies)
        {
            var repo = _servicer.RemoteSet<IDataStore, Contracts.CurrencyRate>();
            repo
                .PutBy(
                    currencies,
                    d =>
                        e =>
                            d.PublishDate == e.PublishDate
                            && d.ProviderId == e.ProviderId
                            && d.TargetCurrencyId == e.TargetCurrencyId
                )
                .Commit();
            repo.Save(true);
        }
    }
}
