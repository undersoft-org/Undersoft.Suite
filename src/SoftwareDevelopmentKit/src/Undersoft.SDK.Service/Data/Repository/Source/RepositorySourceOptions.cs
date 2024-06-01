using System.Data.Common;

namespace Undersoft.SDK.Service.Data.Repository.Source;

public class RepositorySourceOptions
{
    DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder();

    public string ConnectionString { get => dbConnectionStringBuilder.ConnectionString; set => dbConnectionStringBuilder.ConnectionString = value; }

    public string Host { get => (string)dbConnectionStringBuilder["Hosting"]; set => dbConnectionStringBuilder["Hosting"] = value; }

    public int Port { get => (int)dbConnectionStringBuilder["Port"]; set => dbConnectionStringBuilder["Port"] = value; }

    public string UserId { get => (string)dbConnectionStringBuilder["UserId"]; set => dbConnectionStringBuilder["UserId"] = value; }

    public string Database { get => (string)dbConnectionStringBuilder["Database"]; set => dbConnectionStringBuilder["Database"] = value; }

    public string Password { get => (string)dbConnectionStringBuilder["Password"]; set => dbConnectionStringBuilder["Password"] = value; }

    public bool Pooling { get; set; }

    public string SourceProvider { get; set; }

    public int PoolSize { get; set; }
}
