using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities.Contacts;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class ContactProfessionalMappings : EntityTypeMapping<ContactProfessional>
    {
        const string TABLE_NAME = "ContactProfessionals";

        public override void Configure(EntityTypeBuilder<ContactProfessional> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
        }
    }
}
