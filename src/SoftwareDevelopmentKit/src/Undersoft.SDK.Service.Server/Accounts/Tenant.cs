namespace Undersoft.SDK.Service.Server.Accounts;

public class Tenant : Access.MultiTenancy.Tenant
{
    public virtual EntitySet<AccountTenant> AccountTenants { get; set; }
}


