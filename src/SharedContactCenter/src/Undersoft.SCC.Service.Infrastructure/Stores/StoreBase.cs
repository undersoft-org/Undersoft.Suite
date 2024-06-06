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
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Countries;
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
    public virtual DbSet<Contact>? Contacts { get; set; }
    /// <summary>
    /// Gets or sets the contact addresses.
    /// </summary>
    public virtual DbSet<Address>? Addresses { get; set; }
    /// <summary>
    /// Gets or sets the contact organizations.
    /// </summary>
    public virtual DbSet<ContactOrganization>? Organizations { get; set; }
    /// <summary>
    /// Gets or sets the contact personals.
    /// </summary>
    public virtual DbSet<ContactPersonal>? Personals { get; set; }
    /// <summary>
    /// Gets or sets the contact proffesionals.
    /// </summary>
    public virtual DbSet<ContactProfessional>? Proffesionals { get; set; }
    /// <summary>
    /// Gets or sets the groups.
    /// </summary>
    public virtual DbSet<Group>? Groups { get; set; }
    /// <summary>
    /// Gets or sets the countries.
    /// </summary>
    public virtual DbSet<Country>? Countries { get; set; }
    /// <summary>
    /// Gets or sets the country states.
    /// </summary>
    public virtual DbSet<CountryState>? CountryStates { get; set; }
    /// <summary>
    /// Gets or sets the currencies.
    /// </summary>
    public virtual DbSet<Currency>? Currencies { get; set; }
    /// <summary>
    /// Gets or sets the country languages.
    /// </summary>
    public virtual DbSet<CountryLanguage>? Languages { get; set; }

    /// <summary>
    /// Called when [model creating].
    /// </summary>
    /// <param name="modelBuilder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyMapping(new ContactAddressMappings());
        modelBuilder.ApplyMapping(new CountryStateMappings());
        modelBuilder.ApplyMapping(new ContactOrganizationMappings());
        modelBuilder.ApplyMapping(new ContactMappings());
        modelBuilder.ApplyMapping(new ContactPersonalMappings());
        modelBuilder.ApplyMapping(new GroupMappings());
        modelBuilder.ApplyMapping(new CountryLanguageMappings());
        modelBuilder.ApplyMapping(new CountryMappings());
        modelBuilder.ApplyMapping(new CurrencyMappings());
        modelBuilder.ApplyMapping(new ContactProfessionalMappings());

        base.OnModelCreating(modelBuilder);
    }
}
