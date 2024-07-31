namespace Undersoft.SDK.Service.Access.Identity
{
    public interface IPersonal
    {
        DateTime Birthdate { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string Gender { get; set; }
        string Image { get; set; }
        byte[] ImageData { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string SecondName { get; set; }
        string SocialMedia { get; set; }
        string Title { get; set; }
        string Websites { get; set; }
    }
}