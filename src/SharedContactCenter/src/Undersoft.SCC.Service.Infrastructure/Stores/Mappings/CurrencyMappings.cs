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

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;
    using Undersoft.SCC.Domain.Entities.Countries;

    /// <summary>
    /// The currency mappings.
    /// </summary>
    public class CurrencyMappings : EntityTypeMapping<Currency>
    {
        const string TABLE_NAME = "Currencies";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="builder">The builder.</param>
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
