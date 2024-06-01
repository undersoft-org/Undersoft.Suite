using Microsoft.EntityFrameworkCore;

namespace Undersoft.SCC.Service.Infrastructure.Stores;

using Undersoft.SCC.Domain.Entities;
using Undersoft.SCC.Domain.Entities.Contacts;
using Undersoft.SCC.Domain.Entities.Countries;
using Undersoft.SCC.Service.Infrastructure.Stores.Mappings;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public class StoreBase<TStore, TContext> : DbStore<TStore, TContext>
    where TStore : IDataServerStore
    where TContext : DbContext
{
    public StoreBase(DbContextOptions<TContext> options) : base(options) { }

    public virtual DbSet<Contact>? Contacts { get; set; }
    public virtual DbSet<ContactAddress>? ContactAddresses { get; set; }
    public virtual DbSet<ContactOrganization>? ContactOrganizations { get; set; }
    public virtual DbSet<ContactPersonal>? ContactPersonals { get; set; }
    public virtual DbSet<ContactProfessional>? ContactProffesionals { get; set; }
    public virtual DbSet<Group>? Groups { get; set; }
    public virtual DbSet<Country>? Countries { get; set; }
    public virtual DbSet<CountryState>? CountryStates { get; set; }
    public virtual DbSet<Currency>? Currencies { get; set; }
    public virtual DbSet<CountryLanguage>? CountryLanguages { get; set; }

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
