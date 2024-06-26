using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

namespace Undersoft.SVC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SVC.Domain.Entities.Catalogs;
    using Undersoft.SVC.Domain.Entities.Inventory;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class StockMappings : EntityTypeMapping<Stock>
    {
        const string TABLE_NAME = "Stocks";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Stock> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
               .RelateOneToOne<Vaccine, Stock>(
                    r => r.Vaccine,
                    r => r.Stock,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToSet<Stock, Request>(
                    r => r.Stock,
                    r => r.Requests,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToSet<Stock, Traffic>(
                    r => r.Stock,
                    r => r.Traffics,
                    ExpandSite.OnLeft,
                    true
                );
        }
    }
}
