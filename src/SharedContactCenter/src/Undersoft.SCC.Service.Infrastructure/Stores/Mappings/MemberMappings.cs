using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class MemberMappings : EntityTypeMapping<Member>
    {
        const string TABLE_NAME = "Members";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Member> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Member>()
                 .RelateSetToSet<Member, Member>(
                    r => r.RelatedFrom,
                    nameof(Member),
                    r => r.RelatedTo,
                    nameof(Member),
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Member, Setting>(
                    r => r.Members,
                    r => r.Settings,
                    ExpandSite.OnRight,
                    true
                )
                .RelateSetToSet<Member, Detail>(
                    r => r.Members,
                    r => r.Details,
                    ExpandSite.OnRight,
                    true
                )
               .RelateSetToSet<Member, Group>(
                    r => r.Members,
                    r => r.Groups,
                    ExpandSite.OnRight,
                    true
                );
        }
    }
}
