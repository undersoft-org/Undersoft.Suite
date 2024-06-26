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
    public class PostSymptomMappings : EntityTypeMapping<PostSymptom>
    {
        const string TABLE_NAME = "PostSymptoms";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<PostSymptom> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToOne<Personal, PostSymptom>(
                    r => r.Personal,
                    r => r.PostSymptom,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Term, PostSymptom>(
                    r => r.Term,
                    r => r.PostSymptom,
                    ExpandSite.OnLeft,
                    true
                );
        }
    }
}
