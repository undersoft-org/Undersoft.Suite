using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities.Countries;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class CountryStateMappings : EntityTypeMapping<CountryState>
    {
        const string TABLE_NAME = "CountryStates";

        public override void Configure(EntityTypeBuilder<CountryState> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        }
    }
}
