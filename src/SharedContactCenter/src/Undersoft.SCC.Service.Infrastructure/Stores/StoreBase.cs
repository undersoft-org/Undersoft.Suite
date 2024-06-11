using Microsoft.EntityFrameworkCore;

// ********************************************************
//   Copyright (c) Undersoft. All Rights Reserved.
//   Licensed under the MIT License. 
//   author: Dariusz Hanc
//   email: dh@undersoft.pl
//   library: Undersoft.SCC.Service.Infrastructure
// ********************************************************

using Undersoft.SDK.Service.Data.Store;

namespace Undersoft.SCC.Service.Infrastructure.Stores;

using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;

/// <summary>
/// The store base.
/// </summary>
/// <typeparam name="TStore"/>
/// <typeparam name="TContext"/>
public class StoreBase<TStore, TContext> : DbStore<TStore, TContext>
    where TStore : IDataServerStore
    where TContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StoreBase"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public StoreBase(DbContextOptions<TContext> options) : base(options) { }

    /// <summary>
    /// Gets or sets the contacts.
    /// </summary>
    public virtual DbSet<Member>? Members { get; set; }
    /// Gets or sets the details.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    public virtual DbSet<Detail>? Details { get; set; }
    /// <summary>
    /// Gets or sets the settings.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    public virtual DbSet<Setting>? Settings { get; set; }
    /// <summary>
    /// Gets or sets the settings.
    /// </summary>
    /// <value>A TODO: Add missing XML "/&gt;</value>
    public virtual DbSet<Group>? Groups { get; set; }
    /// <summary>
    /// Called when [model creating].
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyMapping(new MemberMappings());
        modelBuilder.ApplyMapping(new DetailMappings());
        modelBuilder.ApplyMapping(new SettingMappings());
        modelBuilder.ApplyMapping(new GroupMappings());

        base.OnModelCreating(modelBuilder);
    }
}
