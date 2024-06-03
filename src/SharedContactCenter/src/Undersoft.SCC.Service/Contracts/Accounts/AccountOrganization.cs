using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Contract;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SCC.Service.Contracts.Accounts;

public class AccountOrganization : DataObject, IContract
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
    [DisplayRubric("ContactOrganization logo")]
    [ViewImage(ViewImageMode.Regular, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "OrganizationImageData")]
    public string? OrganizationImage { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? AccountId { get; set; }
}
