namespace Undersoft.SDK.Service.Server.Accounts;

public class AccountPersonal : DataObject
{
    public string Title { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string FirstName { get; set; }

    public string SecondName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public string SocialMedia { get; set; }

    public string Websites { get; set; }

    public string Gender { get; set; }

    public string Image { get; set; }

    public byte[] ImageData { get; set; }

    public long? AccountId { get; set; }
    public virtual Account Account { get; set; }
}
