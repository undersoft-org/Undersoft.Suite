namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountOrganization : DataObject
{
    public string OrganizationIndustry { get; set; }

    public string OrganizationName { get; set; }

    public string OrganizationFullName { get; set; }

    public string OrganizationWebsites { get; set; }

    public string PositionInOrganization { get; set; }

    public string OrganizationImage { get; set; }

    public byte[] OrganizationImageData { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
