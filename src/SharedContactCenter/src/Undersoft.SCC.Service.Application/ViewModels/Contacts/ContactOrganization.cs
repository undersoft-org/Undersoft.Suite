using Undersoft.SCC.Domain.Entities.Enums;
using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

public class ContactOrganization : DataObject, IViewModel
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
    public OrganizationSize OrganizationSize { get; set; }

    [VisibleRubric]
    [DisplayRubric("ContactOrganization logo")]
    [ViewImage(ViewImageMode.Regular, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "OrganizationImageData")]
    public string? OrganizationImage { get; set; }

    public byte[]? OrganizationImageData { get; set; }

    public long? ContactId { get; set; }
}
