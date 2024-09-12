using IdentityModel.Client;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class ApiDataClient : HttpClient
    {

        public ApiDataClient(Uri serviceUri)
        {
            if (serviceUri == null)
                throw new ArgumentNullException(nameof(serviceUri));
            BaseAddress = new Uri(serviceUri.OriginalString + "/");
            DefaultRequestVersion = HttpVersion.Version20;
            DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;

            Timeout = TimeSpan.FromMinutes(5);
            this.DefaultRequestHeaders.Add("page", "0");
            this.DefaultRequestHeaders.Add("limit", "0");
            this.DefaultRequestHeaders.Remove(@"Accept");
            this.DefaultRequestHeaders.Add(@"Accept", @"application/json");
        }

        public void SetAuthorization(string token)
        {
            if (token != null)
            {               
                this.SetBearerToken(token);
            }
        }

        public void SetPagination(int page, int limit)
        {
            if (page > 0 && limit > 0)
            {
                Page = page;
                Limit = limit;
            }
        }

        public int Page
        {
            get => Int32.Parse(this.DefaultRequestHeaders.Where(h => h.Key == "page").First().Value.First());
            set
            {
                this.DefaultRequestHeaders.Remove("page"); this.DefaultRequestHeaders.Add("page", value.ToString());
            }
        }
        public int Limit
        {
            get => Int32.Parse(this.DefaultRequestHeaders.Where(h => h.Key == "limit").First().Value.First());
            set
            {
                this.DefaultRequestHeaders.Remove("limit"); this.DefaultRequestHeaders.Add("limit", value.ToString());
            }
        }

        public async Task<int> Count<TContract>()
        {
            var response = await this.GetAsync("count");
            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<TContract>> Get<TContract>(int page = 0, int limit = 0) where TContract : IOrigin, IInnerProxy
        {
            SetPagination(page, limit);
            return await this.GetFromJsonAsync<IEnumerable<TContract>>(typeof(TContract).Name);
        }
        public async Task<TContract> Get<TContract>(object key) where TContract : IOrigin, IInnerProxy
        {
            return await this.GetFromJsonAsync<TContract>($"{typeof(TContract).Name}/{key.ToString()}");
        }

        public async Task<string> Create<TContract>(object key, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            return await (await this.PostAsJsonAsync<TContract>($"{typeof(TContract).Name}/{key.ToString()}", contract)).Content.ReadAsStringAsync();
        }
        public async Task<string> Create<TContract>(TContract[] contracts) where TContract : IOrigin, IInnerProxy
        {
            return await (await this.PostAsJsonAsync<TContract[]>($"{typeof(TContract).Name}", contracts)).Content.ReadAsStringAsync();
        }

        public async Task<string> Change<TContract>(object key, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            return await (await this.PatchAsJsonAsync<TContract>($"{typeof(TContract).Name}/{key.ToString()}", contract)).Content.ReadAsStringAsync();
        }
        public async Task<string> Change<TContract>(TContract[] contracts) where TContract : IOrigin, IInnerProxy
        {
            return await (await this.PatchAsJsonAsync<TContract[]>($"{typeof(TContract).Name}", contracts)).Content.ReadAsStringAsync();
        }

        public async Task<string> Update<TContract>(object key, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            return await (await this.PutAsJsonAsync<TContract>($"{typeof(TContract).Name}/{key.ToString()}", contract)).Content.ReadAsStringAsync();
        }
        public async Task<string> Update<TContract>(TContract[] contracts) where TContract : IOrigin, IInnerProxy
        {
            return await (await this.PutAsJsonAsync<TContract[]>($"{typeof(TContract).Name}", contracts)).Content.ReadAsStringAsync();
        }

        public async Task<string> Delete<TContract>(object key, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{typeof(TContract).Name}/{key.ToString()}");
            request.Content = new ByteArrayContent(contract.ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadAsStringAsync();
        }
        public async Task<string> Delete<TContract>(TContract[] contracts) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{typeof(TContract).Name}");
            request.Content = new ByteArrayContent(contracts.ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri, HttpContent content)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            request.Content = content;
            return await this.SendAsync(request);
        }

        public async Task<TContract> Action<TContract>(string method, Arguments arguments) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{typeof(TContract).Name}/Action/{method}");
            request.Content = new ByteArrayContent(arguments.ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadFromJsonAsync<TContract>();
        }
        public async Task<TResult> Action<TContract, TResult>(string method, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{typeof(TContract).Name}/Action/{method}");
            request.Content = new ByteArrayContent(new Arguments(method, contract).ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadFromJsonAsync<TResult>();
        }

        public async Task<TContract> Access<TContract>(string method, Arguments arguments) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{typeof(TContract).Name}/Access/{method}");
            request.Content = new ByteArrayContent(arguments.ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadFromJsonAsync<TContract>();
        }
        public async Task<TResult> Access<TContract, TResult>(string method, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{typeof(TContract).Name}/Access/{method}");
            request.Content = new ByteArrayContent(new Arguments(method, contract).ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadFromJsonAsync<TResult>();
        }

        public async Task<TContract> Setup<TContract>(string method, Arguments arguments) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{typeof(TContract).Name}/Setup/{method}");
            request.Content = new ByteArrayContent(arguments.ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadFromJsonAsync<TContract>();
        }
        public async Task<TResult> Setup<TContract, TResult>(string method, TContract contract) where TContract : IOrigin, IInnerProxy
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{typeof(TContract).Name}/Setup/{method}");
            request.Content = new ByteArrayContent(new Arguments(method, contract).ToJsonBytes());
            return await (await this.SendAsync(request)).Content.ReadFromJsonAsync<TResult>();
        }
    }
}