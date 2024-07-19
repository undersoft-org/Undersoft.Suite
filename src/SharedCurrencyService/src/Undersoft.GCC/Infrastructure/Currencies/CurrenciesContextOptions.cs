using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.Currencies
{
    public class CurrenciesContextOptions<TContext> : CurrenciesContextOptions where TContext : CurrenciesContext
    {
        public CurrenciesContextOptions(Action<CurrencyProvider> setup) : base(setup) { }

        public CurrenciesContextOptions(CurrencyProvider provider) : base(provider) { }
    }

    public class CurrenciesContextOptions
    {
        public CurrenciesContextOptions(Action<CurrencyProvider> setup) : this(new CurrencyProvider())
        {
            setup(Provider);
        }

        public CurrenciesContextOptions(CurrencyProvider provider)
        {
            Provider = provider;
        }

        public CurrencyProvider Provider { get; set; }
    }
}
