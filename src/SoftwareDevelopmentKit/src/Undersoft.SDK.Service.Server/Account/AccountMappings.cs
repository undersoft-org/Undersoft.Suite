using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Undersoft.SDK.Service.Server.Accounts
{
    using Undersoft.SDK.Service.Data.Store;
    using Undersoft.SDK.Service.Infrastructure.Database;

    public class AccountMappings : EntityTypeMapping<Account>
    {
        const string TABLE_NAME = "Accounts";

        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(TABLE_NAME, DataStoreSchema.DomainSchema);

            builder.HasMany(c => c.Tokens).WithOne(c => c.Account).HasForeignKey(c => c.AccountId);

            builder.HasOne(c => c.User).WithOne().HasForeignKey<Account>(u => u.UserId);

            builder.HasOne(c => c.Personal).WithOne().HasForeignKey<Account>(u => u.PersonalId);
            builder.Navigation(n => n.Personal).AutoInclude();

            builder.HasOne(c => c.Address).WithOne().HasForeignKey<Account>(u => u.AddressId);
            builder.Navigation(n => n.Address).AutoInclude();

            builder.HasOne(c => c.Professional).WithOne().HasForeignKey<Account>(c => c.ProfessionalId);
            builder.Navigation(n => n.Professional).AutoInclude();

            builder.HasOne(c => c.Organization).WithMany(o => o.Accounts).HasForeignKey(c => c.OrganizationId);
            builder.Navigation(n => n.Organization).AutoInclude();

            builder.HasOne(c => c.Consent).WithOne().HasForeignKey<Account>(c => c.ConsentId);
            builder.Navigation(n => n.Consent).AutoInclude();

            builder.HasOne(c => c.Subscription).WithMany(s => s.Accounts).HasForeignKey(c => c.SubscriptionId);
            builder.Navigation(n => n.Subscription).AutoInclude();

            builder.HasOne(c => c.Payment).WithOne().HasForeignKey<Account>(c => c.PaymentId);
            builder.Navigation(n => n.Payment).AutoInclude();
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
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountPersonal>(k => k.AccountId);
        }
    }

    public class AccountAddressMappings : EntityTypeMapping<AccountAddress>
    {
        public override void Configure(EntityTypeBuilder<AccountAddress> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountAddress>(k => k.AccountId);
        }
    }

    public class AccountTokenMappings : EntityTypeMapping<AccountToken>
    {
        public override void Configure(EntityTypeBuilder<AccountToken> builder)
        {
            builder.HasOne(c => c.Account).WithMany(c => c.Tokens).HasForeignKey(c => c.AccountId);
        }
    }

    public class AccountProffesionalMappings : EntityTypeMapping<AccountProfessional>
    {
        public override void Configure(EntityTypeBuilder<AccountProfessional> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountProfessional>(k => k.AccountId);
        }
    }

    public class AccountOrganizationsMappings : EntityTypeMapping<AccountOrganization>
    {
        public override void Configure(EntityTypeBuilder<AccountOrganization> builder)
        {
            builder.HasMany(c => c.Accounts).WithOne(c => c.Organization).HasForeignKey(k => k.OrganizationId);
        }
    }

    public class AccountSubscriptionsMappings : EntityTypeMapping<AccountSubscription>
    {
        public override void Configure(EntityTypeBuilder<AccountSubscription> builder)
        {
            builder.HasMany(c => c.Accounts).WithOne(c => c.Subscription).HasForeignKey(k => k.SubscriptionId);
            builder.HasOne(c => c.Subscription).WithMany(c => c.AccountSubscriptions).HasForeignKey(k => k.SubscriptionId);
        }
    }

    public class SubscriptionsMappings : EntityTypeMapping<Subscription>
    {
        public override void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasMany(c => c.AccountSubscriptions).WithOne(c => c.Subscription).HasForeignKey(k => k.SubscriptionId);
        }
    }

    public class AccountConsentsMappings : EntityTypeMapping<AccountConsent>
    {
        public override void Configure(EntityTypeBuilder<AccountConsent> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountConsent>(k => k.AccountId);
        }
    }

    public class ConsentMappings : EntityTypeMapping<Consent>
    {
        public override void Configure(EntityTypeBuilder<Consent> builder)
        {
            builder.HasMany(c => c.AccountConsents).WithOne(c => c.Consent).HasForeignKey(k => k.ConsentId);
        }
    }

    public class AccountPaymentsMappings : EntityTypeMapping<AccountPayment>
    {
        public override void Configure(EntityTypeBuilder<AccountPayment> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountPayment>(k => k.AccountId);
        }
    }
}
