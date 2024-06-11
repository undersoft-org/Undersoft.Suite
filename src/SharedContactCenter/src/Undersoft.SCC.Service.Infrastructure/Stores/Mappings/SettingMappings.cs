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
    /// The setting mappings.
    /// </summary>
    public class SettingMappings : EntityTypeMapping<Setting>
    {
        /// <summary>
        /// The TABLE NAME.
        /// </summary>
        const string TABLE_NAME = "Settings";

        public override void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Setting>()
                .RelateSetToSet<Setting, Setting>(
                    r => r.RelatedFrom,
                     nameof(Setting),
                    r => r.RelatedTo,
                    nameof(Setting),
                    ExpandSite.OnRight
                );
        }
    }
}
