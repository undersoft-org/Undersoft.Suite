using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings.Locations;

using Undersoft.SDK.Service.Access.Identity;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public class AddressesMappings : EntityTypeMapping<Address>
{
    private const string TABLE_NAME = "Addresses";

    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
    }
}