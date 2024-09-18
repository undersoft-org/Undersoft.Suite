using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Server.Accounts
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;
    using Undersoft.SDK.Service.Server.Accounts.Identity;
    using Undersoft.SDK.Service.Server.Accounts.Licensing;
    using Undersoft.SDK.Service.Server.Accounts.MultiTenancy;
    using Undersoft.SDK.Service.Server.Accounts.Tokens;

    public class AccountMappings : EntityTypeMapping<Account>
    {
        const string TABLE_NAME = "Accounts";

        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            builder.HasMany(c => c.Tokens).WithOne(c => c.Account).HasForeignKey(c => c.AccountId);

            builder.HasOne(c => c.Address).WithOne().HasForeignKey<Account>(c => c.AddressId);
            builder.Navigation(n => n.Address).AutoInclude();

            builder.HasOne(c => c.Personal).WithOne().HasForeignKey<Account>(c => c.PersonalId);
            builder.Navigation(n => n.Personal).AutoInclude();

            builder.HasOne(c => c.Professional).WithOne().HasForeignKey<Account>(c => c.ProfessionalId);
            builder.Navigation(n => n.Professional).AutoInclude();

            builder.HasOne(c => c.Organization).WithOne().HasForeignKey<Account>(c => c.OrganizationId);
            builder.Navigation(n => n.Organization).AutoInclude();

            builder.HasOne(c => c.Subscription).WithOne().HasForeignKey<Account>(c => c.SubscriptionId);
            builder.Navigation(n => n.Subscription).AutoInclude();

            builder.HasOne(c => c.Consent).WithOne().HasForeignKey<Account>(c => c.ConsentId);
            builder.Navigation(n => n.Consent).AutoInclude();

            builder.HasOne(c => c.Payment).WithOne().HasForeignKey<Account>(c => c.PaymentId);
            builder.Navigation(n => n.Payment).AutoInclude();

            builder.HasOne(c => c.Tenant).WithOne().HasForeignKey<Account>(c => c.TenantId);
            builder.Navigation(n => n.Tenant).AutoInclude();
        }
    }

    public class RolemMappings : EntityTypeMapping<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(c => c.Claims).WithOne(c => c.Role).HasForeignKey(c => c.AccountRoleId);
        }
    }

    public class AccountPersonalMappings : EntityTypeMapping<AccountPersonal>
    {
        public override void Configure(EntityTypeBuilder<AccountPersonal> builder)
        {
            builder
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<AccountPersonal>(k => k.AccountId);
        }
    }

    public class AccountAddressMappings : EntityTypeMapping<AccountAddress>
    {
        public override void Configure(EntityTypeBuilder<AccountAddress> builder)
        {
            builder
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<AccountAddress>(k => k.AccountId);
        }
    }

    public class AccountTokenMappings : EntityTypeMapping<AccountToken>
    {
        public override void Configure(EntityTypeBuilder<AccountToken> builder)
        {
            builder.HasOne(c => c.Account).WithMany(t => t.Tokens).HasForeignKey(k => k.AccountId);
        }
    }

    public class AccountProffesionalMappings : EntityTypeMapping<AccountProfessional>
    {
        public override void Configure(EntityTypeBuilder<AccountProfessional> builder)
        {
            builder
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<AccountProfessional>(k => k.AccountId);
        }
    }

    public class AccountOrganizationsMappings : EntityTypeMapping<AccountOrganization>
    {
        public override void Configure(EntityTypeBuilder<AccountOrganization> builder)
        {
            builder
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<AccountOrganization>(k => k.AccountId);
            builder
                .HasOne(c => c.Organization)
                .WithMany(c => c.AccountOrganizations)
                .HasForeignKey(k => k.OrganizationId);
        }
    }

    public class OrganizationMappings : EntityTypeMapping<Organization>
    {
        public override void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder
                .HasMany(c => c.AccountOrganizations)
                .WithOne(c => c.Organization)
                .HasForeignKey(k => k.OrganizationId);
        }
    }

    public class AccountSubscriptionsMappings : EntityTypeMapping<AccountSubscription>
    {
        public override void Configure(EntityTypeBuilder<AccountSubscription> builder)
        {
            builder
                 .HasOne(c => c.Account)
                 .WithOne()
                 .HasForeignKey<AccountSubscription>(k => k.AccountId);
            builder
                .HasOne(c => c.Subscription)
                .WithMany(c => c.AccountSubscriptions)
                .HasForeignKey(k => k.SubscriptionId);
        }
    }

    public class SubscriptionsMappings : EntityTypeMapping<Subscription>
    {
        public override void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder
                .HasMany(c => c.AccountSubscriptions)
                .WithOne(c => c.Subscription)
                .HasForeignKey(k => k.SubscriptionId);
        }
    }

    public class AccountConsentsMappings : EntityTypeMapping<AccountConsent>
    {
        public override void Configure(EntityTypeBuilder<AccountConsent> builder)
        {
            builder
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<AccountConsent>(k => k.AccountId);
            builder
                .HasOne(c => c.Consent)
                .WithMany(c => c.AccountConsents)
                .HasForeignKey(k => k.ConsentId);
        }
    }

    public class ConsentMappings : EntityTypeMapping<Consent>
    {
        public override void Configure(EntityTypeBuilder<Consent> builder)
        {
            builder
                .HasMany(c => c.AccountConsents)
                .WithOne(c => c.Consent)
                .HasForeignKey(k => k.ConsentId);
        }
    }

    public class AccountTenantMappings : EntityTypeMapping<AccountTenant>
    {
        public override void Configure(EntityTypeBuilder<AccountTenant> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountTenant>(k => k.AccountId);
            builder
                .HasOne(c => c.Tenant)
                .WithMany(c => c.AccountTenants)
                .HasForeignKey(k => k.TenantId);
        }
    }

    public class TenantMappings : EntityTypeMapping<Tenant>
    {
        public override void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder
                .HasMany(c => c.AccountTenants)
                .WithOne(t => t.Tenant)
                .HasForeignKey(k => k.TenantId);
        }
    }

    public class AccountPaymentsMappings : EntityTypeMapping<AccountPayment>
    {
        public override void Configure(EntityTypeBuilder<AccountPayment> builder)
        {
            builder
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<AccountPayment>(k => k.AccountId);
            builder
                .HasOne(c => c.Payment)
                .WithMany(c => c.AccountPayments)
                .HasForeignKey(k => k.PaymentId);
        }
    }

    public class PaymentMappings : EntityTypeMapping<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder
                .HasMany(c => c.AccountPayments)
                .WithOne(t => t.Payment)
                .HasForeignKey(k => k.PaymentId);
        }
    }
}
