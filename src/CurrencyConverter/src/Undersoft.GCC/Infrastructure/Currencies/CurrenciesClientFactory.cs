using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Utilities;

namespace Undersoft.GCC.Infrastructure.Currencies;

public class CurrenciesClientFactory<TClient> : CurrenciesClientFactory where TClient : CurrenciesClient
{
    public CurrenciesClientFactory(CurrencyProvider provider) : base(provider)
    {
    }

    public override CurrenciesClient CreateClient()
    {
        return typeof(TClient).New<TClient>(_provider);
    }
}

public abstract class CurrenciesClientFactory
{
    protected Uri _baseUri;
    protected CurrencyProvider _provider;

    public CurrenciesClientFactory(CurrencyProvider provider)
    {
        if (provider == null)
            throw new ArgumentNullException(nameof(provider));
        _baseUri = new Uri(provider.BaseUri!);
        _provider = provider;
    }

    public abstract CurrenciesClient CreateClient();
}