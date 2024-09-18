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
    using Undersoft.SVC.Domain.Entities.Vaccination;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class VaccineMappings : EntityTypeMapping<Vaccine>
    {
        const string TABLE_NAME = "Vaccines";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Vaccine> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
               .RelateOneToOne<Safety, Vaccine>(
                    r => r.Safety,
                    r => r.Vaccine,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Specification, Vaccine>(
                    r => r.Specification,
                    r => r.Vaccine,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToSet<Vaccine, Procedure>(
                    r => r.Vaccine,
                    r => r.Procedures,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToSet<Vaccine, Certificate>(
                    r => r.Vaccine,
                    r => r.Certificates,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToSet<Vaccine, PostSymptom>(
                    r => r.Vaccine,
                    r => r.PostSymptoms,
                    ExpandSite.OnLeft,
                    true
                );
        }
    }
}
