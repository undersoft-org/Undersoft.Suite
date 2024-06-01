using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public class ContactPersonalMappings : EntityTypeMapping<ContactPersonal>
{
    const string TABLE_NAME = "ContactPersonals";

    public override void Configure(EntityTypeBuilder<ContactPersonal> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
    }
}