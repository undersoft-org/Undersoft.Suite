namespace Undersoft.SDK.Service.Access
{
    public enum AccessStatus
    {
        Unsigned,
        Failure,
        Succeed,
        SignedIn,
        SignedOut,
        EmailConfirmed,
        TryoutsOverlimit,
        InvalidEmail,
        InvalidPassword,
        RegistrationCompleted,
        RegistrationNotCompleted,
        RegistrationNotConfirmed,
        EmailNotConfirmed,
        ResetPasswordConfirmed,
        ResetPasswordNotConfirmed,
        ActionRequired
    }
}
