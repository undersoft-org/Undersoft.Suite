using Undersoft.GCC.Domain.Entities;
using Undersoft.GCC.Infrastructure.Currencies;
using Undersoft.SDK.Uniques;

namespace Undersoft.GCC.Infrastructure.Currencies.Frankfurter
{
    public class FrankfurterCurrenciesAdapter : CurrenciesJsonAdapter
    {
        public FrankfurterCurrenciesAdapter(CurrencyProvider provider) : base(provider) { }

        public override IEnumerable<Currency> ReadCurrencies(MemoryStream stream, IEnumerable<int> sizes)
        {
            var documents = GetJsonDocuments(stream, sizes);

            foreach (var document in documents)
            {
                foreach (var property in document.RootElement.EnumerateObject())
                {
                    yield return new Currency()
                    {
                        CurrencyCode = property.Name,
                        Name = property.Value.GetString(),
                    };
                }
            }
        }

        public override IEnumerable<CurrencyRate> ReadCurrencyRates(MemoryStream stream, IEnumerable<int> sizes)
        {
            var documents = GetJsonDocuments(stream, sizes);

            foreach (var document in documents)
            {
                foreach (var dateNamedProperty in document.RootElement.GetProperty("rates").EnumerateObject())
                {
                    if (!DateTime.TryParse(dateNamedProperty.Name, out DateTime publishDate))
                        continue;

                    foreach (var rate in dateNamedProperty.Value.EnumerateObject())
                    {
                        yield return new CurrencyRate()
                        {
                            ProviderId = _provider.Id,
                            SourceCurrencyId = _provider.BaseCurrencyId,
                            SourceRate = 1.00D,
                            TargetCurrencyId = rate.Name.UniqueKey64(),
                            TargetRate = rate.Value.GetDouble(),
                            PublishDate = publishDate
                        };
                    }
                }
            }
        }
    }
}
