namespace Undersoft.SDK.Service.Configuration;
using Microsoft.Extensions.Configuration;
using Undersoft.SDK.Service.Access;
using Undersoft.SDK.Service.Data.Client;
using Undersoft.SDK.Service.Data.Store;

public interface IServiceConfiguration : IConfiguration
{
    string Name { get; }
    string BaseUrl { get; }
    string Version { get; }
    string TypeName { get; }
    IServiceConfiguration Configure<TOptions>(string sectionName) where TOptions : class;
    IConfigurationSection Client(string name);
    IConfigurationSection DataCacheLifeTime();
    int ClientPoolSize(IConfigurationSection endpoint);
    string ClientConnectionString(IConfigurationSection client);
    string ClientConnectionString(string name);
    ClientProvider ClientProvider(IConfigurationSection client);
    ClientProvider ClientProvider(string name);
    IEnumerable<IConfigurationSection> Clients();

    string StoreRoutes(string name);
    IConfigurationSection Source(string name);
    int SourcePoolSize(IConfigurationSection endpoint);
    string SourceConnectionString(IConfigurationSection endpoint);
    string SourceConnectionString(string name);
    SourceProvider SourceProvider(IConfigurationSection endpoint);
    SourceProvider SourceProvider(string name);
    IEnumerable<IConfigurationSection> Sources();

    IConfigurationSection Repository();
    IConfigurationSection AccessServer();
    string IdentityServerBaseUrl();
    string IdentityServiceName();
    string[] IdentityServerScopes();
    string[] IdentityServerRoles();
    AccessOptions GetAccessServerConfiguration();
    AccessOptions AccessOptions { get; }
}