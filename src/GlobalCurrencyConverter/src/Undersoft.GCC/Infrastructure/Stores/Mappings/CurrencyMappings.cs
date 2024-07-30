using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GCC.Infrastructure.Stores.Mappings
{
    using Undersoft.GCC.Domain.Entities;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    public class CurrencyMappings : EntityTypeMapping<Currency>
    {
        const string TABLE_NAME = "Currencies";

        public override void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToSet<Currency, CurrencyProvider>(
                    r => r.BaseCurrency,
                    r => r.Providers,
                    ExpandSite.OnLeft
                )
                .RelateOneToSet<Currency, CurrencyRate>(
                    r => r.SourceCurrency,
                    r => r.SourceRates,
                    ExpandSite.OnLeft
                )
                .RelateOneToSet<Currency, CurrencyRate>(
                    r => r.TargetCurrency,
                    r => r.TargetRates,
                    ExpandSite.OnLeft
                ).RelateOneToSet<Currency, CurrencyRateTable>(
                    r => r.SourceCurrency,
                    r => r.RateTables,
                    ExpandSite.OnLeft
                );
        }
    }
}
