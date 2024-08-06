using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.GDC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class ActivityMappings : EntityTypeMapping<Activity>
    {
        const string TABLE_NAME = "Activities";

        public override void Configure(EntityTypeBuilder<Activity> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Activity>()
                .RelateOneToOne<Activity, Location>(
                    r => r.Activity,
                    r => r.Location,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Activity, Setting>(
                    l => l.Activities,
                    r => r.Settings,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Activity, Group>(
                    l => l.Activities,
                    r => r.Groups,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Activity, Detail>(
                    l => l.Activities,
                    r => r.Details,
                    ExpandSite.OnRight, true
                );
        }
    }
}
