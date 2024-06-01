using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SSC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SSC.Domain.Entities;

    public class ApplicationMappings : EntityTypeMapping<Application>
    {
        const string TABLE_NAME = "Applications";

        public override void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Application>()
                .RalateSetToSetExplicitly<Service, Application>(
                    r => r.Services,
                    r => r.Applications,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Application, Member>(
                    r => r.Applications,
                    r => r.Members,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Application, Setting>(
                    r => r.Applications,
                    r => r.Settings,
                    ExpandSite.OnRight
                )
                .RelateOneToOne<Application, Location>(
                    r => r.Application,
                    r => r.Location,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Application, Detail>(
                    r => r.Applications,
                    r => r.Details,
                    ExpandSite.OnRight,
                    true
                )
                .RelateOneToSet<Default, Application>(
                    r => r.Default,
                    r => r.Applications,
                    ExpandSite.OnLeft
                )
                .RelateSetToSet<Application, Application>(
                    r => r.RelatedFrom,
                    nameof(Application),
                    r => r.RelatedTo,
                    nameof(Application),
                    ExpandSite.OnRight
                );
        }
    }
}
