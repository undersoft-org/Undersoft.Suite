using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity;

public class Organization : DataObject, IOrganization
{
    [VisibleRubric]
    [DisplayRubric("Organization logo")]
    [ViewImage(ViewImageMode.Regular, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "OrganizationImageData")]
    public string OrganizationImage { get; set; }

    [VisibleRubric]
    [DisplayRubric("Industry")]
    public string OrganizationIndustry { get; set; }

    [VisibleRubric]
    [DisplayRubric("Short name")]
    public string OrganizationName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Full name")]
    public string OrganizationFullName { get; set; }

    [VisibleRubric]
    [DisplayRubric("Position")]
    public string PositionInOrganization { get; set; }

    [VisibleRubric]
    [DisplayRubric("Websites")]
    public string OrganizationWebsites { get; set; }

    public byte[] OrganizationImageData { get; set; }

    public OrganizationSize OrganizationSize { get; set; }

}
