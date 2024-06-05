using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

public class ContactProfessional : DataObject, IViewModel
{
    [VisibleRubric]
    [DisplayRubric("Industry")]
    public string? ProfessionIndustry { get; set; }

    [VisibleRubric]
    public string? Profession { get; set; } = default!;

    [VisibleRubric]
    [DisplayRubric("Email")]
    public string? ProfessionalEmail { get; set; }

    [VisibleRubric]
    [DisplayRubric("Phone number")]
    public string? ProfessionalPhoneNumber { get; set; }

    [VisibleRubric]
    [DisplayRubric("Social media")]
    public string? ProfessionalSocialMedia { get; set; }

    [VisibleRubric]
    [DisplayRubric("Websites")]
    public string? ProfessionalWebsites { get; set; }

    [VisibleRubric]
    [DisplayRubric("Experience in years")]
    public float? ProfessionalExperience { get; set; }

    public long? ContactId { get; set; }

}
