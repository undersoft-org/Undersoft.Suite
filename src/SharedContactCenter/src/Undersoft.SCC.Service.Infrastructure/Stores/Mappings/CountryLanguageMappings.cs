using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public class CountryLanguageMappings : EntityTypeMapping<CountryLanguage>
{
    const string TABLE_NAME = "CountryLanguages";

    public override void Configure(EntityTypeBuilder<CountryLanguage> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
            .RelateOneToSet<CountryLanguage, Country>(
                c => c.Language,
                c => c.Countries,
                ExpandSite.OnLeft
            );
    }
}
