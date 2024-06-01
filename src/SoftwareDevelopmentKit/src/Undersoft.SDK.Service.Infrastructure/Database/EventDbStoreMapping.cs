using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Infrastructure.Database;

using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;

public class EventDbStoreMapping : EntityTypeMapping<Event>
{
    private const string TABLE_NAME = "Events";

    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

        builder.Property(p => p.PublishTime)
            .HasColumnType("timestamp");
    }
}