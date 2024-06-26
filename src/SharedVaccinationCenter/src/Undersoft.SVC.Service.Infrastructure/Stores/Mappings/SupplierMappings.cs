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
    using Undersoft.SVC.Domain.Entities;
    using Undersoft.SVC.Domain.Entities.Catalogs;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class SupplierMappings : EntityTypeMapping<Supplier>
    {
        const string TABLE_NAME = "Suppliers";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Supplier> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToOne<Supplier, Organization>(
                    r => r.Supplier,
                    r => r.Organization,
                    ExpandSite.WithMany,
                    true
                ).RelateOneToOne<Supplier, Address>(
                    r => r.Supplier,
                    r => r.Address,
                    ExpandSite.WithMany,
                    true
                ).RelateOneToOne<Supplier, Professional>(
                    r => r.Supplier,
                    r => r.Professional,
                    ExpandSite.WithMany,
                    true
                );
        }
    }
}
