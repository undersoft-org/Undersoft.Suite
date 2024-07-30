namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountOrganization : Access.Models.Organization
{
    public virtual long? OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
