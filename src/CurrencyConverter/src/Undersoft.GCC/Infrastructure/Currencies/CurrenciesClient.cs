using Undersoft.GCC.Domain.Entities;
using Undersoft.SDK.Logging;

namespace Undersoft.GCC.Infrastructure.Currencies
{
    public abstract class CurrenciesClient : HttpClient
    {
        protected Uri _baseUri;
        protected CurrencyProvider _provider;

        public CurrenciesClient(CurrencyProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            _baseUri = new Uri(provider.BaseUri!);
            _provider = provider;
        }

        public abstract IEnumerable<int> GetCurrenciesStream(out MemoryStream stream);

        public abstract IEnumerable<int> GetTodayRatesStream(out MemoryStream stream);

        public abstract IEnumerable<int> GetLatestRatesStream(out MemoryStream stream);

        public abstract IEnumerable<int> GetAllRatesStream(out MemoryStream stream);

        public virtual int WriteResponseToStream(Uri uri, ref MemoryStream stream)
        {
            int retryCount = 5;
            Task<byte[]> buf = null!;
            do
            {
                retryCount--;
                try
                {
                    buf = GetByteArrayAsync(uri);
                }
                catch (Exception ex)
                {
                    this.Failure<Netlog>("Getting data from http client failed");
                }

            }
            while ((buf == null || !buf.Wait(10000)) && retryCount > 0);

            int size = 0;
            if (retryCount > 0)
            {
                size = buf!.Result.Length;
                stream.Write(buf.Result, 0, size);
                this.Info<Netlog>($"Succesfully load currency provider {uri.Host} data from endpoint {uri.AbsolutePath}");
            }
            else
                this.Failure<Netlog>("Retrying load data from currency data service failed!", _provider);

            return size;
        }
    }
}