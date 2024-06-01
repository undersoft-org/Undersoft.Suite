namespace Undersoft.SDK.Service.Data.Repository;

using Undersoft.SDK.Service.Data.Repository.Client;
using Undersoft.SDK.Service.Data.Repository.Source;

public class RepositoryOptions
{
    public Dictionary<string, RepositorySourceOptions> Sources { get; set; }

    public Dictionary<string, RepositoryClientOptions> Clients { get; set; }
}
