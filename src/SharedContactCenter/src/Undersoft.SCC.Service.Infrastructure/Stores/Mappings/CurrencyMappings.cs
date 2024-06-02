using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;
    using Undersoft.SCC.Domain.Entities.Countries;
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
               .RelateOneToSet<Currency, Country>(
                c => c.Currency,
                c => c.Countries,
                ExpandSite.OnLeft,
                true
            );
        }
    }
}
