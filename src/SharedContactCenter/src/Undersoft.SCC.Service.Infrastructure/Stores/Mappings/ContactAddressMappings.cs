using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities.Contacts;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class ContactAddressMappings : EntityTypeMapping<ContactAddress>
    {
        const string TABLE_NAME = "ContactAddresses";

        public override void Configure(EntityTypeBuilder<ContactAddress> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
        }
    }
}
