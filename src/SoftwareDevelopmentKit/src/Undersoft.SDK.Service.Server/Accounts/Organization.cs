namespace Undersoft.SDK.Service.Server.Accounts;

public class Organization : Access.Models.Organization
{
    public virtual EntitySet<AccountOrganization> AccountOrganizations { get; set; }
}
