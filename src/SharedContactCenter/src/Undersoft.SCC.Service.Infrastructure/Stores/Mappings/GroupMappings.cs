﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;

    /// <summary>
    /// The detail mappings.
    /// </summary>
    public class GroupMappings : EntityTypeMapping<Group>
    {
        /// <summary>
        /// The TABLE NAME.
        /// </summary>
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
