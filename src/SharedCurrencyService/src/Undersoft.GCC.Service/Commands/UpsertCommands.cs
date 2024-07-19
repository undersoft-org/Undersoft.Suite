using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Service;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GCC.Service.Commands
{
    public class UpsertCommands
    {
        const int TRYOUT_LIMIT = 5;

        IServicer _servicer = default!;

        public UpsertCommands() { }

        public UpsertCommands(IServicer servicer)
        {
            _servicer = servicer;
        }

        public void UpsertCurrencies(IEnumerable<Currency> currencies)
        {
            var tryout = TRYOUT_LIMIT;
            while (--tryout > 0)
            {
                var repo = _servicer.RemoteSet<IDataStore, Contracts.Currency>();
                if (repo == null)
                {
                    Task.Delay(5000);
                    continue;
                }
                var changes = repo.PutBy(currencies, d => e => d.Id == e.Id && d.CurrencyCode == e.CurrencyCode).Commit();
                repo.Save(true);
            }
        }

        public void UpsertLatestRates(IEnumerable<CurrencyRate> currencies)
        {
            var tryout = TRYOUT_LIMIT;
            while (--tryout > 0)
            {
                var repo = _servicer.RemoteSet<IDataStore, Contracts.CurrencyRate>();
                if (repo == null)
                {
                    Task.Delay(5000);
                    continue;
                }
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
        }

        public void UpsertAllRates(IEnumerable<CurrencyRate> currencies)
        {
            var tryout = TRYOUT_LIMIT;
            while (--tryout > 0)
            {
                var repo = _servicer.RemoteSet<IDataStore, Contracts.CurrencyRate>();
                if (repo == null)
                {
                    Task.Delay(5000);
                    continue;
                }
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
}
