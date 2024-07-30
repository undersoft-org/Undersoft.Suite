using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GCC.Infrastructure.Stores.Mappings
{
    using Undersoft.GCC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class CurrencyRateTableMappings : EntityTypeMapping<CurrencyRateTable>
    {
        const string TABLE_NAME = "CurrencyRateTables";

        public override void Configure(EntityTypeBuilder<CurrencyRateTable> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
        }
    }
}
