using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Contracts.Contacts;

public class ContactOrganization : DataObject
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

    [DisplayRubric("Size")]
    public OrganizationSize? OrganizationSize { get; set; }

    [VisibleRubric]
    [DisplayRubric("Organization logo")]
    [FileRubric(FileRubricType.Path, "OrganizationImageData")]
    public string? OrganizationImage { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? ContactId { get; set; }
}
