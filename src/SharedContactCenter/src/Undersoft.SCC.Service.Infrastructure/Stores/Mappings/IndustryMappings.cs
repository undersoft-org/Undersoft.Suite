using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;

/// <summary>
/// The contact organization mappings.
/// </summary>
public class IndustryMappings : EntityTypeMapping<Industry>
{
    const string TABLE_NAME = "Industries";

    /// <summary>
    /// TODO: Add Summary.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void Configure(EntityTypeBuilder<Industry> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder.RelateOneToSet<Industry, Organization>(
                   l => l.Industry,
                   r => r.Organizations,
                   ExpandSite.OnLeft,
                   true
               ).RelateOneToSet<Industry, ContactProfessional>(
                   l => l.Industry,
                   r => r.ContactProfessionals,
                   ExpandSite.OnLeft,
                   true
               );
    }
}
