using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SBC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SBC.Domain.Entities;

    public class MemberMappings : EntityTypeMapping<Member>
    {
        const string TABLE_NAME = "Members";

        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Member>()
                .RelateSetToSet<Member, Activity>(
                    r => r.Members,
                    r => r.Activities, ExpandSite.OnRight
                )
                .RelateSetToSet<Member, Resource>(
                    r => r.Members,
                    r => r.Resources, ExpandSite.OnRight
                )
                .RelateSetToSet<Member, Schedule>(
                    r => r.Members,
                    r => r.Schedules, ExpandSite.OnRight
                )
                .RelateSetToSet<Member, Setting>(
                    r => r.Members,
                    r => r.Settings,
                    ExpandSite.OnRight
                )
                .RelateOneToOne<Member, Location>(
                    r => r.Member,
                    r => r.Location,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Member, Detail>(
                    r => r.Members,
                    r => r.Details,
                    ExpandSite.OnRight, true
                )
                .RelateOneToSet<Default, Member>(
                    r => r.Default,
                    r => r.Members,
                    ExpandSite.OnLeft
                )
                .RelateSetToSet<Member, Member>(
                    r => r.RelatedFrom,
                    nameof(Member),
                    r => r.RelatedTo,
                    nameof(Member),
                    ExpandSite.OnRight
                );
            ;
        }
    }
}
