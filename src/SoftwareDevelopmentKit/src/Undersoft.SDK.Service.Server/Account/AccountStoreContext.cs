using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using Undersoft.SDK.Service.Infrastructure.Database;

namespace Undersoft.SDK.Service.Server.Accounts;

public partial class AccountStore<TStore, TContext> : AccountStoreContext<TStore>
    where TStore : IDataServerStore
    where TContext : DbContext
{
    public AccountStore(DbContextOptions<TContext> options) : base(options) { }
}

public partial class AccountStoreContext<TStore>
    : IdentityDbContext<
        AccountUser,
        Role,
        long,
        AccountClaim,
        AccountRole,
        AccountLogin,
        RoleClaim,
        AccountToken
    >,
        IDataStoreContext<TStore> where TStore : IDataServerStore
{
    public AccountStoreContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Accounts");
        builder
            .ApplyMapping<Account>(new AccountMappings())
            .ApplyMapping<AccountToken>(new AccountTokenMappings())
            .ApplyMapping<Role>(new RolemMappings())
            .ApplyMapping<AccountPersonal>(new AccountPersonalMappings())
            .ApplyMapping<AccountProfessional>(new AccountProffesionalMappings())
            .ApplyMapping<AccountOrganization>(new AccountOrganizationsMappings())
            .ApplyMapping<AccountSubscription>(new AccountSubscriptionsMappings())
            .ApplyMapping<AccountConsent>(new AccountConsentsMappings())
            .ApplyMapping<AccountPayment>(new AccountPaymentsMappings());

        builder.Entity<Account>(entity =>
        {
            entity.ToTable(name: "Accounts");
        });
        builder.Entity<AccountUser>(entity =>
        {
            entity.ToTable(name: "AccountUsers");
        });
        builder.Entity<Role>(entity =>
        {
            entity.ToTable(name: "Roles");
        });
        builder.Entity<AccountRole>(entity =>
        {
            entity.ToTable("AccountRoles");
        });
        builder.Entity<AccountClaim>(entity =>
        {
            entity.ToTable("AccountClaims");
        });
        builder.Entity<AccountLogin>(entity =>
        {
            entity.ToTable("AccountLogins");
        });
        builder.Entity<RoleClaim>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<AccountToken>(entity =>
        {
            entity.ToTable("AccountTokens");
        });
        builder.Entity<AccountPersonal>(entity =>
        {
            entity.ToTable(name: "AccountPersonals");
        });
        builder.Entity<AccountProfessional>(entity =>
        {
            entity.ToTable("AccountProffesionals");
        });
        builder.Entity<AccountOrganization>(entity =>
        {
            entity.ToTable("AccountOrganizations");
        });
        builder.Entity<AccountSubscription>(entity =>
        {
            entity.ToTable("AccountSubscriptions");
        });
        builder.Entity<AccountConsent>(entity =>
        {
            entity.ToTable("AccountConsents");
        });
        builder.Entity<AccountPayment>(entity =>
        {
            entity.ToTable("AccountPayments");
        });
    }

    public IQueryable<TEntity> EntitySet<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public IQueryable EntitySet(Type type)
    {
        return (IQueryable)this.GetEntitySet(type);
    }

    public IQueryable<TEntity> Query<TEntity>() where TEntity : class
    {
        return (IQueryable<TEntity>)EntitySet<TEntity>();
    }

    public new TEntity Add<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Add<TEntity>(entity).Entity;
    }

    public async new ValueTask<TEntity> AddAsync<TEntity>(
        TEntity entity,
        CancellationToken cancellationToken
    ) where TEntity : class
    {
        return await ValueTask.FromResult((await base.AddAsync<TEntity>(entity)).Entity);
    }

    public new object Add(object entity)
    {
        return base.Add(entity).Entity;
    }

    public new TEntity Update<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Update(entity).Entity;
    }

    public new TEntity Remove<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Update(entity).Entity;
    }

    public new object Attach(object entity)
    {
        return base.Attach(entity).Entity;
    }

    public new TEntity Attach<TEntity>(TEntity entity) where TEntity : class
    {
        return base.Attach(entity).Entity;
    }

    public object AttachProperty(object entity, string propertyName, Type type = null)
    {
        if (type == null)
        {
            Attach(entity);
            return entity;
        }
        else if (type.IsAssignableTo(typeof(IEnumerable)))
        {
            var list = Entry(entity).Collection(propertyName);
            Attach(list.EntityEntry.Entity);
            list.Load();
            return list.CurrentValue;
        }
        else
        {
            var obj = Entry(entity).Reference(propertyName);
            Attach(obj.EntityEntry.Entity);
            obj.Load();
            return obj.CurrentValue;
        }
    }

    public virtual Task<int> Save(bool asTransaction, CancellationToken token = default)
    {
        return saveEndpoint(asTransaction, token);
    }

    private async Task<int> saveEndpoint(bool asTransaction, CancellationToken token = default)
    {
        if (ChangeTracker.HasChanges())
        {
            if (asTransaction)
                return await saveAsTransaction(token);
            else
                return await saveChanges(token);
        }
        return 0;
    }

    private async Task<int> saveAsTransaction(CancellationToken token = default)
    {
        await using var tr = await Database.BeginTransactionAsync(token);
        try
        {
            var changes = await SaveChangesAsync(token);

            await tr.CommitAsync(token);

            return changes;
        }
        catch (DbUpdateException e)
        {
            if (e is DbUpdateConcurrencyException)
                tr.Warning<Datalog>(
                    $"Concurrency update exception data changed by: {e.Source}, "
                        + $"entries involved in detail data object",
                    e.Entries,
                    e
                );
            else
                tr.Failure<Datalog>(
                    $"Fail on update database transaction Id:{tr.TransactionId}, using context:{GetType().Name},"
                        + $" context Id:{ContextId}, TimeStamp:{DateTime.Now.ToString()} {e.StackTrace}",
                    e.Entries
                );

            await tr.RollbackAsync(token);

            tr.Warning<Datalog>($"Transaction Id:{tr.TransactionId} Rolling Back !!");
        }
        return -1;
    }

    private async Task<int> saveChanges(CancellationToken token = default)
    {
        try
        {
            return await SaveChangesAsync(token);
        }
        catch (DbUpdateException e)
        {
            if (e is DbUpdateConcurrencyException)
                this.Warning<Datalog>(
                    $"Concurrency update exception data changed by: {e.Source}, "
                        + $"entries involved in detail data object",
                    e.Entries,
                    e
                );
            else
                this.Failure<Datalog>(
                    $"Fail on update database, using context:{GetType().Name}, "
                        + $"context Id: {ContextId}, TimeStamp: {DateTime.Now.ToString()}"
                );
        }

        return -1;
    }
}
