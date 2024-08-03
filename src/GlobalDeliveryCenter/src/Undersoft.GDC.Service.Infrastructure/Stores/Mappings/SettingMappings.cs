using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.GDC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class SettingMappings : EntityTypeMapping<Setting>
    {
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
