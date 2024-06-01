﻿using Microsoft.EntityFrameworkCore;
using Undersoft.SDK.Service.Data.Event;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

namespace Undersoft.SSC.Service.Infrastructure.Stores
{
    public class EventStore : DbStore<IEventStore, EventStore>
    {
        public EventStore(DbContextOptions<EventStore> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyMapping(new EventDbStoreMapping());
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Event>? Events { get; set; }
    }
}
