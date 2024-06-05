using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SCC.Service.Application.ViewModels.Contacts;

public class ContactPersonal : DataObject, IViewModel
{
    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("First name")]
    public string FirstName { get; set; } = default!;

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Last name")]
    public string LastName { get; set; } = default!;

    [VisibleRubric]
    [RequiredRubric]
    public string Email { get; set; } = default!;

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Phone number")]
    public string PhoneNumber { get; set; } = default!;

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Day of birth")]
    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    [VisibleRubric]
    [DisplayRubric("ContactPersonal image")]
    [FileRubric(FileRubricType.Path, "PersonalImageData")]
    public string? PersonalImage { get; set; }

    public byte[]? PersonalImageData { get; set; }

    public long? ContactId { get; set; }

}
