using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GCC.Infrastructure.Stores.Mappings
{
    using Undersoft.GCC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class CurrencyProviderMappings : EntityTypeMapping<CurrencyProvider>
    {
        const string TABLE_NAME = "CurrencyProviders";

        public override void Configure(EntityTypeBuilder<CurrencyProvider> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToSet<CurrencyProvider, CurrencyRate>(
                    r => r.Provider,
                    r => r.Rates,
                    ExpandSite.OnLeft
                ).RelateOneToSet<CurrencyProvider, CurrencyRateTable>(
                    r => r.Provider,
                    r => r.Tables,
                    ExpandSite.OnLeft
                );
        }
    }
}
