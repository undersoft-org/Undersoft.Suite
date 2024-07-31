namespace Undersoft.SDK.Service.Server.Accounts.Identity;

public class AccountOrganization : Access.Identity.Organization
{
    public virtual long? OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
