using Undersoft.SDK.Rubrics.Attributes;

namespace Undersoft.SSC.Service.Contracts.Accounts;

public class AccountPersonal : DataObject
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
    public DateTime Birthdate { get; set; } = DateTime.Parse("01.01.1990");

    [VisibleRubric]
    [DisplayRubric("Upload image")]
    [FileRubric(FileRubricType.Path, "ImageData")]
    public string Image { get; set; } = default!;

    public byte[] ImageData { get; set; } = default!;

    public long? AccountId { get; set; }
}
