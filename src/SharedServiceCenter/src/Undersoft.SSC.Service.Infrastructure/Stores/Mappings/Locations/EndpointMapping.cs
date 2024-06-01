using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SSC.Service.Infrastructure.Stores.Mappings.Locations;

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;
using Undersoft.SSC.Domain.Entities.Locations;

public class EndpointMappings : EntityTypeMapping<Endpoint>
{
    private const string TABLE_NAME = "Endpoints";

    public override void Configure(EntityTypeBuilder<Endpoint> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
    }
}