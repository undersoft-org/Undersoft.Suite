using Microsoft.EntityFrameworkCore;

namespace Undersoft.GDC.Service.Infrastructure.Stores;

using Undersoft.GDC.Domain.Entities;
using Undersoft.GDC.Service.Infrastructure.Stores.Mappings;
using Undersoft.GDC.Service.Infrastructure.Stores.Mappings.Locations;
using Undersoft.SDK.Service.Data.Store;
using Undersoft.SDK.Service.Infrastructure.Database;

public class StoreBase<TStore, TContext> : DbStore<TStore, TContext>
    where TStore : IDataServerStore
    where TContext : DbContext
{
    public StoreBase(DbContextOptions<TContext> options) : base(options) { }

    public virtual DbSet<Service>? Services { get; set; }
    public virtual DbSet<Member>? Members { get; set; }
    public virtual DbSet<Activity>? Activities { get; set; }
    public virtual DbSet<Resource>? Resources { get; set; }
    public virtual DbSet<Schedule>? Schedules { get; set; }
    public virtual DbSet<Detail>? Details { get; set; }
    public virtual DbSet<Setting>? Settings { get; set; }
    public virtual DbSet<Group>? Groups { get; set; }
    public virtual DbSet<Location>? Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyMapping(new ServiceMappings());
        modelBuilder.ApplyMapping(new MemberMappings());
        modelBuilder.ApplyMapping(new ActivityMappings());
        modelBuilder.ApplyMapping(new ResourceMappings());
        modelBuilder.ApplyMapping(new ScheduleMappings());
        modelBuilder.ApplyMapping(new DetailMappings());
        modelBuilder.ApplyMapping(new SettingMappings());
        modelBuilder.ApplyMapping(new GroupMappings());
        modelBuilder.ApplyMapping(new LocationMappings());
        modelBuilder.ApplyMapping(new AddressesMappings());
        modelBuilder.ApplyMapping(new EndpointMappings());
        modelBuilder.ApplyMapping(new PlaceMappings());

        base.OnModelCreating(modelBuilder);
    }
}
