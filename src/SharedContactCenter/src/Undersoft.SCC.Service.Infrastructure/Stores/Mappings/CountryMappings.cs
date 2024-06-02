using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;
    using Undersoft.SCC.Domain.Entities.Countries;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class CountryMappings : EntityTypeMapping<Country>
    {
        const string TABLE_NAME = "Countries";

        public override void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToSet<Country, CountryState>(
                    c => c.Country,
                    c => c.States,
                    ExpandSite.OnRight,
                    true
                );
        }
    }
}
