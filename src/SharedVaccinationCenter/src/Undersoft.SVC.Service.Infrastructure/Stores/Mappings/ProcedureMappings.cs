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

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class ProcedureMappings : EntityTypeMapping<Procedure>
    {
        const string TABLE_NAME = "Procedures";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Procedure> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToOne<Procedure, Appointment>(
                    r => r.Procedure,
                    r => r.Appointment,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Procedure, Cost>(
                    r => r.Procedure,
                    r => r.Cost,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Procedure, Price>(
                    r => r.Procedure,
                    r => r.Price,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Procedure, Term>(
                    r => r.Procedure,
                    r => r.Term,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Procedure, PostSymptom>(
                    r => r.Procedure,
                    r => r.PostSymptom,
                    ExpandSite.OnLeft,
                    true
                );
        }
    }
}
