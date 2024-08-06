using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings;

using Undersoft.GDC.Domain.Entities;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;
using Undersoft.SDK.Service.Infrastructure.Database.Relation;

public class ScheduleMappings : EntityTypeMapping<Schedule>
{
    const string TABLE_NAME = "Schedules";

    public override void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
            .ApplyIdentifiers<Schedule>()
            .RelateSetToSet<Schedule, Detail>(
                r => r.Schedules,
                r => r.Details,
                ExpandSite.OnRight
            )
            .RelateSetToSet<Schedule, Setting>(
                r => r.Schedules,
                r => r.Settings,
                ExpandSite.OnRight
            )
            .RelateOneToOne<Schedule, Location>(
                r => r.Schedule,
                r => r.Location,
                ExpandSite.OnRight
            ).RelateSetToSet<Schedule, Group>(
                    l => l.Schedules,
                    r => r.Groups,
                    ExpandSite.OnRight
            );
    }
}
