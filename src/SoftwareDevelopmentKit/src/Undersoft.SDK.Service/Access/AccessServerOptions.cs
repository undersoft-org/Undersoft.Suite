namespace Undersoft.SDK.Service.Access
{
    public class AccessServerOptions
    {
        public string ServiceName { get; set; }

        public string ServiceVersion { get; set; }

        public string ServerBaseUrl { get; set; }

        public string ServiceBaseUrl { get; set; }

        public string ServiceClientId { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string[] Scopes { get; set; }

        public string[] Roles { get; set; }

        public string[] Claims { get; set; }

        public string AdministrationRole { get; set; }

        public bool CorsAllowAnyOrigin { get; set; }

        public string[] CorsAllowOrigins { get; set; }
    }
}