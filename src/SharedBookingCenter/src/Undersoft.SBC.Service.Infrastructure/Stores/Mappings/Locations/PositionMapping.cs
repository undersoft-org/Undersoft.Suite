﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SBC.Service.Infrastructure.Stores.Mappings.Locations;

using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;
using Undersoft.SBC.Domain.Entities.Locations;

public class PositionMappings : EntityTypeMapping<Position>
{
    private const string TABLE_NAME = "Positions";

    public override void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);
    }
}