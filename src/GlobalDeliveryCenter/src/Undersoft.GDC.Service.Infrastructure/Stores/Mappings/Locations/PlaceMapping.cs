using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings.Locations;

using Undersoft.SDK.Service.Access.Identity;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public class PlaceMappings : EntityTypeMapping<Place>
{
    private const string TABLE_NAME = "Places";

    public override void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
              .RelateOneToSet<Place, Endpoint>(
                  r => r.Place,
                  r => r.Endpoints,
                  ExpandSite.OnRight
              );
    }
}