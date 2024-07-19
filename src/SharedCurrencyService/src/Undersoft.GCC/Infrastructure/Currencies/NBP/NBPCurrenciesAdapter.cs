using Undersoft.GCC.Domain.Entities;
using Undersoft.GCC.Infrastructure.Currencies;
using Undersoft.SDK.Uniques;

namespace Undersoft.GCC.Infrastructure.Currencies.NBP;

public class NBPCurrenciesAdapter : CurrenciesJsonAdapter
{
    public NBPCurrenciesAdapter(CurrencyProvider provider) : base(provider) { }

    public override IEnumerable<Currency> ReadCurrencies(MemoryStream stream, IEnumerable<int> sizes)
    {
        var documents = GetJsonDocuments(stream, sizes);

        foreach (var document in documents)
        {
            foreach (var table in document.RootElement.EnumerateArray())
            {
                foreach (var element in table.GetProperty("rates").EnumerateArray())
                {
                    yield return new Currency()
                    {
                        CurrencyCode = element.GetProperty("code").GetString(),
                        Name = element.GetProperty("currency").GetString(),
                    };
                }
            }
        }
    }

    public override IEnumerable<CurrencyRate> ReadCurrencyRates(MemoryStream stream, IEnumerable<int> sizes)
    {
        var documents = GetJsonDocuments(stream, sizes);

        foreach (var document in documents)
        {
            foreach (var table in document.RootElement.EnumerateArray())
            {
                if (!table.TryGetProperty("effectiveDate", out var dateElemenet))
                    continue;

                if (!DateTime.TryParse(dateElemenet.GetString(), out DateTime publishDate))
                    continue;

                var rateTableNumber = "";
                if (table.TryGetProperty("no", out var noELement))
                    rateTableNumber = noELement.GetString();

                var rateTypes = new[] { "mid" };
                if (table.TryGetProperty("table", out var tableElement) && tableElement.GetString()!.Equals("c"))
                    rateTypes = new[] { "bid", "ask" };

                if (!table.TryGetProperty("rates", out var ratesElement))
                    continue;

                foreach (var rates in ratesElement.EnumerateArray())
                {
                    if (!rates.TryGetProperty("code", out var codeElement))
                        continue;

                    foreach (var rateType in rateTypes)
                    {
                        if (!rates.TryGetProperty(rateType, out var rateElement))
                            continue;

                        yield return new CurrencyRate()
                        {
                            ProviderId = _provider.Id,
                            Label = rateTableNumber,
                            SourceCurrencyId = _provider.BaseCurrencyId,
                            SourceRate = 1.00D,
                            TargetCurrencyId = codeElement.GetString().UniqueKey64(),
                            TargetRate = rateElement.GetDouble(),
                            PublishDate = publishDate,
                            Type = Enum.Parse<CurrencyRateType>(rateType, true)
                        };
                    }
                }
            }
        }
    }
}
