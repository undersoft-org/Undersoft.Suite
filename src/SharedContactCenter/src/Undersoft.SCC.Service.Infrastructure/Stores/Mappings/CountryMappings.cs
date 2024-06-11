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
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Countries;

/// <summary>
/// The country mappings.
/// </summary>
public class CountryMappings : EntityTypeMapping<Country>
{
    const string TABLE_NAME = "Countries";

    /// <summary>
    /// TODO: Add Summary.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
            .RelateOneToSet<Country, CountryState>(
                c => c.Country,
                c => c.States,
                ExpandSite.OnRight,
                true
            )
            .RelateOneToSet<Country, ContactAddress>(
                c => c.Country,
                c => c.Addresses,
                ExpandSite.OnLeft,
                true
            );
    }
}
