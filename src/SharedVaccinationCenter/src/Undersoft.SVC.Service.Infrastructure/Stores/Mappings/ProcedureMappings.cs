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
    using Undersoft.SVC.Domain.Entities.Vaccination;

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
                .RelateOneToOne<Appointment, Procedure>(
                    r => r.Appointment,
                    r => r.Procedure,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Cost, Procedure>(
                    r => r.Cost,
                    r => r.Procedure,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Price, Procedure>(
                    r => r.Price,
                    r => r.Procedure,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Term, Procedure>(
                    r => r.Term,
                    r => r.Procedure,
                    ExpandSite.OnLeft,
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
