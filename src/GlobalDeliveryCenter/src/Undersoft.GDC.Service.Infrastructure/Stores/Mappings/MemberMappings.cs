﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.GDC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class MemberMappings : EntityTypeMapping<Member>
    {
        const string TABLE_NAME = "Members";

        public override void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Member>()
                .RelateSetToSet<Member, Service>(
                    r => r.Members,
                    r => r.Services,
                    ExpandSite.OnRight
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
                    ExpandSite.OnRight,
                    true
                )
                 .RelateSetToSet<Member, Group>(
                    l => l.Members,
                    r => r.Groups,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Member, Member>(
                    r => r.RelatedFrom,
                    $"From{nameof(Member)}",
                    r => r.RelatedTo,
                    nameof(Member),
                    ExpandSite.OnRight);
            ;
        }
    }
}
