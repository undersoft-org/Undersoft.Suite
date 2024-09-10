using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SBC.Service.Infrastructure.Stores.Mappings;

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;
using Undersoft.SDK.Service.Infrastructure.Database.Relation;
using Undersoft.SBC.Domain.Entities;

public class ScheduleMappings : EntityTypeMapping<Schedule>
{
    const string TABLE_NAME = "Schedules";

    public override void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
            .ApplyIdentifiers<Schedule>()
            .RelateSetToSet<Schedule, Activity>(r => r.Schedules, r => r.Activities, ExpandSite.OnRight)
            .RelateSetToSet<Schedule, Setting>(
                r => r.Schedules,
                r => r.Settings,
                ExpandSite.OnRight
            )
            .RelateOneToOne<Schedule, Location>(
                r => r.Schedule,
                r => r.Location,
                ExpandSite.OnRight
            )
            .RelateSetToSet<Schedule, Detail>(r => r.Schedules, r => r.Details, ExpandSite.OnRight)
            .RelateOneToSet<Default, Schedule>(r => r.Default, r => r.Schedules, ExpandSite.OnLeft)
            .RelateSetToSet<Schedule, Schedule>(
                r => r.RelatedFrom,
                nameof(Schedule),
                r => r.RelatedTo,
                nameof(Schedule),
                ExpandSite.OnRight
            );
    }
}
