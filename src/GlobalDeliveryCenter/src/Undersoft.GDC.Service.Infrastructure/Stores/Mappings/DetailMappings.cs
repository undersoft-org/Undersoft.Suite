using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.GDC.Domain.Entities;

    public class DetailMappings : EntityTypeMapping<Detail>
    {
        const string TABLE_NAME = "DetailSet";

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
