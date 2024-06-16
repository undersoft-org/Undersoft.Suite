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
                .RelateOneToOne<Certificate, Procedure>(
                    r => r.Certificate,
                    r => r.Procedure,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Certificate, Personal>(
                    r => r.Certificate,
                    r => r.Personal,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Certificate, Term>(
                    r => r.Certificate,
                    r => r.Term,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<Certificate, Payment>(
                    r => r.Certificate,
                    r => r.Payment,
                    ExpandSite.OnRight,
                    true
                ); ;
        }
    }
}
