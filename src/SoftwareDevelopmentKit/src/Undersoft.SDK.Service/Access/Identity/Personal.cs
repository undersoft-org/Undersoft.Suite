using Undersoft.SDK.Rubrics.Attributes;
using Undersoft.SDK.Service.Data.Model.Attributes;
using Undersoft.SDK.Service.Data.Object;

namespace Undersoft.SDK.Service.Access.Identity;

public class Personal : DataObject, IPersonal
{
    [VisibleRubric]
    [DisplayRubric("Upload image")]
    [ViewImage(ViewImageMode.Persona, "30px", "30px")]
    [FileRubric(FileRubricType.Property, "ImageData")]
    public string Image { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("First name")]
    public string FirstName { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Last name")]
    public string LastName { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    public string Email { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Phone number")]
    public string PhoneNumber { get; set; }

    [VisibleRubric]
    [RequiredRubric]
    [DisplayRubric("Day of birth")]
    public DateTime Birthdate { get; set; } = DateTime.Parse("01.01.1990");

    public byte[] ImageData { get; set; }

    public string Title { get; set; }

    public string SecondName { get; set; }

    public string SocialMedia { get; set; }

    public string Websites { get; set; }

    public string Gender { get; set; }
}
