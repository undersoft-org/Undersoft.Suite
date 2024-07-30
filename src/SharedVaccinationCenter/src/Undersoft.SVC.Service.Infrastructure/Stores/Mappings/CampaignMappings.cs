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
    public class CampaignMappings : EntityTypeMapping<Campaign>
    {
        const string TABLE_NAME = "Campaigns";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Campaign> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
               .RelateOneToSet<Campaign, Appointment>(
                    r => r.Campaign,
                    r => r.Appointments,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Campaign, Price>(
                 r => r.Campaign,
                 r => r.Price,
                 ExpandSite.OnRight,
                 true
             ).RelateSetToSet<Campaign, Vaccine>(
                    r => r.Campaigns,
                    r => r.Vaccines,
                    ExpandSite.OnRight,
                    true
                );
        }
    }
}
