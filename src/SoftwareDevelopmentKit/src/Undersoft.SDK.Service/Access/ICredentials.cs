namespace Undersoft.SDK.Service.Access
{
    public interface ICredentials
    {
        int AccessFailedCount { get; set; }
        string Email { get; set; }
        string EmailConfirmationToken { get; set; }
        bool EmailConfirmed { get; set; }
        string NormalizedUserName { get; set; }
        string OldPassword { get; set; }
        string Password { get; set; }
        string PasswordResetToken { get; set; }
        string PhoneNumber { get; set; }
        string PhoneNumberConfirmationToken { get; set; }
        bool PhoneNumberConfirmed { get; set; }
        bool RegistrationCompleted { get; set; }
        string RegistrationCompleteToken { get; set; }
        bool SaveAccountInCookies { get; set; }
        string SessionToken { get; set; }
        string UserName { get; set; }
        bool Authenticated { get; set; }
        bool IsLockedOut { get; set; }
    }
}