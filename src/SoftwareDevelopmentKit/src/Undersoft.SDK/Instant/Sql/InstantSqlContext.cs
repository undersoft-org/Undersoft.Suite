using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Undersoft.SDK.Instant.Sql
{
    public class InstantSqlContext : InstantSqlDb
    {
        public InstantSqlContext(InstantSqlOptions identity) : base(identity) { }

        public InstantSqlContext(string connectionString) : base(connectionString) { }

        public InstantSqlContext(IConfiguration configuration, string connectionName)
            : base(configuration.GetConnectionString(connectionName)) { }

        public InstantSqlContext(IConfiguration configuration)
            : base(
                configuration.GetSection("ConnectionString")?.GetChildren()?.FirstOrDefault()?.Value
            )
        { }
    }
}
