using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.Currencies;

public abstract class CurrenciesAdapter
{
    public abstract void SetProvider(CurrencyProvider provider);

    public abstract IEnumerable<Currency> ReadCurrencies(MemoryStream data, IEnumerable<int> sizes);

    public abstract IEnumerable<CurrencyRate> ReadCurrencyRates(MemoryStream data, IEnumerable<int> sizes);
}
