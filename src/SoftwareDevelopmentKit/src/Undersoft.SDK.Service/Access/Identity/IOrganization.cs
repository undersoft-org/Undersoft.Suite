namespace Undersoft.SDK.Service.Access.Identity
{
    public interface IOrganization
    {
        string OrganizationFullName { get; set; }
        string OrganizationImage { get; set; }
        byte[] OrganizationImageData { get; set; }
        string OrganizationIndustry { get; set; }
        string OrganizationName { get; set; }
        string OrganizationWebsites { get; set; }
        string PositionInOrganization { get; set; }
    }
}