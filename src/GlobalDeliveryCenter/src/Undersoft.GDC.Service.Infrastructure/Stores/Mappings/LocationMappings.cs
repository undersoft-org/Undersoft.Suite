using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Access.Identity;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class LocationMappings : EntityTypeMapping<Domain.Entities.Location>
    {
        const string TABLE_NAME = "Locations";

        public override void Configure(EntityTypeBuilder<Domain.Entities.Location> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToSet<Domain.Entities.Location, Address>(
                    r => r.Locations,
                    r => r.Addresses,
                    ExpandSite.OnRight
                )
                .RelateOneToSet<Domain.Entities.Location, Place>(
                    r => r.Location,
                    r => r.Places,
                    ExpandSite.OnRight
                );
        }
    }
}
