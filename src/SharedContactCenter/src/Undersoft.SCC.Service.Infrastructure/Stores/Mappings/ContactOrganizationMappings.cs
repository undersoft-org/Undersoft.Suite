using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities.Contacts;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class ContactOrganizationMappings : EntityTypeMapping<ContactOrganization>
    {
        const string TABLE_NAME = "ContactOrganizations";

        public override void Configure(EntityTypeBuilder<ContactOrganization> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
        }
    }
}
