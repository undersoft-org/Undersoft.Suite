using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License.
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SCC.Domain.Entities;
    using Undersoft.SCC.Domain.Entities.Contacts;
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;

    /// <summary>
    /// The contact mappings.
    /// </summary>
    public class ContactMappings : EntityTypeMapping<Contact>
    {
        const string TABLE_NAME = "Contacts";

        /// <summary>
        /// TODO: Add Summary.
        /// </summary>
        /// <param name="typeBuilder">The type builder.</param>
        public override void Configure(EntityTypeBuilder<Contact> typeBuilder)
        {
            typeBuilder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Contact>()
                 .RelateSetToSet<Contact, Contact>(
                    r => r.RelatedFrom,
                    nameof(Contact),
                    r => r.RelatedTo,
                    nameof(Contact),
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Contact, Setting>(
                    r => r.Contacts,
                    r => r.Settings,
                    ExpandSite.OnRight,
                    true
                )
                .RelateSetToSet<Contact, Detail>(
                    r => r.Contacts,
                    r => r.Details,
                    ExpandSite.OnRight,
                    true
                )
                .RelateSetToSet<Contact, Group>(
                    c => c.Contacts,
                    c => c.Groups,
                    ExpandSite.OnRight,
                    true
                )
                .RelateOneToOne<Contact, ContactAddress>(
                    r => r.Contact,
                    r => r.Address,
                    ExpandSite.OnRight,
                    true
                )
                .RelateOneToOne<Contact, ContactPersonal>(
                    r => r.Contact,
                    r => r.Personal,
                    ExpandSite.OnRight,
                    true
                )
                .RelateOneToOne<Contact, ContactProfessional>(
                    r => r.Contact,
                    r => r.Professional,
                    ExpandSite.OnRight,
                    true
                );
        }
    }
}
