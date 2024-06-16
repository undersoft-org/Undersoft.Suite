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
                .RelateOneToOne<PostSymptom, Personal>(
                    r => r.PostSymptom,
                    r => r.Personal,
                    ExpandSite.OnRight,
                    true
                ).RelateOneToOne<PostSymptom, Term>(
                    r => r.PostSymptom,
                    r => r.Term,
                    ExpandSite.OnRight,
                    true
                );
        }
    }
}
