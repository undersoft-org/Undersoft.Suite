using Microsoft.OData.Client;
using Microsoft.OData.Edm;
using Undersoft.SDK.Service.Access;

namespace Undersoft.SDK.Service.Data.Client
{
    public partial class OpenDataContext : DataServiceContext
    {
        protected ApiDataContext ApiContext;

        private IAccessString _securityString;

        public OpenDataContext(Uri serviceUri) : base(serviceUri)
        {
            if (serviceUri == null)
                throw new ArgumentNullException(nameof(serviceUri));

            ApiContext = new ApiDataContext(
                new Uri(serviceUri.OriginalString.Replace("open", "api"))
            );

            MergeOption = MergeOption.NoTracking;

            IgnoreResourceNotFoundException = true;

            AutoNullPropagation = true;
            HttpRequestTransportMode = HttpRequestTransportMode.HttpClient;
            DisableInstanceAnnotationMaterialization = true;
            EnableWritingODataAnnotationWithoutPrefix = true;        
            AddAndUpdateResponsePreference = DataServiceResponsePreference.None;
            SaveChangesDefaultOptions = SaveChangesOptions.ContinueOnError;
            ResolveName = (t) => this.GetMappedName(t);
            ResolveType = (n) => this.GetMappedType(n);            
            BuildingRequest += RequestAuthorization;
            Format.LoadServiceModel = GetServiceModel;
        }

        public Registry<RemoteRelation> Remotes { get; set; } = new Registry<RemoteRelation>(true);

        private void RequestAuthorization(object sender, BuildingRequestEventArgs e)
        {  
            if (_securityString != null)
                e.Headers.Add("Authorization", $"Bearer {_securityString.Encoded}");
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
                var token = securityString.Split(" ").LastOrDefault();
                _securityString = new AccessString(token);
                ApiContext.SetAuthorization(token);
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
                return responseContents;
            }
            return new string[0];
        }
    }
}
