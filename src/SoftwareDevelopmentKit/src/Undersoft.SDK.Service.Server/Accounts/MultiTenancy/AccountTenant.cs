namespace Undersoft.SDK.Service.Server.Accounts.MultiTenancy;

public class AccountTenant : Access.MultiTenancy.Tenant
{
    public long? TenantId { get; set; }
    public virtual Tenant Tenant { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
