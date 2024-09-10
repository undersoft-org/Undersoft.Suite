using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SSC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SSC.Domain.Entities;

    public class ActivityMappings : EntityTypeMapping<Activity>
    {
        const string TABLE_NAME = "Activities";

        public override void Configure(EntityTypeBuilder<Activity> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Activity>()
                .RelateSetToSet<Activity, Resource>(
                    r => r.Activities,
                    r => r.Resources,
                    ExpandSite.OnRight
                )
                .RelateOneToSet<Default, Activity>(
                    r => r.Default,
                    r => r.Activities,
                    ExpandSite.OnLeft
                )
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
                .RelateSetToSet<Activity, Detail>(
                    l => l.Activities,
                    r => r.Details,
                    ExpandSite.OnRight, true
                )
                .RelateSetToSet<Activity, Activity>(
                    rm => rm.RelatedFrom,
                    nameof(Activity),
                    rm => rm.RelatedTo,
                    nameof(Activity),
                    ExpandSite.OnRight
                );
        }
    }
}
