using Microsoft.EntityFrameworkCore;

namespace Undersoft.SDK.Service.Data.Repository.Source
{
    public interface ISourceProviderConfiguration
    {
        IServiceRegistry AddSourceProvider(SourceProvider provider);
        IServiceRegistry RegisterSourceProvider(SourceProvider provider);
        DbContextOptionsBuilder BuildOptions(DbContextOptionsBuilder builder, SourceProvider provider, string connectionString);
    }
}