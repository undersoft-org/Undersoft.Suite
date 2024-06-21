using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.DataServices.Currencies
{
    public interface ICurrenciesContext
    {
        IEnumerable<CurrencyRate> GetAllRates();
        IEnumerable<Currency> GetCurrencies();
        IEnumerable<CurrencyRate> GetLatestRates();
        CurrencyProvider GetProvider();
        IEnumerable<CurrencyRate> GetTodayRates();
    }
}