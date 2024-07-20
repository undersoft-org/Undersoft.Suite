namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountTenant : DataObject, ITenant
{
    public string TenantName { get; set; }

    public string TenantFullName { get; set; }

    public string TenantUrl { get; set; }

    public string TenantRoute { get; set; }

    public virtual EntitySet<Account> Accounts { get; set; }
}
