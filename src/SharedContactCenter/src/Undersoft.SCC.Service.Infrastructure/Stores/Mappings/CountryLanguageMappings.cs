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

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Countries;

/// <summary>
/// The country language mappings.
/// </summary>
public class CountryLanguageMappings : EntityTypeMapping<CountryLanguage>
{
    const string TABLE_NAME = "Languages";

    /// <summary>
    /// TODO: Add Summary.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void Configure(EntityTypeBuilder<CountryLanguage> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
            .RelateOneToSet<CountryLanguage, Country>(
                c => c.Language,
                c => c.Countries,
                ExpandSite.OnLeft,
                true
            );
    }
}
