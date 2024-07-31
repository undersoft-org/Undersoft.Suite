namespace Undersoft.SDK.Service.Server.Accounts.Identity;

public class Organization : Access.Identity.Organization
{
    public virtual EntitySet<AccountOrganization> AccountOrganizations { get; set; }
}
