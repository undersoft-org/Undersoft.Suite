using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SBC.Service.Infrastructure.Stores.Mappings
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Infrastructure.Database.Relation;
    using Undersoft.SBC.Domain.Entities;

    public class ServiceMappings : EntityTypeMapping<Service>
    {
        const string TABLE_NAME = "Services";

        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            ModelBuilder
                .ApplyIdentifiers<Service>()
                .RelateSetToSet<Service, Member>(
                    r => r.Services,
                    r => r.Members,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Service, Setting>(
                    r => r.Services,
                    r => r.Settings,
                    ExpandSite.OnRight
                )
                .RelateOneToOne<Service, Location>(
                    r => r.Service,
                    r => r.Location,
                    ExpandSite.OnRight
                )
                .RelateSetToSet<Service, Detail>(
                    r => r.Services,
                    r => r.Details,
                    ExpandSite.OnRight
                )
                .RelateOneToSet<Default, Service>(
                    r => r.Default,
                    r => r.Services,
                    ExpandSite.OnLeft
                )
                .RelateSetToSet<Service, Service>(
                    r => r.RelatedFrom,
                    nameof(Service),
                    r => r.RelatedTo,
                    nameof(Service),
                    ExpandSite.OnRight
                );
            ;
        }
    }
}
