using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SSC.Service.Contracts.Accounts;

public class AccountOrganization : DataObject
{
    [VisibleRubric]
    [DisplayRubric("Industry")]
    public string? OrganizationIndustry { get; set; }

    [VisibleRubric]
    [DisplayRubric("Short name")]
    public string? OrganizationName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Full name")]
    public string? OrganizationFullName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Position")]
    public string? PositionInOrganization { get; set; }

    [VisibleRubric]
    [DisplayRubric("Websites")]
    public string? OrganizationWebsites { get; set; }

    [VisibleRubric]
    [FileRubric(FileRubricType.Path, "OrganizationImageData")]
    public string? OrganizationImage { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? AccountId { get; set; }
}
