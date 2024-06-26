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
    using Undersoft.SVC.Domain.Entities;
    using Undersoft.SVC.Domain.Entities.Inventory;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class TrafficMappings : EntityTypeMapping<Traffic>
    {
        const string TABLE_NAME = "Traffics";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Traffic> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
              .RelateOneToOne<Cost, Traffic>(
                   r => r.Cost,
                   r => r.Traffic,
                   ExpandSite.OnLeft,
                   true
               ).RelateOneToOne<Price, Traffic>(
                   r => r.Price,
                   r => r.Traffic,
                   ExpandSite.OnLeft,
                   true
               );
        }
    }
}
