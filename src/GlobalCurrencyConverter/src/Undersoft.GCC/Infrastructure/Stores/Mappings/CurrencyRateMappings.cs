using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GCC.Infrastructure.Stores.Mappings
{
    using Undersoft.GCC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class CurrencyRateMappings : EntityTypeMapping<CurrencyRate>
    {
        const string TABLE_NAME = "CurrencyRates";

        public override void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToSet<CurrencyProvider, CurrencyRate>(
                    r => r.Provider,
                    r => r.Rates,
                    ExpandSite.OnLeft
                ).RelateOneToSet<CurrencyRateTable, CurrencyRate>(
                    r => r.Table,
                    r => r.Rates,
                    ExpandSite.OnLeft
                );
        }
    }
}
