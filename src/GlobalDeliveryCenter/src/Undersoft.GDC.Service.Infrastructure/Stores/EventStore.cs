using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.GDC.Service.Infrastructure.Stores
{
    public class EventStore : DbStore<IEventStore, EventStore>
    {
        public EventStore(DbContextOptions<EventStore> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyMapping(new DbEventStoreMappings());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Event>? Events { get; set; }
    }
}
