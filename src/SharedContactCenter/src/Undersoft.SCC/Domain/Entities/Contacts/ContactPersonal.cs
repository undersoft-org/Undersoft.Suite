namespace Undersoft.SCC.Domain.Entities.Contacts;

public class ContactPersonal : Entity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public DateTime Birthdate { get; set; } = DateTime.Parse("01.01.1990");

    public string? PersonalImage { get; set; }

    public byte[]? PersonalImageData { get; set; }

    public long? ContactId { get; set; }
    public virtual Contact? Contact { get; set; }


}
