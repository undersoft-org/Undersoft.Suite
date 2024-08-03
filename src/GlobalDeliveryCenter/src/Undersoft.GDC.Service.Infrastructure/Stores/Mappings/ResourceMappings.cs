using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.GDC.Service.Infrastructure.Stores.Mappings;

using Undersoft.GDC.Domain.Entities;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;
using Undersoft.SDK.Service.Infrastructure.Database.Relation;

public class ResourceMappings : EntityTypeMapping<Resource>
{
    const string TABLE_NAME = "Resources";

    public override void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        ModelBuilder
                 .ApplyIdentifiers<Resource>()
                 .RelateSetToSet<Resource, Schedule>(
                     r => r.Resources,
                     r => r.Schedules,
                     ExpandSite.OnRight
                 )
                 .RelateSetToSet<Resource, Setting>(
                     r => r.Resources,
                     r => r.Settings,
                     ExpandSite.OnRight
                 )
                 .RelateOneToOne<Resource, Location>(
                     r => r.Resource,
                     r => r.Location,
                     ExpandSite.OnRight
                 )
                 .RelateSetToSet<Resource, Detail>(
                     r => r.Resources,
                     r => r.Details,
                     ExpandSite.OnRight
                 )
                 .RelateSetToSet<Resource, Resource>(
                     r => r.RelatedFrom,
                     nameof(Resource),
                     r => r.RelatedTo,
                     nameof(Resource),
                     ExpandSite.OnRight
                 );
    }
}