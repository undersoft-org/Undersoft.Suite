using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class GroupMappings : EntityTypeMapping<Group>
    {
        const string TABLE_NAME = "Groups";

        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
        }
    }
}
