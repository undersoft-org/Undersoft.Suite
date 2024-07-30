using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.Currencies
{
    public class CurrenciesOptions
    {
        public CurrenciesOptions()
        {
        }

        public Dictionary<string, CurrencyProvider>? Providers { get; set; }
    }
}
