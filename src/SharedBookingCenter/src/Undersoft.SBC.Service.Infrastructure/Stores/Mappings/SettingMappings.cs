using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SBC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SBC.Domain.Entities;

    public class SettingMappings : EntityTypeMapping<Setting>
    {
        const string TABLE_NAME = "SettingSet";

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
