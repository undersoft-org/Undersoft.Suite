using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SSC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SSC.Domain.Entities;

    public class DefaultMappings : EntityTypeMapping<Default>
    {
        const string TABLE_NAME = "Defaults";

        public override void Configure(EntityTypeBuilder<Default> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToSet<Default, Detail>(
                    r => r.Default,
                    r => r.Details,
                    ExpandSite.OnRight
                )
                .RelateOneToSet<Default, Setting>(
                    r => r.Default,
                    r => r.Settings,
                    ExpandSite.OnRight
                );
        }
    }
}
