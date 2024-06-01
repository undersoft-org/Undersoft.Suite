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

            builder.HasOne(c => c.Address).WithOne().HasForeignKey<Account>(u => u.AddressId);

            builder.HasOne(c => c.Professional).WithOne().HasForeignKey<Account>(c => c.ProfessionalId);

            builder.HasOne(c => c.Organization).WithOne().HasForeignKey<Account>(c => c.OrganizationId);

            builder.HasOne(c => c.Consent).WithOne().HasForeignKey<Account>(c => c.ConsentId);

            builder.HasOne(c => c.Subscription).WithOne().HasForeignKey<Account>(c => c.SubscriptionId);

            builder.HasOne(c => c.Payment).WithOne().HasForeignKey<Account>(c => c.PaymentId);
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
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountOrganization>(k => k.AccountId);
        }
    }

    public class AccountSubscriptionsMappings : EntityTypeMapping<AccountSubscription>
    {
        public override void Configure(EntityTypeBuilder<AccountSubscription> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountSubscription>(k => k.AccountId);
        }
    }

    public class AccountConsentsMappings : EntityTypeMapping<AccountConsent>
    {
        public override void Configure(EntityTypeBuilder<AccountConsent> builder)
        {
            builder.HasOne(c => c.Account).WithOne().HasForeignKey<AccountConsent>(k => k.AccountId);
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
