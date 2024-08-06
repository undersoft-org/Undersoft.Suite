using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.GDC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class GroupMappings : EntityTypeMapping<Group>
    {
        const string TABLE_NAME = "Groups";

        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateSetToSet<Group, Group>(
                    r => r.RelatedFrom,
                    nameof(Group),
                    r => r.RelatedTo,
                    nameof(Group),
                    ExpandSite.OnRight
                );
            ;
        }
    }
}
