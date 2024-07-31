using Undersoft.GCC.Domain.Entities;

namespace Undersoft.GCC.Infrastructure.Currencies.NBP
{
    public class NBPCurrenciesClient : CurrenciesClient
    {
        static readonly char[] TABLE_GROUP = ['a', 'b', 'c'];

        public NBPCurrenciesClient(CurrencyProvider provider) : base(provider)
        {
        }

        public override IEnumerable<int> GetCurrenciesStream(out MemoryStream stream)
        {
            return GetRatesStream(out stream);
        }

        public override IEnumerable<int> GetLatestRatesStream(out MemoryStream stream)
        {
            return GetRatesStream(out stream);
        }

        public override IEnumerable<int> GetTodayRatesStream(out MemoryStream stream)
        {
            return GetRatesStream(out stream, true);
        }

        public override IEnumerable<int> GetAllRatesStream(out MemoryStream stream)
        {
            var date = DateTimeOffset.UtcNow;
            stream = new MemoryStream();
            IList<int> listing = new List<int>();
            do
            {
                string timeRoute = $"{date.AddDays(-90).ToString("yyyy-MM-dd")}/{date.ToString("yyyy-MM-dd")}/";

                for (int i = 0; i < TABLE_GROUP.Length; i++)
                    listing.Add(WriteResponseToStream(new Uri($"{_baseUri.AbsoluteUri}/tables/{TABLE_GROUP[i]}/{timeRoute}?format=json"), ref stream));

                date = date.AddDays(-91);
            }
            while (date > _provider.HistorySince);

            return listing;
        }

        private IEnumerable<int> GetRatesStream(out MemoryStream stream, bool publishedToday = false)
        {

            string today = publishedToday ? "today/" : "";
            stream = new MemoryStream();
            IList<int> listing = new List<int>();

            for (int i = 0; i < TABLE_GROUP.Length; i++)
                listing.Add(WriteResponseToStream(new Uri($"{_baseUri.AbsoluteUri}/tables/{TABLE_GROUP[i]}/{today}?format=json"), ref stream));

            return listing;
        }
    }
}
