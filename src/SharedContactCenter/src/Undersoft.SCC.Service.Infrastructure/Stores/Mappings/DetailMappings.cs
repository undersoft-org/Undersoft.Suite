using Microsoft.EntityFrameworkCore;
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
    public class DetailMappings : EntityTypeMapping<Detail>
    {
        /// <summary>
        /// The TABLE NAME.
        /// </summary>
        const string TABLE_NAME = "Details";

        public override void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Detail>()
                .RelateSetToSet<Detail, Detail>(
                    r => r.RelatedFrom,
                    nameof(Detail),
                    r => r.RelatedTo,
                    nameof(Detail),
                    ExpandSite.OnRight
                );
            ;
        }
    }
}
