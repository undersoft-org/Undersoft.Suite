using Undersoft.SDK.Service.Data.Contract;

namespace Undersoft.SCC.Service.Contracts.Contacts;

public class ContactPersonal : DataObject, IContract
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public DateTime Birthdate { get; set; } = DateTime.UtcNow;

    public string? PersonalImage { get; set; }

    public byte[]? PersonalImageData { get; set; }

    public long? ContactId { get; set; }

}
