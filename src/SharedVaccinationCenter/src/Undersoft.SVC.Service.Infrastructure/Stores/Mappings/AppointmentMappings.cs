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
    using Undersoft.SVC.Domain.Entities.Vaccination;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class AppointmentMappings : EntityTypeMapping<Appointment>
    {
        const string TABLE_NAME = "Appointments";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Appointment> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .RelateOneToOne<Personal, Appointment>(
                    r => r.Personal,
                    r => r.Appointment,
                    ExpandSite.OnLeft,
                    true
                ).RelateOneToOne<Schedule, Appointment>(
                    r => r.Schedule,
                    r => r.Appointment,
                    ExpandSite.WithMany,
                    true
                );
        }
    }
}
