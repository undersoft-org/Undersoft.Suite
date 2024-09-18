using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.Currencies.Frankfurter
{
    public class FrankfurterCurrenciesClient : CurrenciesClient
    {
        public FrankfurterCurrenciesClient(CurrencyProvider provider) : base(provider) { }

        public override IEnumerable<int> GetAllRatesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}{_provider.HistorySince.ToString("yyyy-MM-dd")}");

            return WriteResponseToStream(route, ref stream).ToEnumerable(); ;
        }

        public override IEnumerable<int> GetCurrenciesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}currencies");

            return WriteResponseToStream(route, ref stream).ToEnumerable(); ;
        }

        public override IEnumerable<int> GetLatestRatesStream(out MemoryStream stream)
        {
            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}latest");

            return WriteResponseToStream(route, ref stream).ToEnumerable(); ;
        }

        public override IEnumerable<int> GetTodayRatesStream(out MemoryStream stream)
        {
            stream = default!;
            var now = DateTime.UtcNow;

            if (now.Hour < _provider.UpdateHour
                || now.Minute < _provider.UpdateMinute)
                return [];

            stream = new MemoryStream();
            Uri route = new Uri($"{_baseUri.AbsoluteUri}latest");

            return WriteResponseToStream(route, ref stream).ToEnumerable();
        }
    }
}
