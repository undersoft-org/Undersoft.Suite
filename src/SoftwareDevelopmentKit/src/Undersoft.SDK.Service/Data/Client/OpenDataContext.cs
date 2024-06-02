using Microsoft.OData.Client;
using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class OpenDataContext : DataServiceContext
    {
        protected ApiDataContext ApiContext;

        private ISecurityString _securityString;

        public OpenDataContext(Uri serviceUri) : base(serviceUri)
        {
            if (serviceUri == null)
                throw new ArgumentNullException(nameof(serviceUri));

            ApiContext = new ApiDataContext(
                new Uri(serviceUri.OriginalString.Replace("open", "api"))
            );

            MergeOption = MergeOption.AppendOnly;

            IgnoreResourceNotFoundException = true;

            AutoNullPropagation = true;
            HttpRequestTransportMode = HttpRequestTransportMode.HttpClient;
            DisableInstanceAnnotationMaterialization = false;
            EnableWritingODataAnnotationWithoutPrefix = true;
            AddAndUpdateResponsePreference = DataServiceResponsePreference.None;
            SaveChangesDefaultOptions = SaveChangesOptions.BatchWithSingleChangeset;
            ResolveName = (t) => this.GetMappedName(t);
            ResolveType = (n) => this.GetMappedType(n);
            SendingRequest2 += RequestAuthorization;
            Format.LoadServiceModel = GetServiceModel;
            //ResolveEntitySet = (s) => 
        }

        //public Uri ResolveEntitySet(string  entitySetUri)
        //{

        //}

        public Registry<RemoteRelation> Remotes { get; set; } = new Registry<RemoteRelation>(true);

        private void RequestAuthorization(object sender, SendingRequest2EventArgs e)
        {
            if (_securityString != null)
                e.RequestMessage.SetHeader("Authorization", _securityString.Encoded);
        }

        public async Task<IEdmModel> CreateServiceModel()
        {
            var edmModel = await AddServiceModel();
            Format.UseJson();
            return edmModel;
        }

        public async Task<IEdmModel> AddServiceModel()
        {
            string t = GetType().FullName;
            if (!OpenDataRegistry.EdmModels.TryGet(t, out IEdmModel edmModel))
                OpenDataRegistry.EdmModels.Add(
                    t,
                    edmModel = OnModelCreating(await this.GetEdmModelAsync())
                );
            return edmModel;
        }

        public IEdmModel GetServiceModel()
        {
            return OpenDataRegistry.EdmModels.Get(GetType().FullName);
        }

        protected virtual IEdmModel OnModelCreating(IEdmModel builder)
        {
            return builder;
        }

        public override DataServiceQuery<T> CreateQuery<T>(string resourcePath, bool isComposable)
        {
            return base.CreateQuery<T>(resourcePath, isComposable);
        }

        public void SetAuthorization(string securityString)
        {
            _securityString = null;

            if (securityString != null)
            {
                var strings = securityString.Split(" ");
                string prefix = strings.Length > 0 ? strings[0] : null;
                _securityString = new SecurityString(strings.LastOrDefault(), prefix);
            }
        }

        public virtual Task CommandAsync<TEntity>(CommandType command, TEntity payload, string name)
        {
            return ApiContext.CommandAsync(command, payload, name);
        }

        public virtual Task CommandSetAsync<TEntity>(
            CommandType command,
            IEnumerable<TEntity> payload,
            string name
        )
        {
            return ApiContext.CommandSetAsync(command, payload, name);
        }

        public virtual void Command<TEntity>(CommandType command, TEntity payload, string name)
        {
            ApiContext.Command(command, payload, name);
        }

        public virtual void CommandSet<TEntity>(
            CommandType command,
            IEnumerable<TEntity> payload,
            string name
        )
        {
            ApiContext.CommandSet(command, payload, name);
        }

        public async Task<string[]> CommitChanges(bool changesets = false)
        {
            var responseContents = await ApiContext.SendCommands(changesets);
            if (responseContents != null)
            {
                return await Task.WhenAll(responseContents);
            }
            return new string[0];
        }
    }
}
