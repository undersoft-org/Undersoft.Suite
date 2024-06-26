using Microsoft.EntityFrameworkCore;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SVC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SVC.Service.Infrastructure.Stores
{
    /// <summary>
    /// The event store.
    /// </summary>
    public class EventStore : DbStore<IEventStore, EventStore>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventStore"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public EventStore(DbContextOptions<EventStore> options) : base(options) { }

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyMapping(new DbEventStoreMappings());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        public virtual DbSet<Event>? Events { get; set; }
    }
}