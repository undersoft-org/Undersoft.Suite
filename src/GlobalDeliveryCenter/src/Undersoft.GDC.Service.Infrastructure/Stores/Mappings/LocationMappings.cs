using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.GDC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class LocationMappings : EntityTypeMapping<Location>
    {
        const string TABLE_NAME = "Locations";

        public override void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateSetToSet<Location, Address>(
                    r => r.Locations,
                    r => r.Addresses,
                    ExpandSite.OnRight
                )
                .RelateOneToSet<Location, Place>(
                    r => r.Location,
                    r => r.Places,
                    ExpandSite.OnRight
                );
        }
    }
}
