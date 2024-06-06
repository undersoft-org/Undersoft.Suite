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
/// The contact professional mappings.
/// </summary>
public class ContactProfessionalMappings : EntityTypeMapping<ContactProfessional>
{
    const string TABLE_NAME = "Professionals";

    /// <summary>
    /// TODO: Add Summary.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public override void Configure(EntityTypeBuilder<ContactProfessional> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
    }
}
