﻿using Microsoft.EntityFrameworkCore;
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

using Undersoft.SCC.Domain.Entities.Contacts;

/// <summary>
/// The contact organization mappings.
/// </summary>
public class ContactOrganizationMappings : EntityTypeMapping<ContactOrganization>
{
    const string TABLE_NAME = "Organizations";

    /// <summary>
    /// TODO: Add Summary.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void Configure(EntityTypeBuilder<ContactOrganization> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
    }
}
