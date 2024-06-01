using Undersoft.SDK.Service.Data.Object.Detail;

namespace Undersoft.SSC.Service.Contracts.Details;

[Detail]
public class Personal : DataObject
{
    public Personal() { }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? LastName { get; set; }

    public string? BirthDate { get; set; }

    public string? Age { get; set; }

    public string? Gender { get; set; }
}
