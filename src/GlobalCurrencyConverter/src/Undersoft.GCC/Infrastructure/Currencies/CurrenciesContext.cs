using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Utilities;

namespace Undersoft.GCC.Infrastructure.Currencies;

public abstract class CurrenciesContext : ICurrenciesContext
{
    CurrenciesClientFactory _clientFactory = default!;
    CurrenciesAdapter _adapter = default!;
    CurrencyProvider _provider = default!;

    public CurrenciesContext(CurrenciesContextOptions options)
    {
        if (options != null)
            _provider = options.Provider;
        if (_provider == null)
            _provider = new CurrencyProvider();

        Initialize();
    }

    private void Initialize()
    {
        if (_provider != null)
        {
            var baseName = $"{GetType().Namespace}.{_provider.Name}";

            var adapterType = AssemblyUtilities.FindTypeByFullName($"{baseName}{nameof(CurrenciesAdapter)}");
            if (adapterType != null)
                _adapter = adapterType.New<CurrenciesAdapter>(_provider);

            var clientType = AssemblyUtilities.FindTypeByFullName($"{baseName}{nameof(CurrenciesClient)}");
            var factoryType = typeof(CurrenciesClientFactory<>).MakeGenericType(clientType);

            if (clientType != null && factoryType != null)
                _clientFactory = factoryType.New<CurrenciesClientFactory>(_provider);
        }
    }

    public virtual CurrencyProvider GetProvider()
    {
        return _provider;
    }

    public virtual IEnumerable<Currency> GetCurrencies()
    {
        if (_adapter == null || _clientFactory == null)
            return default!;

        var sizes = _clientFactory.CreateClient().GetCurrenciesStream(out MemoryStream stream);

        if (!sizes.Any() || stream == null)
            return [];

        var currencies = _adapter.ReadCurrencies(stream, sizes).Commit();

        return currencies;
    }

    public IEnumerable<CurrencyRate> GetTodayRates()
    {
        if (_adapter == null || _clientFactory == null)
            return default!;

        var sizes = _clientFactory.CreateClient().GetTodayRatesStream(out MemoryStream stream);

        return GetRates(stream, sizes);
    }

    public IEnumerable<CurrencyRate> GetLatestRates()
    {
        if (_adapter == null || _clientFactory == null)
            return default!;

        var sizes = _clientFactory.CreateClient().GetLatestRatesStream(out MemoryStream stream);

        return GetRates(stream, sizes);
    }

    public IEnumerable<CurrencyRate> GetAllRates()
    {
        if (_adapter == null || _clientFactory == null)
            return default!;

        var sizes = _clientFactory.CreateClient().GetAllRatesStream(out MemoryStream stream);

        return GetRates(stream, sizes);
    }

    private IEnumerable<CurrencyRate> GetRates(MemoryStream stream, IEnumerable<int> sizes)
    {
        if (!sizes.Any() || stream == null)
            return []!;

        var rates = _adapter!.ReadCurrencyRates(stream, sizes).Commit();

        return rates;
    }
}
