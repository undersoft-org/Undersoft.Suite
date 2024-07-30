using Undersoft.GCC.Domain.Entities;
using Undersoft.GCC.Infrastructure.Currencies;

namespace Undersoft.GCC.Infrastructure.Currencies.ECB
{
    public class ECBCurrenciesClient : CurrenciesClient
    {
        public ECBCurrenciesClient(CurrencyProvider provider) : base(provider)
        {
        }

        public override IEnumerable<int> GetAllRatesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}/eurofxref-hist.xml");

            return WriteResponseToStream(route, ref stream).ToEnumerable();
        }

        public override IEnumerable<int> GetCurrenciesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}/eurofxref-daily.xml");

            return WriteResponseToStream(route, ref stream).ToEnumerable();
        }

        public override IEnumerable<int> GetLatestRatesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}/eurofxref-daily.xml");

            return WriteResponseToStream(route, ref stream).ToEnumerable();
        }

        public override IEnumerable<int> GetTodayRatesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            var now = DateTime.UtcNow;
            if (now.Hour < _provider.UpdateHour
                || now.Minute < _provider.UpdateMinute)
                return [];

            Uri route = new Uri($"{_baseUri.AbsoluteUri}/eurofxref-daily.xml");

            return WriteResponseToStream(route, ref stream).ToEnumerable();
        }
    }
}

// https://www.ecb.europa.eu/stats/eurofxref/eurofxref-sdmx.xml
// https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist.xml
// https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml
// https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml