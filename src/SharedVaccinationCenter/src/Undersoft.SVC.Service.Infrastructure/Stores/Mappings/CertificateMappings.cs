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
    public class CertificateMappings : EntityTypeMapping<Procedure>
    {
        const string TABLE_NAME = "Certificates";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Procedure> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToOne<Procedure, Certificate>(
                    r => r.Procedure,
                    r => r.Certificate,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Personal, Certificate>(
                    r => r.Personal,
                    r => r.Certificate,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Term, Certificate>(
                    r => r.Term,
                    r => r.Certificate,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Payment, Certificate>(
                    r => r.Payment,
                    r => r.Certificate,
                    ExpandSite.OnLeft,
                    true
                ); ;
        }
    }
}
