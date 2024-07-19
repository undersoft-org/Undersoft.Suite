using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.Currencies
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